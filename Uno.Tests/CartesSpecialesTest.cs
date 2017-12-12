using Moq;
using Xunit;

namespace Uno.Tests
{
    public class CartesSpecialesTest
    {
        private Mock<IPile> pileMock;

        private Partie partie;

        public CartesSpecialesTest()
        {
            pileMock = new Mock<IPile>();

            partie = new Partie(pileMock.Object);
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