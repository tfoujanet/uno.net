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
        private Mock<IPioche> piocheMock;
        private Mock<ITour> tourMock;
        private readonly Partie partie;

        public JoueursTests()
        {
            pileMock = new Mock<IPile>();
            piocheMock = new Mock<IPioche>();
            tourMock = new Mock<ITour>();

            partie = new Partie(pileMock.Object, piocheMock.Object, tourMock.Object);
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