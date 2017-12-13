using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Uno.MsTests
{
    [TestClass]
    public class TourTest
    {
        private readonly Mock<IPartie> partieMock;
        private readonly Tour tour;
        private readonly List<Joueur> listeJoueurs = new List<Joueur>
            {
                new Joueur("Joueur 1"),
                new Joueur("Joueur 2"),
                new Joueur("Joueur 3"),
                new Joueur("Joueur 4")
            };

        public TourTest()
        {
            partieMock = new Mock<IPartie>();
            tour = new Tour(partieMock.Object);
            partieMock.Raise(partie => partie.PartieCommencee -= null, listeJoueurs);
        }

        [TestMethod]
        public void QuandUneCarteChangementSensEstJoueeLeTourChangeDeSens()
        {
            partieMock.Raise(partie => partie.CarteJouee -= null, new Joueur("Joueur 1"), new Carte(Valeur.ChangementSens, Couleur.Rouge));

            Assert.AreEqual(Sens.Antihoraire, tour.Sens);
        }

        [TestMethod]
        public void QuandUneCarteEstJoueeLeJoueurSuivantPeutJouer()
        {
            partieMock.Raise(partie => partie.CarteJouee -= null, new Joueur("Joueur 1"), new Carte(Valeur.Cinq, Couleur.Rouge));

            Assert.AreEqual("Joueur 2", tour.JoueurDuTour.Nom);
        }

        [TestMethod]
        public void QuandUneCartePasseTourEstJoueeLeJoueurSuivantNePeutPasJouer()
        {
            partieMock.Raise(partie => partie.CarteJouee -= null, new Joueur("Joueur 1"), new Carte(Valeur.PasseTour, Couleur.Rouge));

            Assert.AreEqual("Joueur 3", tour.JoueurDuTour.Nom);
        }

        [TestMethod]
        public void QuandUneCartePlusDeuxEstJoueeLeJoueurSuivantNePeutPasJouer()
        {
            partieMock.Raise(partie => partie.CarteJouee -= null, new Joueur("Joueur 1"), new Carte(Valeur.Plus2, Couleur.Rouge));

            Assert.AreEqual("Joueur 3", tour.JoueurDuTour.Nom);
        }

        [TestMethod]
        public void QuandUneCartePlusQuatreEstJoueeLeJoueurSuivantNePeutPasJouer()
        {
            partieMock.Raise(partie => partie.CarteJouee -= null, new Joueur("Joueur 1"), new Carte(Valeur.Plus4, Couleur.Rouge));

            Assert.AreEqual("Joueur 3", tour.JoueurDuTour.Nom);
        }

        [TestMethod]
        public void QuandUneCartePlusQuatreEstJoueeLeJoueurSuivantDoitPiocherQuatreCartes()
        {
            Tuple<string, int> piochePourJoueur = null;
            tour.JoueurDoitPiocher += (joueur, nombre) => 
            {
                piochePourJoueur = new Tuple<string, int>(joueur.Nom, nombre);
            };

            partieMock.Raise(partie => partie.CarteJouee -= null, new Joueur("Joueur 1"), new Carte(Valeur.Plus4, Couleur.Noir));

            Assert.IsNotNull(piochePourJoueur);
            Assert.AreEqual("Joueur 2", piochePourJoueur.Item1);
            Assert.AreEqual(4, piochePourJoueur.Item2);
        }

        [TestMethod]
        public void QuandUneCartePlusDeuxEstJoueeLeJoueurSuivantDoitPiocherDeuxCartes()
        {
            Tuple<string, int> piochePourJoueur = null;
            tour.JoueurDoitPiocher += (joueur, nombre) => 
            {
                piochePourJoueur = new Tuple<string, int>(joueur.Nom, nombre);
            };

            partieMock.Raise(partie => partie.CarteJouee -= null, new Joueur("Joueur 1"), new Carte(Valeur.Plus2, Couleur.Rouge));

            Assert.IsNotNull(piochePourJoueur);
            Assert.AreEqual("Joueur 2", piochePourJoueur.Item1);
            Assert.AreEqual(2, piochePourJoueur.Item2);
        }
    }
}