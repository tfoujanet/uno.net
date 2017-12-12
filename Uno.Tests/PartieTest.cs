using System.Collections.Generic;
using Moq;
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
        }

        [Fact]
        public void QuandLaPartieCommenceLesJoueursOntSeptCartes()
        {
            partie.Joueurs.AddRange(new []
            {
                new Joueur("Joueur 1"),
                new Joueur("Joueur 2")
            });
            partie.CommencerPartie();

            Assert.All(partie.Joueurs, _ => Assert.Equal(7, _.Main.Count));
        }
    }
}