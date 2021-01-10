using System;
using System.Collections.Generic;

namespace HuisAppotheek.Domain.DAL
{
    public partial class Persoonlijkeapotheek
    {
        public int Apotheekid { get; set; }
        public string Dosering { get; set; }
        public string Groep { get; set; }
        public bool ActiefIngenomen { get; set; }
        public bool InApotheek { get; set; }
        public string Opmerkingen { get; set; }
        public int Patientid { get; set; }
        public int Medicijnid { get; set; }
        public virtual Medicijn Medicijn { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
