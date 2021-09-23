using ApplicazioneIndustriale.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicazioneIndustriale.CORE
{
    public class Evento
    {
        public delegate void ScriviSuFile(Evento evento, Esalazione esalazione, MisurazioneTemperatura mt); //delegato
        public delegate void SalvataggioEsalazione(Evento evento, Esalazione esalazione); //delegato
        public delegate void SalvataggioTemperatura(Evento evento, MisurazioneTemperatura temperatura); //delegato

        public event ScriviSuFile MandaLaNotifica; //evento del tipo del delegato

        public event SalvataggioEsalazione MandaLaNotificaCalcoloE;
        public event SalvataggioTemperatura MandaLaNotificaCalcoloT;

        public void SeSogliaSuperata(Esalazione esalazione, MisurazioneTemperatura mt)
        {
            if (MandaLaNotifica != null)
            {
                MandaLaNotifica(this, esalazione, mt);
            }
        }

        public void SeSalvattagioOkE(Esalazione esalazione)
        {
            if (MandaLaNotificaCalcoloE != null)
            {
                MandaLaNotificaCalcoloE(this, esalazione);
            }
        }

        public void SeSalvattagioOkT(MisurazioneTemperatura temperatura)
        {
            if (MandaLaNotificaCalcoloT != null)
            {
                MandaLaNotificaCalcoloT(this, temperatura);
            }
        }
    }
}
