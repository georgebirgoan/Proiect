using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clase;
using NivelStocareDate;

//istoric tranzactii bancomat si stocare in memorie toate tranzactiile
//sortare dupa litera alfabet


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
            Console.WriteLine("NC.Numere Card");
            Console.WriteLine("TR.Istoric Tranzactii");
            Console.WriteLine("I. Info despre card");
            Console.WriteLine("4.Iesire");
            Console.WriteLine("\n");

            int nrUseri = 0;
            int NrNumere = 0;
            double suma = 0.0;
            int IdUser = 0;
            double depozit = 0;
            double retragere = 0;
            int nrTranzactii = 0;


            string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
            AdministrareFisierText adminUseriFisier = new AdministrareFisierText(numeFisier);


            Bancomat bancomat = new Bancomat();
            User    user= new User();
            MemorieUseri memorie = new MemorieUseri();

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
                                Console.WriteLine($"Afisare pin user:{user.Pin}");
                              
                                memorie.AddUser(user);
                                break;
                            case "V":
                                optiune= VerificareUser(user);
                                break;
                            case "A":

                                AfisareUserCurent(user);
                                break;

                            case "D":
                                ++nrTranzactii;
                                //bancomat.IstoricTranz = nrTranz;

                                depozit = DepunereNumerar(user, bancomat, depozit);
                                //Console.WriteLine($"Tranzactia:{nrTranz},depunere suma:{depozit}");


                                Bancomat tranz = new Bancomat("DEPUNERE", depozit, DateTime.Now);
                                memorie.AddIstoricTranz(tranz);
                                break;

                            case "R":
                                ++nrTranzactii;
                                //bancomat.IstoricTranz = nrTranz;

                                retragere = RetragereNumerar(user, bancomat);

                                //Console.WriteLine($"Tranzactia:{nrTranz},retragere suma:{user.Balance}");

                                Bancomat tranz2 = new Bancomat("RETRAGERE", retragere, DateTime.Now);
                                memorie.AddIstoricTranz(tranz2);

                                break;

                            case "AF":
                                User[] utilizatori = adminUseriFisier.GetUseriFisier(out nrUseri);
                                Console.WriteLine("Afisare useri fisier:");
                                AfisareUseriFisier(utilizatori, nrUseri);
                                break;

                            case "CN":
                                Console.WriteLine("Introdu nume user:");
                                string cNume = Console.ReadLine();
                                Console.WriteLine("Introdu prenume user:");
                                string cPrenume = Console.ReadLine();
                                memorie.GetUserNume(cNume, cPrenume);
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
                                Console.WriteLine("Useri salvati cu succes in fisier!");

                        break;
                            
                            case "O":
                                User[] useri = adminUseriFisier.GetUseriFisier(out nrUseri);
                                SortareSiAfisareNumePrenume(useri, nrUseri);
                                break;

                            case "TR":
                                Bancomat[] tranzactii = memorie.GetIstoricTranz(nrTranzactii);
                                AfisareTranzactiiBancomat(tranzactii, nrTranzactii);
                                break;


                    case "X":
                        break;

                }
            } while (optiune != "X");

        }



        public static User CitireTastatura()
        {
            bool schimbarePin = false;

            Console.WriteLine("Numele utilizator card");
            string nume = Console.ReadLine();

            Console.WriteLine("Prenume utilizator card");
            string prenume = Console.ReadLine();

            Console.WriteLine("Cod card");
            int codCard = int.Parse(Console.ReadLine());

            Console.WriteLine("Introduceti optiunilea (1 pentru RetragereNumerar, 2 pentru DepunereNumerar, etc.):");
            int optiune = int.Parse(Console.ReadLine());

            Console.WriteLine("Introduceti starea bancomatului (0 pentru Activ, 1 pentru Inactiv, 2 pentru Blocat):");
            int optiuneStare = int.Parse(Console.ReadLine());
            StareBancomat stare = (StareBancomat)optiuneStare;

            // Convertim valoarea int la enum OptiuniBancomat
            OptiuniBancomat optiuniBancomat = (OptiuniBancomat)optiune;

            switch (optiuniBancomat)
            {
                case OptiuniBancomat.RetragereNumerar:
                    if (stare == StareBancomat.Activ)
                    {
                        Console.WriteLine("Pueti efectua operatiunea de retragere numerar.");
                    }
                    else
                    {
                        Console.WriteLine("Bancomatul nu este disponibil pentru retragere de numerar în acest moment.");
                    }
                    break;
                case OptiuniBancomat.DepunereNumerar:
                    if (stare == StareBancomat.Activ)
                    {
                        Console.WriteLine("Pueti efectua operatiunea de depunere numerar.");
                    }
                    else
                    {
                        Console.WriteLine("Bancomatul nu este disponibil pentru depunere de numerar în acest moment.");
                    }
                    break;
                case OptiuniBancomat.InterogareSold:
                    if (stare == StareBancomat.Activ)
                    {
                        Console.WriteLine("Pueti efectua operatiunea de interogare sold.");
                    }
                    else
                    {
                        Console.WriteLine("Bancomatul nu este disponibil pentru interogare de sold în acest moment.");
                    }
                    break;
                case OptiuniBancomat.TransferFonduri:
                    if (stare == StareBancomat.Activ)
                    {
                        Console.WriteLine("Transferul de fonduri nu este disponibil în prezent pt cardul dv.");
                    }
                    else
                    {
                        Console.WriteLine("Bancomatul nu este disponibil pentru transfer de fonduri în acest moment.");
                    }
                    break;
                case OptiuniBancomat.PlataFactura:
                    if (stare == StareBancomat.Activ)
                    {
                        Console.WriteLine("Plata facturilor nu este disponibilă în prezent pt cardul dv.");
                    }
                    else
                    {
                        Console.WriteLine("Bancomatul nu este disponibil pentru plată de facturi în acest moment.");
                    }
                    break;
                case OptiuniBancomat.SchimbarePin:
                    if (stare == StareBancomat.Activ)
                    {
                        Console.WriteLine("Ati selectat schimbarea codului PIN.");
                        schimbarePin = true;
                    }
                    else
                    {
                        Console.WriteLine("Bancomatul nu este disponibil pentru schimbare de PIN în acest moment.");
                    }
                    break;
                default:
                    Console.WriteLine("Opțiune invalidă.");
                    break;
            }

            if (schimbarePin)
            {
                Console.WriteLine("Introduceti noul cod PIN:");
                int newPin = Convert.ToInt32(Console.ReadLine());
                return new User(newPin,0, optiuniBancomat);
            }
            else
            {
                return new User(codCard, 0, optiuniBancomat);
            }
        }



        public static void AfisareUserCurent(User user)
        {
            Console.WriteLine(user.Info());
        }

        public static void AfisareTranzactiiBancomat(Bancomat[] tranzactii, int nrTranzactii)
        {
            Console.WriteLine(nrTranzactii);
            Console.WriteLine("\nTranzactii din bancomat:");
            for (int i = 0; i < nrTranzactii; i++)
            {
                if (tranzactii[i] != null)
                {
                    string infoTranzactie = tranzactii[i].InfoTranzactii();
                    Console.WriteLine(infoTranzactie);
                }
                else
                {
                    Console.WriteLine("Tranzactie nula");
                }
            }
        }

        public static double DepunereNumerar(User user, Bancomat bancomat,double depozit)
        {
            Console.WriteLine('\n');
            Console.WriteLine("Care este suma dorita petru depunere?");

            depozit = Double.Parse(Console.ReadLine());
            user.Balance = depozit;
            bancomat.SoldDepunere(depozit);
            Console.WriteLine($"\n Suma depusa este:{depozit} lei");


            Console.WriteLine('\n');
            return depozit;
        }

        public static double RetragereNumerar(User user,Bancomat bancomat)
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
                return retragere;
            }
            return 0;
        }

        public static string VerificareUser(User useri)
        {
            Console.WriteLine("Introduceti PIN-ul pentru cardul Dvs.:");
            double codCard = double.Parse(Console.ReadLine());
            string optiune = string.Empty;
            if (useri.Pin == codCard)
                    {
                        Console.WriteLine("PIN corect. Puteti efectua operatiuni!");
                        Console.WriteLine("Introdu optiunea dorita");
                    }
                    else
                    {
                        Console.WriteLine("PIN incorect. Va rugam incercati din nou.");
                        Console.WriteLine("Introdu din nou o optiune");
                    }
            return optiune;

                }

        public static void AfisareSoldUser(User user)
        {
            Console.WriteLine($"Sold dv: {user.Balance}");
        }


        public static void AfisareUseriFisier(User[] user, int nrUseri)
        {
            Console.WriteLine("\n Useri din fisier cu nume,prenunme,cod card");

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
