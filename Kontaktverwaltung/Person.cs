using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontaktverwaltung
{
    [Serializable]
    class Person
    {
        //Members
        private string id, vorname, nachname, telefonnummer, adresse, email;

        //Konstruktor
        public Person()
        {
            this.id = "Not defined";
            this.vorname = "Not defined";
            this.nachname = "Not defined";
            this.telefonnummer = "Not defined";
            this.adresse = "Not defined";
            this.email = "Not defined";
        }
        public Person(string vorname, string nachname, string telefonnummer, string adresse, string email)
        {
            this.vorname = vorname;
            this.nachname = nachname;
            this.telefonnummer = telefonnummer;
            this.adresse = adresse;
            this.email = email;
            this.id = $"{this.vorname} {this.nachname}";
        }

        //Methoden
        public void setVorname(string vorname)
        {
            this.vorname = vorname;
        }

        public string getVorname()
        {
            return this.vorname;
        }

        public void setNachname(string nachname)
        {
            this.nachname = nachname;
        }

        public string getNachname()
        {
            return this.nachname;
        }

        public void setTelefonnummer(string telefonnummer)
        {
            this.telefonnummer = telefonnummer;
        }

        public string getTelefonnummer()
        {
            return this.telefonnummer;
        }

        public void setAdresse(string adresse)
        {
            this.adresse = adresse;
        }

        public string getAdresse()
        {
            return this.adresse;
        }

        public void setEmail(string email)
        {
            this.email = email;
        }

        public string getEmail()
        {
            return this.email;
        }

        public void setId(string id)
        {
            this.id = id;
        }

        public string getId()
        {
            return this.id;
        }
    }
}
