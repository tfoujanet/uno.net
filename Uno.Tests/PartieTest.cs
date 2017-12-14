using System;
using System.Collections.Generic;
using Moq;
using Uno.Exceptions;
using Uno.Interfaces;
using Uno.ValueObjects;
using Xunit;

namespace Uno.Tests
{
    public class PartieTest
    {
        private Mock<ITalon> talonMock;
        private Mock<IPioche> piocheMock;
        private Mock<ITour> tourMock;

        private Partie partie;

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
            talonMock.SetupGet(_ => _.DerniereCarte).Returns(new Carte(Valeur.Cinq, Couleur.Rouge));

            partie.JouerCarte(new Joueur("Joueur 1"), new Carte(Valeur.Cinq, Couleur.Bleu));

            Assert.DoesNotContain(new Carte(Valeur.Cinq, Couleur.Bleu), partie.Joueurs[0].Main);
        }

        [Fact]
        public void UnJoueurPeutPiocherUneCarteLorsqueCestSonTour()
        {
            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 1"));
            talonMock.SetupGet(_ => _.DerniereCarte).Returns(new Carte(Valeur.Deux, Couleur.Bleu));
            piocheMock.Setup(_ => _.TirerCarte()).Returns(new Carte(Valeur.Huit, Couleur.Jaune));

            partie.PiocherCarte(new Joueur("Joueur 1"));

            Assert.Single(partie.Joueurs[0].Main);
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
            talonMock.SetupGet(_ => _.DerniereCarte).Returns(new Carte(Valeur.Deux, Couleur.Rouge));
            piocheMock.Setup(_ => _.TirerCarte()).Returns(new Carte(Valeur.Huit, Couleur.Jaune));

            partie.PiocherCarte(new Joueur("Joueur 1"));

            Assert.Contains(new Carte(Valeur.Huit, Couleur.Jaune), partie.Joueurs[0].Main);
            Assert.Equal(4, partie.Joueurs[0].Main.Count);
        }

        [Fact]
        public void UnJoueurNePeutPasChoisirDeCouleurSiCeNestPasPossible()
        {
            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 1"));
            talonMock.SetupGet(_ => _.CouleurJeu).Returns(Couleur.Bleu);

            Assert.Throws<CouleurDeJeuDejaChoisieException>(() => partie.ChoisirCouleur(new Joueur("Joueur 1"), Couleur.Rouge));
        }

        [Fact]
        public void UnJoueurNePeutPasChoisirDeCouleurSilNaPasPoseDeCarteNoire()
        {
            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 3"));
            talonMock.SetupGet(_ => _.CouleurJeu).Returns((Couleur?)null);
            talonMock.SetupGet(_ => _.JoueurChoixCouleur).Returns(new Joueur("Joueur 2"));

            Assert.Throws<MauvaisJoueurDeJouerException>(() => partie.ChoisirCouleur(new Joueur("Joueur 1"), Couleur.Rouge));
        }

        [Fact]
        public void LaCouleurNoireNePeutJamaisEtreChoisie()
        {
            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 3"));
            talonMock.SetupGet(_ => _.CouleurJeu).Returns((Couleur?)null);
            talonMock.SetupGet(_ => _.JoueurChoixCouleur).Returns(new Joueur("Joueur 1"));

            Assert.Throws<MauvaiseCouleurChoisieException>(() => partie.ChoisirCouleur(new Joueur("Joueur 1"), Couleur.Noir));
        }

        [Fact]
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

            Assert.NotNull(carteJouee);
            Assert.Equal("Joueur 1", carteJouee.Item1);
            Assert.Equal(new Carte(Valeur.Huit, Couleur.Rouge), carteJouee.Item2);
        }

        [Fact]
        public void UnJoueurNePeutPasJouerSiLaCouleurNaPasEteChoisie()
        {
            partie.Joueurs[1].Main.Add(new Carte(Valeur.Neuf, Couleur.Bleu));
            talonMock.SetupGet(_ => _.DerniereCarte).Returns(new Carte(Valeur.Joker, Couleur.Noir));
            talonMock.SetupGet(_ => _.CouleurJeu).Returns((Couleur?)null);
            talonMock.SetupGet(_ => _.JoueurChoixCouleur).Returns(new Joueur("Joueur 1"));

            tourMock.SetupGet(_ => _.JoueurDuTour).Returns(new Joueur("Joueur 2"));

            Assert.Throws<CouleurDeJeuPasEncoreChoisieException>(() => partie.JouerCarte(new Joueur("Joueur 2"), new Carte(Valeur.Neuf, Couleur.Bleu)));
        }

        [Fact]
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

            Assert.NotNull(carteJouee);
            Assert.Equal("Joueur 2", carteJouee.Item1);
            Assert.Equal(new Carte(Valeur.Neuf, Couleur.Bleu), carteJouee.Item2);
        }       

        [Fact]
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

            Assert.Throws<MauvaiseCarteJoueeException>(() => partie.JouerCarte(new Joueur("Joueur 2"), new Carte(Valeur.Neuf, Couleur.Jaune)));
        }
    }
}