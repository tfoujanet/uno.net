using System.Collections.Generic;
using Moq;
using Uno.Exceptions;
using Xunit;

namespace Uno.Tests
{
    public class JoueursTests
    {
        private Mock<IPile> pileMock;
        private readonly Partie partie;

        public JoueursTests() 
        {            
            pileMock = new Mock<IPile>();

            partie = new Partie(pileMock.Object);
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