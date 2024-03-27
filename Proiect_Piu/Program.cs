using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clase;
using NivelStocareDate;


namespace Proiect_Piu
{
    public class Program
    {
        static void Main()
        {


            Console.WriteLine('\n');
            Console.WriteLine("Alegeti operatiunea dorita..");
            Console.WriteLine("C.Citire user");
            Console.WriteLine("A.Afisare user");
            Console.WriteLine("V.Verificare user");
            Console.WriteLine("D.Depunere nmerar");
            Console.WriteLine("R.Retragere nmerar");
            Console.WriteLine("CN.Cautare utilizator dupa nume");
            Console.WriteLine("AL.Alimentare bancomat numerar");
            Console.WriteLine("SB.Verificare stoc numerar bancomat");
            Console.WriteLine("SU.Interogarare sold user");
            Console.WriteLine("SF.Salvare user fisier");
            Console.WriteLine("AF.Afisare useri fisier");
            Console.WriteLine("O.Ordonare useri alfabetic");
            Console.WriteLine("I. Info despre card");
            Console.WriteLine("4.Iesire");
            Console.WriteLine("\n");

            int nrUseri = 0;
            double suma = 0.0;
            int IdUser = 0;


            string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
            AdministrareFisierText adminUseriFisier = new AdministrareFisierText(numeFisier);


            Bancomat bancomat = new Bancomat();
            User    user= new User();
             MemorieUseri adminUseri = new MemorieUseri();

            //apelare pt id unic
            //daca fisierul este gol->eroare
               adminUseriFisier.GetUseriFisier(out nrUseri);
            

            string optiune;

            do
            {
                Console.WriteLine("\n");
                Console.WriteLine("Alegeti o optiune");
                optiune = Console.ReadLine();

                switch (optiune.ToUpper())
                {
                    case "C":
                        user= CitireTastatura();
                        adminUseri.AddUser(user);
                        break;

                    case "A":
                        AfisareUserCurent(user);
                        break;

                    case "V":
                        optiune = VerificareUser(user);
                        break;
                    case "D":
                        DepunereNumerar(user, bancomat);
                        break;

                    case "R":
                        RetragereNumerar(user,bancomat);
                        break;

                    case "AF":
                        User[] utilizatori = adminUseriFisier.GetUseriFisier(out nrUseri);
                        Console.WriteLine("Afisare useri fisier:");
                        AfisareUseriFisier(utilizatori, nrUseri);

                        break;

                    case "CN":
                        CautareNume(adminUseri);
                        break;

                    case "AL":
                        Console.WriteLine("Introduceti suma pt alimentare bancomat:");
                        suma = double.Parse(Console.ReadLine());
                        bancomat.Balance = suma;
                        break;

                    case "SU":
                        AfisareSoldUser(user);
                        break;

                    case "SB":
                        Console.WriteLine(bancomat.SoldBancomat());
                        break;

                    case "I":
                        user.Info();
                        break;

                    case "SF":
                        int idUser = ++nrUseri;
                        user.IdUser = idUser;
                        //adaugare student in fisier
                        adminUseriFisier.AddUser(user);
                        break;
                    case "O":
                        User[] useri = adminUseriFisier.GetUseriFisier(out nrUseri);
                        SortareSiAfisareNumePrenume(useri, nrUseri);    
                        break;


                    case "X":
                        break;

                }


            } while (optiune != "X");

        }




        public static User CitireTastatura()
        {
            Console.WriteLine("Numele utilizator card");
            string nume = Console.ReadLine();

            Console.WriteLine("Prenume utilizator card");
            string prenume = Console.ReadLine();

            Console.WriteLine("Cod card");
            int codCard = int.Parse(Console.ReadLine());

            User user = new User(codCard, nume, prenume, 0);
            return user;
        }


        public static void AfisareUserCurent(User user)
        {
            Console.WriteLine(user.Info());
        }


        public static void DepunereNumerar(User user, Bancomat bancomat)
        {
            Console.WriteLine('\n');
            Console.WriteLine("Care este suma dorita petru depunere?");

            double depozit = Double.Parse(Console.ReadLine());
            user.Balance = depozit;
            bancomat.SoldDepunere(depozit);
            Console.WriteLine($"\n Suma depusa este:{depozit} lei");
            Console.WriteLine('\n');
        }

        public static void RetragereNumerar(User user,Bancomat bancomat)
        {
            Console.WriteLine("Care este suma pt retragere:");
            double retragere = double.Parse(Console.ReadLine());

            if (user.Balance < retragere)
            {
                Console.WriteLine("Nu exista sold disponibil pt retragere,înncercati o alta operatiune!");
            }
            else
            {
                user.Balance = user.SoldCurentUser(retragere);
                bancomat.SoldRetragere(retragere);
            }
        }

        public static string VerificareUser(User user)
        {
            Console.WriteLine("Introdu pin pt cardul Dv:");
            double codCard = double.Parse(Console.ReadLine());
            string optiune = string.Empty;

            if (user.Pin == codCard)
            {
                Console.WriteLine("Pin corect.Puteti efectua operatiuni!");
                Console.WriteLine("Introdu opt dorita:");
            }
            else
            {
                Console.WriteLine("Pin incorect.Incearca din nou");
                Console.WriteLine("Introdu din nou o optiune:");

            }
            return optiune;

        }

        public static void AfisareSoldUser(User user)
        {
            Console.WriteLine($"Sold dv: {user.Balance}");
        }

        public static void CautareNume(MemorieUseri adminUseri)
        {
            Console.WriteLine("Introdu nume user:");
            string cNume = Console.ReadLine();
            Console.WriteLine("Introdu prenume user:");
            string cPrenume = Console.ReadLine();

            User cautareUser = adminUseri.GetUserNume(cNume, cPrenume);
            if (cautareUser != null)
            { 
                Console.WriteLine("Utilizatorul a fost gasit cu succes!");
            }
            else
            { 
                Console.WriteLine("Utilizator negasit!");
            }
        }

        public static void AfisareUseriFisier(User[] user, int nrUseri)
        {
            Console.WriteLine("\n Useri din fisier cu nume si prenunme");

            for (int i = 0; i < nrUseri; i++)
            {
                if (user[i] != null)
                {
                    string Info = user[i].Info();
                    Console.WriteLine($"{Info}");
                }
                else
                {
                    Console.WriteLine("Utilizator null");
                }
            }
        }




        public static void SortareSiAfisareNumePrenume(User[] useri, int nrUseri)
        {
            // Creați un nou vector care să conțină numai numele și prenumele utilizatorilor
            string[] numePrenume = new string[nrUseri];
            for (int i = 0; i < nrUseri; i++)
            {
                if (useri[i] != null)
                {
                    numePrenume[i] = $"{useri[i].Nume} {useri[i].Prenume}";
                }
            }

            // Sortați numele și prenumele utilizatorilor
            Array.Sort(numePrenume);

            // Afișați numele și prenumele utilizatorilor sortați
            Console.WriteLine("\nUtilizatori ordonați alfabetic după nume:");
            foreach (string np in numePrenume)
            {
                if (np != null)
                {
                    Console.WriteLine(np);
                }
            }
        }










    }
}
