using Moq;
using Xunit;

namespace Uno.Tests
{
    public class CartesSpecialesTest
    {
        private Mock<IPile> pileMock;
        private Mock<IPioche> piocheMock;

        private Partie partie;

        public CartesSpecialesTest()
        {
            pileMock = new Mock<IPile>();
            piocheMock = new Mock<IPioche>();

            partie = new Partie(pileMock.Object, piocheMock.Object);
        }

        [Fact]
        public void QuandUneCarteChangementDeSensEstJoueeLaPartieChangeSens()
        {
            pileMock.SetupGet(_ => _.DerniereCarte).Returns(new Carte(Valeur.Deux, Couleur.Rouge));
            partie.Sens = Sens.Horaire;

            partie.JouerCarte(new Carte(Valeur.ChangementSens, Couleur.Rouge));

            Assert.Equal(Sens.Antihoraire, partie.Sens);
        }
    }
}