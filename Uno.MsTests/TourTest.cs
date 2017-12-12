using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Uno.MsTests
{
    [TestClass]
    public class TourTest
    {
        private readonly Mock<IPartie> partieMock;
        private readonly Tour tour;

        public TourTest()
        {
            partieMock = new Mock<IPartie>();
            tour = new Tour(partieMock.Object);
        }

        [TestMethod]
        public void QuandUneCarteChangementSensEstJoueeLeTourChangeDeSens()
        {
            partieMock.Raise(partie => partie.CarteJouee -= null, new Carte(Valeur.ChangementSens, Couleur.Rouge));

            Assert.AreEqual(Sens.Antihoraire, tour.Sens);
        }
    }
}