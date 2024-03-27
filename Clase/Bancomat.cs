using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clase

{
    public class Bancomat
    {

        public double Balance { get; set; }


        //stoc bancomat
        public Bancomat()
        {

        }

        public void SoldRetragere(double retragere)
        {
            Balance -= retragere;
        }

        public void SoldDepunere(double retragere)
        {
            Balance += retragere;
        }

        public string SoldBancomat()
        {
            string InfoBancomat = $"Numerar disponibil in bancomat:{Balance}  lei";
            return InfoBancomat;

        }



    }
}
