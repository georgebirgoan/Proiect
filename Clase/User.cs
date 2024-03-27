using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//lab 4
//Id unic
//Scrierea studenti fisier si salvare date fiser
//ordonare dupa nume

namespace Clase
{
    public class User
    {

        private const char SEPARATOR_PRINCIPAL_FISIER = ',';
        private const int ID = 0;
        private const int NUME = 1;
        private const int PRENUME = 2;


        public int Pin { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public double Balance { get; set; }

        public int IdUser { get; set; }


        public User(){}

        public User(int pin, string firstName,
            string lastName, double balance)
        {
            this.Pin = pin;
            this.Nume = firstName;
            this.Prenume = lastName;
            this.Balance = balance;
        }

        public string Info()
        {
            string Info = $"Nume utilizator:{Nume} Prenume:{Prenume}";
            return Info;
        }

        public double SoldCurentUser(double retragere)
        {
            Balance -= retragere;
            return Balance;
        }

        //constructor fisier
        public User(string linieFisier)
        {
            var dateFisier = linieFisier.Split(SEPARATOR_PRINCIPAL_FISIER);

            this.IdUser = Convert.ToInt32(dateFisier[ID]);
            this.Nume = dateFisier[NUME];
            this.Prenume = dateFisier[PRENUME];
        }

        //aici se converteste si se afiseaza
        public string ConversieLaSir_PentruFisier()
        {
            string obiectStudentPentruFisier = string.Format("{1}{0}{2}{0}{3}{0}",
                SEPARATOR_PRINCIPAL_FISIER,
                IdUser.ToString(),
                (Nume ?? " NECUNOSCUT "),
                (Prenume ?? " NECUNOSCUT ")
                );

            return obiectStudentPentruFisier;
        }
    }

}

