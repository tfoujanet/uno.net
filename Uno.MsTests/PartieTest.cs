using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Uno;
using Uno.Exceptions;
using Uno.Helpers;
using Uno.Interfaces;

namespace Uno.MsTests
{
    [TestClass]
    public class PartieTest
    {
        private Mock<ITalon> talonMock;
        private Mock<IPioche> piocheMock;
        private Mock<ITour> tourMock;
        private readonly Partie partie;

        public PartieTest()
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
        }

        [TestMethod]
        public void QuandLaPartieCommenceLesJoueursOntSeptCartes()
        {            
            partie.CommencerPartie();

            var joueur1 = partie.Joueurs[0];
            var joueur2 = partie.Joueurs[1];

            CollectionAssert.Equals(7, joueur1.Main.Count);
            CollectionAssert.Equals(7, joueur2.Main.Count);
        }

        [TestMethod]
        public void UnJoueurNePeutPasJouerSiCeNestPasSonTour()
        {
            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 1"));

            Assert.ThrowsException<MauvaisJoueurDeJouerException>(() => partie.JouerCarte(new Joueur("Joueur 2"), new Carte(Valeur.Deux, Couleur.Jaune)));
        }

        [TestMethod]
        public void UnJoueurNePeutPasJouerUneCarteQuilNaPas()
        {
            partie.Joueurs[0].Main.AddRange(new []
            {
                new Carte(Valeur.Deux, Couleur.Rouge),
                new Carte(Valeur.ChangementSens, Couleur.Jaune),
                new Carte(Valeur.Cinq, Couleur.Bleu)
            });

            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 1"));

            Assert.ThrowsException<JoueurNePossedePasLaCarteException>(() => partie.JouerCarte(new Joueur("Joueur 1"), new Carte(Valeur.Zero, Couleur.Rouge)));            
        }        

        [TestMethod]
        public void UneCarteJoueeParUnJoueurNeDoitPlusEtreDansSaMain()
        {
            partie.Joueurs[0].Main.AddRange(new []
            {
                new Carte(Valeur.Deux, Couleur.Rouge),
                new Carte(Valeur.ChangementSens, Couleur.Jaune),
                new Carte(Valeur.Cinq, Couleur.Bleu)
            });

            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 1"));
            talonMock.SetupGet(_ => _.DerniereCarte).Returns(new Carte(Valeur.Cinq, Couleur.Rouge));

            partie.JouerCarte(new Joueur("Joueur 1"), new Carte(Valeur.Cinq, Couleur.Bleu));

            CollectionAssert.DoesNotContain(partie.Joueurs[0].Main, new Carte(Valeur.Cinq, Couleur.Bleu));
        }

        [TestMethod]
        public void UnJoueurPeutPiocherUneCarteLorsqueCestSonTour()
        {
            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 1"));
            talonMock.SetupGet(_ => _.DerniereCarte).Returns(new Carte(Valeur.Trois, Couleur.Vert));
            piocheMock.Setup(_ => _.TirerCarte()).Returns(new Carte(Valeur.Huit, Couleur.Jaune));

            partie.PiocherCarte(new Joueur("Joueur 1"));

            Assert.AreEqual(1, partie.Joueurs[0].Main.Count); 
        }

        [TestMethod]
        public void UnJoueurNePeutPasPiocherUneCarteLorsqueCeNestPasSonTour()
        {
            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 1"));
            piocheMock.Setup(_ => _.TirerCarte()).Returns(new Carte(Valeur.Huit, Couleur.Jaune));

            Assert.ThrowsException<MauvaisJoueurDeJouerException>(() => partie.PiocherCarte(new Joueur("Joueur 2")));
        }

        [TestMethod]
        public void QuandUnJoueurPiocheUneCarteLaCarteVientDansSaMain()
        {
            partie.Joueurs[0].Main.AddRange(new []
            {
                new Carte(Valeur.Deux, Couleur.Rouge),
                new Carte(Valeur.ChangementSens, Couleur.Jaune),
                new Carte(Valeur.Cinq, Couleur.Bleu)
            });

            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 1"));
            talonMock.SetupGet(_ => _.DerniereCarte).Returns(new Carte(Valeur.Deux, Couleur.Rouge));
            piocheMock.Setup(_ => _.TirerCarte()).Returns(new Carte(Valeur.Huit, Couleur.Jaune));

            partie.PiocherCarte(new Joueur("Joueur 1"));

            CollectionAssert.Contains(partie.Joueurs[0].Main, new Carte(Valeur.Huit, Couleur.Jaune));
            Assert.AreEqual(4, partie.Joueurs[0].Main.Count);
        }

        [TestMethod]
        public void UnJoueurNePeutPasChoisirDeCouleurSiCeNestPasPossible()
        {
            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 1"));
            talonMock.SetupGet(_ => _.CouleurJeu).Returns(Couleur.Bleu);

            Assert.ThrowsException<CouleurDeJeuDejaChoisieException>(() => partie.ChoisirCouleur(new Joueur("Joueur 1"), Couleur.Rouge));
        }

