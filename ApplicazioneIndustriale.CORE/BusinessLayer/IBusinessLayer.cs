using ApplicazioneIndustriale.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicazioneIndustriale.CORE.BusinessLayer
{
    public interface IBusinessLayer
    {
        void EseguiCalcoli();
        void AddTemperatura(MisurazioneTemperatura temp);
        void AddEsalazione(Esalazione e);
    }
}
