using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Uno.Interfaces;
using Uno.ValueObjects;

namespace Uno.MsTests
{
    [TestClass]
    public class ValeursCarteTest
    {
        private Mock<ITalon> talonMock;
        private Mock<IPioche> piocheMock;
        private Mock<ITour> tourMock;
        private readonly Partie partie;

        public ValeursCarteTest()
        {
            talonMock = new Mock<ITalon>();
            piocheMock = new Mock<IPioche>();
            tourMock = new Mock<ITour>();

            partie = new Partie(talonMock.Object, piocheMock.Object, tourMock.Object);
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

        [TestMethod]
        public void OnPeutJouerUneCarteDeMemeCouleur()
        {
            talonMock.SetupGet(_ => _.DerniereCarte).Returns(new Carte(Valeur.Deux, Couleur.Rouge));
            var listeCarte = new List<Carte>();

            partie.CarteJouee += (joueur, carte) => {
                listeCarte.Add(carte);
            };      

            partie.JouerCarte(new Joueur("Joueur 1"), new Carte(Valeur.Quatre, Couleur.Rouge));

            CollectionAssert.Equals(1, listeCarte.Count);
        }

        [TestMethod]
        public void OnPeutJouerUneCarteDeMemeNumero()
        {
            talonMock.SetupGet(_ => _.DerniereCarte).Returns(new Carte(Valeur.Deux, Couleur.Rouge));
            var listeCarte = new List<Carte>();

            partie.CarteJouee += (joueur, carte) => {
                listeCarte.Add(carte);
            };      

            partie.JouerCarte(new Joueur("Joueur 1"), new Carte(Valeur.Deux, Couleur.Vert));

            CollectionAssert.Equals(1, listeCarte.Count);
        }

        [TestMethod]
        public void OnPeutJouerUneCarteJokerSurNimporteQuelleCouleur()
        {
            talonMock.SetupGet(_ => _.DerniereCarte).Returns(new Carte(Valeur.Deux, Couleur.Rouge));
            var listeCarte = new List<Carte>();

            partie.CarteJouee += (joueur, carte) => {
                listeCarte.Add(carte);
            };      

            partie.JouerCarte(new Joueur("Joueur 1"), new Carte(Valeur.Joker, Couleur.Noir));

            CollectionAssert.Equals(1, listeCarte.Count);
        }

        [TestMethod]
        public void OnPeutJouerUneCarteSuperJokerSurNimporteQuelleCouleur()
        {
            talonMock.SetupGet(_ => _.DerniereCarte).Returns(new Carte(Valeur.Deux, Couleur.Rouge));
            var listeCarte = new List<Carte>();

            partie.CarteJouee += (joueur, carte) => {
                listeCarte.Add(carte);
            };      

            partie.JouerCarte(new Joueur("Joueur 1"), new Carte(Valeur.Plus4, Couleur.Noir));

            CollectionAssert.Equals(1, listeCarte.Count);
        }
    }
}