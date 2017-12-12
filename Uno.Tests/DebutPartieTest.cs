using System.Collections.Generic;
using Moq;
using Uno.Exceptions;
using Xunit;

namespace Uno.Tests
{
    public class DebutPartieTest
    {
        private Mock<IPile> pileMock;
        private Mock<IPioche> piocheMock;

        private Partie partie;

        public DebutPartieTest()
        {
            pileMock = new Mock<IPile>();
            piocheMock = new Mock<IPioche>();

            partie = new Partie(pileMock.Object, piocheMock.Object);
        }

        [Fact]
        public void UnePartieNePeutPasCommencerSiIlYaMoinsDeDeuxJoueurs()
        {
            Assert.Throws<PasAssezDeJoueurException>(() => partie.CommencerPartie());
        }

        [Fact]
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

            Assert.Single(listeEvenement);
        }
    }
}