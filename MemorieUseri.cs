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

        
        public User GetUserNume(string nume, string prenume)
        {
            User userNou = new User();
            for (int i = 0; i < NrUseri; i++)
            {
                if (useri[i].Nume == nume && useri[i].Prenume == prenume)
                {
                    return useri[i];
                    break;
                }
            }
            return null;
        }

        public User[] GetUseri(out int nrUseri)
        {
            nrUseri = this.NrUseri;
            return useri;
        }



  
        





        }
    }



