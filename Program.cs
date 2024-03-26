using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_re
{
    internal class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine('\n');
            Console.WriteLine("Alegeti operatiunea dorita..");
            Console.WriteLine("C.Citire user");
            Console.WriteLine("A.Afisare user");
            Console.WriteLine("V.Verificare user");
            Console.WriteLine("D.Depunere nmerar");
            Console.WriteLine("R.Retragere nmerar");
            Console.WriteLine("T.Afisare useri memorie");
            Console.WriteLine("S.Interogarare sold");
            Console.WriteLine("4.Iesire");
            Console.WriteLine("\n");


            User user = new User();
            MemorieUseri adminUseri = new MemorieUseri();
            string optiune;

            do
            {

                Console.WriteLine("Alegeti o optiune");
                optiune = Console.ReadLine();


                switch (optiune.ToUpper())
                {
                    case "C":
                        user = CitireTastatura();
                        adminUseri.AddUser(user);

                        break;

                    case "A":
                        AfisareUserCurent(user);
                        break;

                    case "V":
                       optiune=VerificareUser(user);
                        break;
                    case "D":
                        DepunereNumerar(user);
                        break;

                    case "R":
                        RetragereNumerar(user);
                        break;

                    case "T":
                        User[] utilizatori = adminUseri.GetUseri();
                        AfisareUseriMemorie(utilizatori);
                        break;
                    case "S":
                        AfisareSoldUser(user);
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


        public static void DepunereNumerar(User user)
        {
            Console.WriteLine('\n');
            Console.WriteLine("Care este suma dorita petru depunere?");

            double depozit = Double.Parse(Console.ReadLine());
            user.Balance = depozit;

            Console.WriteLine($"\n Suma depusa este:{depozit} lei");
        }

        public static void RetragereNumerar(User user)
        {
            Console.WriteLine("Care este suma pt retragere:");
            double retragere = double.Parse(Console.ReadLine());

            if (user.Balance < retragere)
            {
                Console.WriteLine("Nu este sold disponibil pt retragere");
            }
            else
            {
                user.Balance = user.SoldCurentUser(retragere);
            }
        }

        public static string  VerificareUser(User user)
        {
            Console.WriteLine("Introdu pin pt cardul Dv:");
            double codCard = double.Parse(Console.ReadLine());

            string optiune = string.Empty;

            if(user.Pin == codCard)
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



        public static void AfisareUseriMemorie(User[] utilizatori)
        {
            Console.WriteLine("\n Useri din memorie:");

            for (int i = 0; i < utilizatori.Length; i++)
            {
                Console.WriteLine(utilizatori[i].Info());
            }
        }


    }
}
        