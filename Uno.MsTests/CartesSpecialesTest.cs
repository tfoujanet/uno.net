using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Uno.MsTests
{
    [TestClass]
    public class CartesSpecialesTest
    {
        private Mock<IPile> pileMock;
        private Mock<IPioche> piocheMock;
        private readonly Partie partie;

        public CartesSpecialesTest()
        {
            pileMock = new Mock<IPile>();
            piocheMock = new Mock<IPioche>();

            partie = new Partie(pileMock.Object, piocheMock.Object);
        }

        [TestMethod]
        public void QuandUneCarteChangementDeSensEstJoueeLaPartieChangeSens()
        {
            pileMock.SetupGet(_ => _.DerniereCarte).Returns(new Carte(Valeur.Deux, Couleur.Rouge));
            partie.Sens = Sens.Horaire;

            partie.JouerCarte(new Carte(Valeur.ChangementSens, Couleur.Rouge));

            Assert.AreEqual(Sens.Antihoraire, partie.Sens);
        }
    }
}