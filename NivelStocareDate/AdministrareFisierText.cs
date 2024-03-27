using System;
using System.IO;
using Clase;

namespace NivelStocareDate
{
    public class AdministrareFisierText
    {
        private const int NR_MAX_STUDENTI = 50;
        private string numeFisier;

        public AdministrareFisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;
            // se incearca deschiderea fisierului in modul OpenOrCreate
            // astfel incat sa fie creat daca nu exista
            Stream streamFisierText = File.Open(numeFisier, FileMode.OpenOrCreate);
            streamFisierText.Close();

        }

        public void AddUser(User user)
        {
            // instructiunea 'using' va apela la final streamWriterFisierText.Close();
            // al doilea parametru setat la 'true' al constructorului StreamWriter indica
            // modul 'append' de deschidere al fisierului
            using (StreamWriter streamWriterFisierText = new StreamWriter(numeFisier, true))
            {
                streamWriterFisierText.WriteLine(user.ConversieLaSir_PentruFisier());
            }
        }

        public User[] GetUseriFisier(out int nrUseri)
        {
            User[] useri = new User[NR_MAX_STUDENTI];

            // instructiunea 'using' va apela streamReader.Close()
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;
                nrUseri = 0;

                // citeste cate o linie si creaza un obiect de tip User
                // pe baza datelor din linia citita
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    useri[nrUseri++] = new User(linieFisier);
                }
                Console.WriteLine("S-au citit useri din fisier");
            }

            return useri;
        }
    }
}
