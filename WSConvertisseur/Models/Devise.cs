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
    }
}
