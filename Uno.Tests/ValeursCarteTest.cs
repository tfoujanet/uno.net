using System.Collections.Generic;
using Moq;
using Xunit;

namespace Uno.Tests
{
    public class ValeursCarteTest
    {
        private Mock<ITalon> pileMock;
        private Mock<IPioche> piocheMock;
        private Mock<ITour> tourMock;

        private Partie partie;

        public ValeursCarteTest()
        {
            pileMock = new Mock<ITalon>();
            piocheMock = new Mock<IPioche>();
            tourMock = new Mock<ITour>();

            partie = new Partie(pileMock.Object, piocheMock.Object, tourMock.Object);
            partie.Joueurs.AddRange(new []
            {
                new Joueur("Joueur 1"),
                new Joueur("Joueur 2")
            });
            partie.Joueurs[0].Main.AddRange(new []
            {
                new Carte(Valeur.Quatre, Couleur.Rouge),
                new Carte(Valeur.Deux, Couleur.Vert),
                new Carte(Valeur.Joker, Couleur.Noir),
                new Carte(Valeur.Plus4, Couleur.Noir)
            });
            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 1"));            
        }

        [Fact]
        public void OnPeutJouerUneCarteDeMemeCouleur()
        {
            pileMock.SetupGet(_ => _.DerniereCarte).Returns(new Carte(Valeur.Deux, Couleur.Rouge));
            var listeCarte = new List<Carte>();

            partie.CarteJouee += (joueur, carte) =>
            {
                listeCarte.Add(carte);
            };

            partie.JouerCarte(new Joueur("Joueur 1"), new Carte(Valeur.Quatre, Couleur.Rouge));

            Assert.Single(listeCarte);
        }

        [Fact]
        public void OnPeutJouerUneCarteDeMemeNumero()
        {
            pileMock.SetupGet(_ => _.DerniereCarte).Returns(new Carte(Valeur.Deux, Couleur.Rouge));
            var listeCarte = new List<Carte>();

            partie.CarteJouee += (joueur, carte) =>
            {
                listeCarte.Add(carte);
            };

            partie.JouerCarte(new Joueur("Joueur 1"), new Carte(Valeur.Deux, Couleur.Vert));

            Assert.Single(listeCarte);
        }

        [Fact]
        public void OnPeutJouerUneCarteJokerSurNimporteQuelleCouleur()
        {
            pileMock.SetupGet(_ => _.DerniereCarte).Returns(new Carte(Valeur.Deux, Couleur.Rouge));
            var listeCarte = new List<Carte>();

            partie.CarteJouee += (joueur, carte) =>
            {
                listeCarte.Add(carte);
            };

            partie.JouerCarte(new Joueur("Joueur 1"), new Carte(Valeur.Joker, Couleur.Noir));

            Assert.Single(listeCarte);
        }

        [Fact]
        public void OnPeutJouerUneCarteSuperJokerSurNimporteQuelleCouleur()
        {
            pileMock.SetupGet(_ => _.DerniereCarte).Returns(new Carte(Valeur.Deux, Couleur.Rouge));
            var listeCarte = new List<Carte>();

            partie.CarteJouee += (joueur, carte) =>
            {
                listeCarte.Add(carte);
            };

            partie.JouerCarte(new Joueur("Joueur 1"), new Carte(Valeur.Plus4, Couleur.Noir));

            Assert.Single(listeCarte);
        }
    }
}