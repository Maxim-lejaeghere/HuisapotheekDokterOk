using System;
using System.Collections.Generic;

namespace HuisAppotheek.Domain.DAL
{
    public partial class Dokter
    {
        public Dokter()
        {
            Medicijn = new HashSet<Medicijn>();
        }

        public int Dokterid { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Straat { get; set; }
        public string Huisnummer { get; set; }
        public string Bus { get; set; }
        public string Postcode { get; set; }
        public string Stad { get; set; }
        public string Land { get; set; }
        public string Telefoon { get; set; }
        public string Mobiel { get; set; }
        public string Email { get; set; }
        public string Reservatieurl { get; set; }

        public virtual ICollection<Medicijn> Medicijn { get; set; }
    }
}
