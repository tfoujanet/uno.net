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
    }
}