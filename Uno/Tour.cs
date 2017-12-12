namespace Uno
{
    public interface ITour    
    {
        Sens Sens { get; set; }
    }

    public class Tour : ITour
    {
        public Tour(Partie partie)
        {
            partie.CarteJouee += CarteJouee;

            Sens = Sens.Horaire;
        }

        public Sens Sens { get; set; }

        private void CarteJouee(Carte carte)
        {
            if (carte.Valeur == Valeur.ChangementSens)
                InverserSens();
        }

        private void InverserSens()
        {
            Sens = Sens == Sens.Horaire ? Sens.Antihoraire : Sens.Horaire;
        }
    }
}