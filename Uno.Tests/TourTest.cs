using System;
using System.Collections.Generic;
using Moq;
using Xunit;

namespace Uno.Tests
{
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

        [Fact]
        public void QuandUneCarteChangementSensEstJoueeLeTourChangeDeSens()
        {
            partieMock.Raise(partie => partie.CarteJouee -= null, new Joueur("Joueur 1"), new Carte(Valeur.ChangementSens, Couleur.Rouge));

            Assert.Equal(Sens.Antihoraire, tour.Sens);
        }

        [Fact]
        public void QuandUneCarteEstJoueeLeJoueurSuivantPeutJouer()
        {            
            partieMock.Raise(partie => partie.CarteJouee -= null, new Joueur("Joueur 1"), new Carte(Valeur.Cinq, Couleur.Rouge));

            Assert.Equal("Joueur 2", tour.JoueurDuTour.Nom);
        }

        [Fact]
        public void QuandUneCartePasseTourEstJoueeLeJoueurSuivantNePeutPasJouer()
        {
            partieMock.Raise(partie => partie.CarteJouee -= null, new Joueur("Joueur 1"), new Carte(Valeur.PasseTour, Couleur.Rouge));

            Assert.Equal("Joueur 3", tour.JoueurDuTour.Nom);
        }

        [Fact]
        public void QuandUneCartePlusDeuxEstJoueeLeJoueurSuivantNePeutPasJouer()
        {
            partieMock.Raise(partie => partie.CarteJouee -= null, new Joueur("Joueur 1"), new Carte(Valeur.Plus2, Couleur.Rouge));

            Assert.Equal("Joueur 3", tour.JoueurDuTour.Nom);
        }        

        [Fact]
        public void QuandUneCartePlusQuatreEstJoueeLeJoueurSuivantNePeutPasJouer()
        {
            partieMock.Raise(partie => partie.CarteJouee -= null, new Joueur("Joueur 1"), new Carte(Valeur.Plus4, Couleur.Rouge));

            Assert.Equal("Joueur 3", tour.JoueurDuTour.Nom);
        }

        [Fact]
        public void QuandUneCartePlusQuatreEstJoueeLeJoueurSuivantDoitPiocherQuatreCartes()
        {
            Tuple<string, int> piochePourJoueur = null;
            tour.JoueurDoitPiocher += (joueur, nombre) => 
            {
                piochePourJoueur = new Tuple<string, int>(joueur.Nom, nombre);
            };

            partieMock.Raise(partie => partie.CarteJouee -= null, new Joueur("Joueur 1"), new Carte(Valeur.Plus4, Couleur.Noir));

            Assert.NotNull(piochePourJoueur);
            Assert.Equal("Joueur 2", piochePourJoueur.Item1);
            Assert.Equal(4, piochePourJoueur.Item2);
        }

        [Fact]
        public void QuandUneCartePlusDeuxEstJoueeLeJoueurSuivantDoitPiocherDeuxCartes()
        {
            Tuple<string, int> piochePourJoueur = null;
            tour.JoueurDoitPiocher += (joueur, nombre) => 
            {
                piochePourJoueur = new Tuple<string, int>(joueur.Nom, nombre);
            };

            partieMock.Raise(partie => partie.CarteJouee -= null, new Joueur("Joueur 1"), new Carte(Valeur.Plus2, Couleur.Rouge));

            Assert.NotNull(piochePourJoueur);
            Assert.Equal("Joueur 2", piochePourJoueur.Item1);
            Assert.Equal(2, piochePourJoueur.Item2);
        }
    }
}