using System.Collections.Generic;
using Moq;
using Uno.Exceptions;
using Uno.Interfaces;
using Xunit;

namespace Uno.Tests
{
    public class DebutPartieTest
    {
        private Mock<ITalon> talonMock;
        private Mock<IPioche> piocheMock;
        private Mock<ITour> tourMock;

        private Partie partie;

        public DebutPartieTest()
        {
            talonMock = new Mock<ITalon>();
            piocheMock = new Mock<IPioche>();
            tourMock = new Mock<ITour>();

            partie = new Partie(talonMock.Object, piocheMock.Object, tourMock.Object);
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

            var listeEvenement = new List<IEnumerable<Joueur>>();
            partie.PartieCommencee += (joueurs) =>
            {
                listeEvenement.Add(joueurs);
            };

            partie.CommencerPartie();

            Assert.Single(listeEvenement);
        }
    }
}