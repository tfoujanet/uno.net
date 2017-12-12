using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Uno.Exceptions;

namespace Uno.MsTests
{

    [TestClass]
    public class DebutPartieTest
    {
        private Mock<IPile> pileMock;
        private Mock<IPioche> piocheMock;
        private readonly Partie partie;

        public DebutPartieTest()
        {
            pileMock = new Mock<IPile>();
            piocheMock = new Mock<IPioche>();

            partie = new Partie(pileMock.Object, piocheMock.Object);
        }

        [TestMethod]
        public void UnePartieNePeutPasCommencerSiIlYaMoinsDeDeuxJoueurs()
        {
            Assert.ThrowsException<PasAssezDeJoueurException>(() => partie.CommencerPartie());
        }

        [TestMethod]
        public void UnePartiePeutCommencerSiPlusDeDeuxJoueursSontInscrits()
        {
            partie.Joueurs.AddRange(new List<Joueur>
            {
                new Joueur("joueur 1"),
                new Joueur("joueur 2")
            });

            var listeEvenement = new List<object>();
            partie.PartieCommencee += () =>
            {
                listeEvenement.Add(new object());
            };

            partie.CommencerPartie();

            CollectionAssert.Equals(1, listeEvenement.Count);
        }
    }
}