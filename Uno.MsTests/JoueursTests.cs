using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Uno.Exceptions;

namespace Uno.MsTests
{
    [TestClass]
    public class JoueursTests
    {
        private Mock<IPile> pileMock;
        private readonly Partie partie;

        public JoueursTests() 
        {            
            pileMock = new Mock<IPile>();

            partie = new Partie(pileMock.Object);
        }

        [TestMethod]
        public void UnJoueurPeutRejoindreLaPartie()
        {
            var listeJoueurs = new List<Joueur>();
            partie.JoueurAjoute += (joueur) => {
                listeJoueurs.Add(joueur);
            };

            partie.AjouterJoueur(new Joueur("joueur"));

            CollectionAssert.Equals(1, listeJoueurs.Count);
        }

        [TestMethod]
        public void UnJoueurNePeutPasRejoindreDeuxFoisLaPartie()
        {
            partie.Joueurs.Add(new Joueur("joueur"));

            Assert.ThrowsException<JoueurDejaAjouteException>(() => partie.AjouterJoueur(new Joueur("joueur")));
        }
    }
}