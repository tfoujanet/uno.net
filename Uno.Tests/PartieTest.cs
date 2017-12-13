using System.Collections.Generic;
using Moq;
using Uno.Exceptions;
using Xunit;

namespace Uno.Tests
{
    public class PartieTest
    {
        private Mock<IPile> pileMock;
        private Mock<IPioche> piocheMock;
        private Mock<ITour> tourMock;

        private Partie partie;

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

        [Fact]
        public void QuandLaPartieCommenceLesJoueursOntSeptCartes()
        {
            partie.CommencerPartie();

            Assert.All(partie.Joueurs, _ => Assert.Equal(7, _.Main.Count));
        }

        [Fact]
        public void UnJoueurNePeutPasJouerSiCeNestPasSonTour()
        {
            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 1"));

            Assert.Throws<MauvaisJoueurDeJouerException>(() => partie.JouerCarte(new Joueur("Joueur 2"), new Carte(Valeur.Deux, Couleur.Jaune)));
        }

        [Fact]
        public void UnJoueurNePeutPasJouerUneCarteQuilNaPas()
        {
            partie.Joueurs[0].Main.AddRange(new []
            {
                new Carte(Valeur.Deux, Couleur.Rouge),
                    new Carte(Valeur.ChangementSens, Couleur.Jaune),
                    new Carte(Valeur.Cinq, Couleur.Bleu)
            });

            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 1"));

            Assert.Throws<JoueurNePossedePasLaCarteException>(() => partie.JouerCarte(new Joueur("Joueur 1"), new Carte(Valeur.Zero, Couleur.Rouge)));
        }

        [Fact]
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

            Assert.DoesNotContain(new Carte(Valeur.Cinq, Couleur.Bleu), partie.Joueurs[0].Main);
        }

        [Fact]
        public void UnJoueurPeutPiocherUneCarteLorsqueCestSonTour()
        {
            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 1"));
            piocheMock.Setup(_ => _.TirerCarte()).Returns(new Carte(Valeur.Huit, Couleur.Jaune));

            partie.PiocherCarte(new Joueur("Joueur 1"));
        }

        [Fact]
        public void UnJoueurNePeutPasPiocherUneCarteLorsqueCeNestPasSonTour()
        {
            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 1"));
            piocheMock.Setup(_ => _.TirerCarte()).Returns(new Carte(Valeur.Huit, Couleur.Jaune));

            Assert.Throws<MauvaisJoueurDeJouerException>(() => partie.PiocherCarte(new Joueur("Joueur 2")));
        }

        [Fact]
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

            Assert.Contains(new Carte(Valeur.Huit, Couleur.Jaune), partie.Joueurs[0].Main);
            Assert.Equal(4, partie.Joueurs[0].Main.Count);
        }

        [Fact]
        public void UnJoueurNePeutPasChoisirDeCouleurSiCeNestPasPossible()
        {
            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 1"));
            pileMock.SetupGet(_ => _.CouleurJeu).Returns(Couleur.Bleu);

            Assert.Throws<CouleurDeJeuDejaChoisieException>(() => partie.ChoisirCouleur(new Joueur("Joueur 1"), Couleur.Rouge));
        }

        [Fact]
        public void UnJoueurNePeutPasChoisirDeCouleurSilNaPasPoseDeCarteNoire()
        {
            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 3"));
            pileMock.SetupGet(_ => _.CouleurJeu).Returns((Couleur?)null);
            pileMock.SetupGet(_ => _.JoueurChoixCouleur).Returns(new Joueur("Joueur 2"));

            Assert.Throws<MauvaisJoueurDeJouerException>(() => partie.ChoisirCouleur(new Joueur("Joueur 1"), Couleur.Rouge));
        }

        [Fact]
        public void LaCouleurNoireNePeutJamaisEtreChoisie()
        {
            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 3"));
            pileMock.SetupGet(_ => _.CouleurJeu).Returns((Couleur?)null);
            pileMock.SetupGet(_ => _.JoueurChoixCouleur).Returns(new Joueur("Joueur 1"));

            Assert.Throws<MauvaiseCouleurChoisieException>(() => partie.ChoisirCouleur(new Joueur("Joueur 1"), Couleur.Noir));
        }
    }
}