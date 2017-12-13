using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Uno;
using Uno.Exceptions;
using Uno.Helpers;

namespace Uno.MsTests
{

    [TestClass]
    public class PartieTest
    {
        private Mock<IPile> pileMock;
        private Mock<IPioche> piocheMock;
        private Mock<ITour> tourMock;
        private readonly Partie partie;

        public PartieTest()
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
            pileMock.SetupGet(_ => _.DerniereCarte).Returns(new Carte(Valeur.Cinq, Couleur.Rouge));

            partie.JouerCarte(new Joueur("Joueur 1"), new Carte(Valeur.Cinq, Couleur.Bleu));

            CollectionAssert.DoesNotContain(partie.Joueurs[0].Main, new Carte(Valeur.Cinq, Couleur.Bleu));
        }

        [TestMethod]
        public void UnJoueurPeutPiocherUneCarteLorsqueCestSonTour()
        {
            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 1"));
            piocheMock.Setup(_ => _.TirerCarte()).Returns(new Carte(Valeur.Huit, Couleur.Jaune));

            partie.PiocherCarte(new Joueur("Joueur 1"));
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
            piocheMock.Setup(_ => _.TirerCarte()).Returns(new Carte(Valeur.Huit, Couleur.Jaune));

            partie.PiocherCarte(new Joueur("Joueur 1"));

            CollectionAssert.Contains(partie.Joueurs[0].Main, new Carte(Valeur.Huit, Couleur.Jaune));
            Assert.AreEqual(4, partie.Joueurs[0].Main.Count);
        }
    }
}