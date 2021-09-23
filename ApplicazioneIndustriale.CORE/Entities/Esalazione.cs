using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicazioneIndustriale.CORE.Entities
{
    public class Esalazione
    {
        //le proprietà sono le colonne del mio database
        public int Id { get; set; }
        public DateTime DataMisurazione { get; set; }
        public TimeSpan OraMisurazione { get; set; }
        public double ConcentrazionePpm { get; set; }
        public bool? Stato { get; set; } //nullabile perchè lo è sul database
    }
}
