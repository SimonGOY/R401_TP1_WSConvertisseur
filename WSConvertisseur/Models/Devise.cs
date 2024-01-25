using System.ComponentModel.DataAnnotations;

namespace WSConvertisseur.Models
{
    public class Devise
    {
        private int id;
        private string? nomdevise;
        private double taux;

        public Devise(int id, string? nomdevise, double taux)
        {
            Id = id;
            Nomdevise = nomdevise;
            Taux = taux;
        }

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        [Required]
        public string? Nomdevise
        {
            get
            {
                return nomdevise;
            }
            set
            {
                nomdevise = value;
            }
        }
        public double Taux
        {
            get
            {
                return taux;
            }
            set
            {
                taux = value;
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Devise devise &&
                   Id == devise.Id &&
                   Nomdevise == devise.Nomdevise &&
                   Taux == devise.Taux;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nomdevise, Taux);
        }
    }
}
