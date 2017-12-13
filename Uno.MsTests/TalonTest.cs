using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Uno.MsTests
{
    [TestClass]
    public class TalonTest
    {
        private readonly Mock<IPartie> partieMock;
        private readonly Talon pile;

        public TalonTest()
        {
            partieMock = new Mock<IPartie>();
            pile = new Talon(partieMock.Object);
        }

        [TestMethod]
        public void QuandUneCarteEstJoueeLaCouleurDuJeuEstLaCouleurDeLaCarte()
        {
            partieMock.Raise(partie => partie.CarteJouee -= null, new Joueur("Joueur 1"), new Carte(Valeur.Deux, Couleur.Rouge));

            Assert.AreEqual(Couleur.Rouge, pile.CouleurJeu);
            Assert.IsNull(pile.JoueurChoixCouleur);
        }

        [TestMethod]
        public void QuandUneCarteJokerEstJoueeIlNyAPasDeCouleurEnJeuEtUnJoueurDoitChoisirUneCouleur()
        {
            partieMock.Raise(partie => partie.CarteJouee -= null, new Joueur("Joueur 1"), new Carte(Valeur.Joker, Couleur.Noir));

            Assert.IsNull(pile.CouleurJeu);
            Assert.IsNotNull(pile.JoueurChoixCouleur);
            Assert.AreEqual("Joueur 1", pile.JoueurChoixCouleur.Nom);
        }
    }
}