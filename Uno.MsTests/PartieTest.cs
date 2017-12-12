using System;
using System.Linq;
using Uno;
using Uno.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Uno.MsTests
{
    [TestClass]
    public class PartieTest
    {
        private Mock<IPile> pileMock;
        private readonly Partie partie;

        public PartieTest()
        {

            pileMock = new Mock<IPile>();

            partie = new Partie(pileMock.Object);
        }

        [TestMethod]
        public void OnPeutJouerUneCarteDeMemeCouleur()
        {
            pileMock.SetupGet(_ => _.DerniereCarte).Returns(new Carte(Valeur.Deux, Couleur.Rouge));
            var listeCarte = new List<Carte>();

            partie.CarteJouee += (carte) => {
                listeCarte.Add(carte);
            };      

            partie.JouerCarte(new Carte(Valeur.Quatre, Couleur.Rouge));

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

            partie.JouerCarte(new Carte(Valeur.Deux, Couleur.Vert));

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

            partie.JouerCarte(new Carte(Valeur.Joker, Couleur.Noir));

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

            partie.JouerCarte(new Carte(Valeur.Plus4, Couleur.Noir));

            CollectionAssert.Equals(1, listeCarte.Count);
        }
    }
}