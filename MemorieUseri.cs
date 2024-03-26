using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_re
{

        internal class MemorieUseri
        {

            private const int NR_MAX_USERI = 100;

            private User[] useri;
            private int NrUseri = 0;


            public MemorieUseri()
            {
                //alocare memorie pt array de useri
                useri = new User[NR_MAX_USERI];
                NrUseri = 0;


            }

            //adaugare user in array
            public void AddUser(User user)
            {
                useri[NrUseri] = user;
                NrUseri++;

            }

            public User[] GetUseri()
            {
                return useri;
            }


       
            public string Info(User user)
            {
                string Info = $"Nume utilizator:{user.Nume} Prenume:{user.Prenume}";
                return Info;
            }
        





        }
    }