        [TestMethod]
        public void UnJoueurNePeutPasChoisirDeCouleurSilNaPasPoseDeCarteNoire()
        {
            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 3"));
            talonMock.SetupGet(_ => _.CouleurJeu).Returns((Couleur?)null);
            talonMock.SetupGet(_ => _.JoueurChoixCouleur).Returns(new Joueur("Joueur 2"));

            Assert.ThrowsException<MauvaisJoueurDeJouerException>(() => partie.ChoisirCouleur(new Joueur("Joueur 1"), Couleur.Rouge));
        }

        [TestMethod]
        public void LaCouleurNoireNePeutJamaisEtreChoisie()
        {
            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 3"));
            talonMock.SetupGet(_ => _.CouleurJeu).Returns((Couleur?)null);
            talonMock.SetupGet(_ => _.JoueurChoixCouleur).Returns(new Joueur("Joueur 1"));

            Assert.ThrowsException<MauvaiseCouleurChoisieException>(() => partie.ChoisirCouleur(new Joueur("Joueur 1"), Couleur.Noir));
        }

        [TestMethod]
        public void LaCartePiocheeEstJoueeSiEllePeutLetre()
        {
            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 1"));
            talonMock.SetupGet( _=> _.DerniereCarte).Returns(new Carte(Valeur.Huit, Couleur.Bleu));
            piocheMock.Setup(_ => _.TirerCarte()).Returns(new Carte(Valeur.Huit, Couleur.Rouge));

            Tuple<string, Carte> carteJouee = null;
            partie.CarteJouee += (joueur, carte) =>
            {
                carteJouee = new Tuple<string, Carte>(joueur.Nom, carte);
            };

            partie.PiocherCarte(new Joueur("Joueur 1"));

            Assert.IsNotNull(carteJouee);
            Assert.AreEqual("Joueur 1", carteJouee.Item1);
            Assert.AreEqual(new Carte(Valeur.Huit, Couleur.Rouge), carteJouee.Item2);
        }

        [TestMethod]
        public void UnJoueurNePeutPasJouerSiLaCouleurNaPasEteChoisie()
        {
            partie.Joueurs[1].Main.Add(new Carte(Valeur.Neuf, Couleur.Bleu));
            talonMock.SetupGet(_ => _.DerniereCarte).Returns(new Carte(Valeur.Joker, Couleur.Noir));
            talonMock.SetupGet(_ => _.CouleurJeu).Returns((Couleur?)null);
            talonMock.SetupGet(_ => _.JoueurChoixCouleur).Returns(new Joueur("Joueur 1"));

            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 2"));

            Assert.ThrowsException<CouleurDeJeuPasEncoreChoisieException>(() => partie.JouerCarte(new Joueur("Joueur 2"), new Carte(Valeur.Neuf, Couleur.Bleu)));
        }

        [TestMethod]
        public void UnJoueurPeutJouerDeLaBonneCouleurChoisie()
        {
            partie.Joueurs[1].Main.Add(new Carte(Valeur.Neuf, Couleur.Bleu));
            talonMock.SetupGet(_ => _.DerniereCarte).Returns(new Carte(Valeur.Joker, Couleur.Noir));
            talonMock.SetupGet(_ => _.CouleurJeu).Returns(Couleur.Bleu);
            talonMock.SetupGet(_ => _.JoueurChoixCouleur).Returns((Joueur)null);

            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 2"));

            Tuple<string, Carte> carteJouee = null;
            partie.CarteJouee += (joueur, carte) =>
            {
                carteJouee = new Tuple<string, Carte>(joueur.Nom, carte);
            };

            partie.JouerCarte(new Joueur("Joueur 2"), new Carte(Valeur.Neuf, Couleur.Bleu));

            Assert.IsNotNull(carteJouee);
            Assert.AreEqual("Joueur 2", carteJouee.Item1);
            Assert.AreEqual(new Carte(Valeur.Neuf, Couleur.Bleu), carteJouee.Item2);
        }

        [TestMethod]
        public void QuandUneCouleurEstChoisieLeJoueurNePeutPasJouerUneAutreCouleur()
        {
            partie.Joueurs[1].Main.Add(new Carte(Valeur.Neuf, Couleur.Jaune));
            talonMock.SetupGet(_ => _.DerniereCarte).Returns(new Carte(Valeur.Joker, Couleur.Noir));
            talonMock.SetupGet(_ => _.CouleurJeu).Returns(Couleur.Bleu);
            talonMock.SetupGet(_ => _.JoueurChoixCouleur).Returns((Joueur)null);

            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 2"));

            Tuple<string, Carte> carteJouee = null;
            partie.CarteJouee += (joueur, carte) =>
            {
                carteJouee = new Tuple<string, Carte>(joueur.Nom, carte);
            };

            Assert.ThrowsException<MauvaiseCarteJoueeException>(() => partie.JouerCarte(new Joueur("Joueur 2"), new Carte(Valeur.Neuf, Couleur.Jaune)));
        }
    }
}