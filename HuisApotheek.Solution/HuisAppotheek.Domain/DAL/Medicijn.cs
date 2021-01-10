using System;
using System.Collections.Generic;

namespace HuisAppotheek.Domain.DAL
{
    public partial class Medicijn
    {
        public Medicijn()
        {
            Persoonlijkeapotheek = new HashSet<Persoonlijkeapotheek>();
        }

        public int Medicijnid { get; set; }
        public string Volledigenaam { get; set; }
        public string Groep { get; set; }
        public DateTime Vervaldatum { get; set; }
        public bool OpVoorschrift { get; set; }
        public string Postcode { get; set; }
        public string Bijsluiter { get; set; }
        public string UrlBijsluiter { get; set; }
        public int? Dokterid { get; set; }

        public virtual Dokter Dokter { get; set; }
        public virtual ICollection<Persoonlijkeapotheek> Persoonlijkeapotheek { get; set; }
    }
}
