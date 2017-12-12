using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Uno.MsTests
{
    [TestClass]
    public class ValeursCarteTest
    {
        private Mock<IPile> pileMock;
        private Mock<IPioche> piocheMock;
        private Mock<ITour> tourMock;
        private readonly Partie partie;

        public ValeursCarteTest()
        {
            pileMock = new Mock<IPile>();
            piocheMock = new Mock<IPioche>();
            tourMock = new Mock<ITour>();

            partie = new Partie(pileMock.Object, piocheMock.Object, tourMock.Object);
            partie.Joueurs.AddRange(new []
            {
                new Joueur("Joueur 1"),
                new Joueur("Joueur 2")
            });
            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 1"));
        }

        [TestMethod]
        public void OnPeutJouerUneCarteDeMemeCouleur()
        {
            pileMock.SetupGet(_ => _.DerniereCarte).Returns(new Carte(Valeur.Deux, Couleur.Rouge));
            var listeCarte = new List<Carte>();

            partie.CarteJouee += (carte) => {
                listeCarte.Add(carte);
            };      

            partie.JouerCarte(new Joueur("Joueur 1"), new Carte(Valeur.Quatre, Couleur.Rouge));

            CollectionAssert.Equals(1, listeCarte.Count);
        }

        [TestMethod]
        public void OnPeutJouerUneCarteDeMemeNumero()
        {
            pileMock.SetupGet(_ => _.DerniereCarte).Returns(new Carte(Valeur.Deux, Couleur.Rouge));
            var listeCarte = new List<Carte>();

            partie.CarteJouee += (carte) => {
                listeCarte.Add(carte);
            };      

            partie.JouerCarte(new Joueur("Joueur 1"), new Carte(Valeur.Deux, Couleur.Vert));

            CollectionAssert.Equals(1, listeCarte.Count);
        }

        [TestMethod]
        public void OnPeutJouerUneCarteJokerSurNimporteQuelleCouleur()
        {
            pileMock.SetupGet(_ => _.DerniereCarte).Returns(new Carte(Valeur.Deux, Couleur.Rouge));
            var listeCarte = new List<Carte>();

            partie.CarteJouee += (carte) => {
                listeCarte.Add(carte);
            };      

            partie.JouerCarte(new Joueur("Joueur 1"), new Carte(Valeur.Joker, Couleur.Noir));

            CollectionAssert.Equals(1, listeCarte.Count);
        }

        [TestMethod]
        public void OnPeutJouerUneCarteSuperJokerSurNimporteQuelleCouleur()
        {
            pileMock.SetupGet(_ => _.DerniereCarte).Returns(new Carte(Valeur.Deux, Couleur.Rouge));
            var listeCarte = new List<Carte>();

            partie.CarteJouee += (carte) => {
                listeCarte.Add(carte);
            };      

            partie.JouerCarte(new Joueur("Joueur 1"), new Carte(Valeur.Plus4, Couleur.Noir));

            CollectionAssert.Equals(1, listeCarte.Count);
        }
    }
}