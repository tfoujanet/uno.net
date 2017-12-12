using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Uno;
using Uno.Helpers;

namespace Uno.MsTests
{

    [TestClass]
    public class PartieTest
    {
        private Mock<IPile> pileMock;
        private Mock<IPioche> piocheMock;
        private Mock<ITour> tourMock;
        private readonly Partie partie;

        public PartieTest()
        {
            pileMock = new Mock<IPile>();
            piocheMock = new Mock<IPioche>();
            tourMock = new Mock<ITour>();

            partie = new Partie(pileMock.Object, piocheMock.Object, tourMock.Object);
        }

        [TestMethod]
        public void QuandLaPartieCommenceLesJoueursOntSeptCartes()
        {
            partie.Joueurs.AddRange(new []
            {
                new Joueur("Joueur 1"),
                new Joueur("Joueur 2")
            });
            partie.CommencerPartie();

            var joueur1 = partie.Joueurs[0];
            var joueur2 = partie.Joueurs[1];

            CollectionAssert.Equals(7, joueur1.Main.Count);
            CollectionAssert.Equals(7, joueur2.Main.Count);
        }
    }
}