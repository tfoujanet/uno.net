using System;
using System.Linq;
using Uno;
using Uno.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Uno.MsTests
{
    [TestClass]
    public class PartieTest
    {
        private Mock<IPile> pileMock;
        private readonly Partie partie;

        public PartieTest()
        {

            pileMock = new Mock<IPile>();

            partie = new Partie(pileMock.Object);
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