using System;
using System.Collections.Generic;

namespace HuisAppotheek.Domain.DAL
{
    public partial class Patient
    {
        public Patient()
        {
            Persoonlijkeapotheek = new HashSet<Persoonlijkeapotheek>();
        }

        public int Patientid { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public DateTime Geboortedatumdatum { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Persoonlijkeapotheek> Persoonlijkeapotheek { get; set; }
    }
}
