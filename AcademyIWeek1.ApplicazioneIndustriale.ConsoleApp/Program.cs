using ApplicazioneIndustriale.ADO;
using ApplicazioneIndustriale.CORE.BusinessLayer;
using ApplicazioneIndustriale.CORE.Entities;
using System;

namespace AcademyIWeek1.ApplicazioneIndustriale.ConsoleApp
{
    class Program
    {
        private static readonly IBusinessLayer bl = new BusinessLayer(new RepositoryMisurazioneTemperatura(), new RepositoryEsalazione());
        static void Main(string[] args)
        {
            //try
            //{
            //    bl.EseguiCalcoli();
            //}
            //catch(Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            bool check = true;
            int choice;
            do
            {
                Console.WriteLine("Premi 1 per inserire un'esalazione\nPremi 2 per inserire una temperatura\n0.Exit");
                while(!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 2)
                {
                    Console.WriteLine("Inserisci una scelta valida!");
                }

                switch(choice)
                {
                    case 1:
                        AddEsalazione();
                        break;
                    case 2:
                        AddTemperatura();
                        break;
                    case 0:
                        check = false;
                        break;
                }
            } while (check);
        }

        private static void AddTemperatura()
        {
            MisurazioneTemperatura temp = new MisurazioneTemperatura();
            double t;
            DateTime dataMisurazione = DateTime.Now.Date;
            TimeSpan oraMisurazione = dataMisurazione.TimeOfDay;

            Console.WriteLine("Inserisci temperatura");
            while (!double.TryParse(Console.ReadLine(), out t))
            {
                Console.WriteLine("Inserisci una temperatura valida!");
            }

            temp.DataMisurazione = dataMisurazione;
            temp.OraMisurazione = oraMisurazione;
            temp.Temperatura = t;

            bl.AddTemperatura(temp);
        }

        private static void AddEsalazione()
        {
            Esalazione e = new Esalazione();

            double ppm;
            DateTime dataMisurazione = DateTime.Now.Date;
            TimeSpan oraMisurazione = dataMisurazione.TimeOfDay;

            Console.WriteLine("Inserisci concentrazione ppm");
            while (!double.TryParse(Console.ReadLine(), out ppm))
            {
                Console.WriteLine("Inserisci una temperatura valida!");
            }

            e.DataMisurazione = dataMisurazione;
            e.OraMisurazione = oraMisurazione;
            e.ConcentrazionePpm = ppm;

            bl.AddEsalazione(e);
        }
    }
}
