using ApplicazioneIndustriale.CORE.Entities;
using ApplicazioneIndustriale.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicazioneIndustriale.CORE.BusinessLayer
{
    public class BusinessLayer : IBusinessLayer
    {
        private readonly IRepositoryMisurazioneTemperatura misurazTempRepo;
        private readonly IRepositoryEsalazione esalazioneRepo;

        public BusinessLayer(IRepositoryMisurazioneTemperatura repositoryMisurazioneTemperatura, IRepositoryEsalazione repositoryEsalazione)
        {
            misurazTempRepo = repositoryMisurazioneTemperatura;
            esalazioneRepo = repositoryEsalazione;
        }

        public void AddEsalazione(Esalazione e)
        {
            esalazioneRepo.Add(e);
            Evento evento = new Evento();
            evento.MandaLaNotificaCalcoloE += ChiamaCalcolaE;
            evento.SeSalvattagioOkE(e);

        }

        public void AddTemperatura(MisurazioneTemperatura temp)
        {
            misurazTempRepo.Add(temp);
            Evento evento = new Evento();
            evento.MandaLaNotificaCalcoloT += ChiamaCalcolaT;
            evento.SeSalvattagioOkT(temp);
        }

        public void EseguiCalcoli()
        {
            List<Esalazione> esalazioni = new List<Esalazione>();
            List<MisurazioneTemperatura> temperature = new List<MisurazioneTemperatura>();
            try
            {
                esalazioni = esalazioneRepo.GetItemsWithOutState();
                temperature = misurazTempRepo.GetItemsWithOutState();



                if (esalazioni.Count() > 0 && temperature.Count() > 0)
                {
                    //hanno almeno una riga
                    //posso andare a verificare se superano o no la soglia critica
                    foreach (var esalazione in esalazioni)
                    {
                        if (esalazione.ConcentrazionePpm > 10)
                        {
                            esalazione.Stato = true; //true è superamento soglia
                        }
                        else
                        {
                            esalazione.Stato = false;
                        }
                        //prendo la temperatura che corrisponde a quella Data e Ora
                        MisurazioneTemperatura mt = temperature.Where(t => t.DataMisurazione == esalazione.DataMisurazione && t.OraMisurazione == esalazione.OraMisurazione).SingleOrDefault();
                        if (mt == null)
                        {
                            //salvare  sul database
                            esalazioneRepo.Update(esalazione);
                        }
                        else
                        {
                            if (mt.Temperatura > 30)
                            {
                                mt.Stato = true;
                            }
                            else
                            {
                                mt.Stato = false;
                            }

                            //salvare entrambi sul database
                            esalazioneRepo.Update(esalazione);
                            misurazTempRepo.Update(mt);

                            Evento evento = new Evento();
                            evento.MandaLaNotifica += ScriviSuFile;

                            //Se uno dei due ha superato la soglia critica (stato = true) -> evento che scrive su file
                            if (esalazione.Stato == true || mt.Stato == true)
                            {
                                //scrivere su file
                                evento.SeSogliaSuperata(esalazione, mt);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        private void ScriviSuFile(Evento evento, Esalazione esalazione, MisurazioneTemperatura mt)
        {
            //cosa farà alla creazione dell'evento => scrittura su file

            string path = @"C:\Users\naima.el.khattabi\Desktop\Industriale.txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine($"{esalazione.DataMisurazione}, {esalazione.OraMisurazione}" +
                        $"-Temperatura : {mt.Temperatura} gradi  -Esalazione : {esalazione.ConcentrazionePpm} ppm");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void ChiamaCalcolaE(Evento evento, Esalazione esalazione)
        {
            EseguiCalcoli();
        }

        private void ChiamaCalcolaT(Evento evento, MisurazioneTemperatura temperatura)
        {
            EseguiCalcoli();
        }
    }
}
