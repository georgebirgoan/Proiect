using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_re
{
    public class User
    {

        
        public int Pin { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public double Balance { get; set; }


        public User() { }




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

        public  double SoldCurentUser(double retragere)
        {
            Balance -= retragere;
            return Balance;
        }



    }
}
