using System;

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

        public override int GetHashCode()        
        {            
            return (int)Valeur ^ (int)Couleur;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (GetType() != obj.GetType()) return false;

            var carte = (Carte)obj;

            return Valeur == carte.Valeur && Couleur == carte.Couleur;
        }

        public static bool operator ==(Carte carte1, Carte carte2)
        {
            return Object.Equals(carte1, carte2);
        }

        public static bool operator !=(Carte carte1, Carte carte2)
        {
            return !Object.Equals(carte1, carte2);
        }
    }
}