namespace Uno
{
    public class Carte
    {
        public Carte(Valeur valeur, Couleur couleur)
        {
            Valeur = valeur;
            Couleur = couleur;
        }

        public Valeur Valeur { get; private set; }

        public Couleur Couleur { get; private set; }
    }
}