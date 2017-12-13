using System.Collections.Generic;
using Moq;
using Uno.Exceptions;
using Xunit;

namespace Uno.Tests
{
    public class JoueursTests
    {
        private Mock<ITalon> pileMock;
        private Mock<IPioche> piocheMock;
        private Mock<ITour> tourMock;

        private Partie partie;

        public JoueursTests()
        {
            pileMock = new Mock<ITalon>();
            piocheMock = new Mock<IPioche>();
            tourMock = new Mock<ITour>();

            partie = new Partie(pileMock.Object, piocheMock.Object, tourMock.Object);
        }

        [Fact]
        public void UnJoueurPeutRejoindreLaPartie()
        {
            var listeJoueurs = new List<Joueur>();
            partie.JoueurAjoute += (joueur) => {
                listeJoueurs.Add(joueur);
            };

            partie.AjouterJoueur(new Joueur("joueur"));

            Assert.Single(listeJoueurs);            
        }

        [Fact]
        public void UnJoueurNePeutPasRejoindreDeuxFoisLaPartie()
        {
            partie.Joueurs.Add(new Joueur("joueur"));

            Assert.Throws<JoueurDejaAjouteException>(() => partie.AjouterJoueur(new Joueur("joueur")));
        }
    }
}