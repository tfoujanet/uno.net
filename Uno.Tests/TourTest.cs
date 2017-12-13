using Moq;
using Xunit;

namespace Uno.Tests
{
    public class TourTest
    {
        private readonly Mock<IPartie> partieMock;
        private readonly Tour tour;

        public TourTest()
        {
            partieMock = new Mock<IPartie>();
            tour = new Tour(partieMock.Object);
        }

        [Fact]
        public void QuandUneCarteChangementSensEstJoueeLeTourChangeDeSens()
        {
            partieMock.Raise(partie => partie.CarteJouee -= null, new Joueur("Joueur 1"), new Carte(Valeur.ChangementSens, Couleur.Rouge));

            Assert.Equal(Sens.Antihoraire, tour.Sens);
        }
    }
}