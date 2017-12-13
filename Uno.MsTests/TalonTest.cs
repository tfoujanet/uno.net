using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Uno.MsTests
{
    [TestClass]
    public class TalonTest
    {
        private readonly Mock<IPartie> partieMock;
        private readonly Talon talon;

        public TalonTest()
        {
            partieMock = new Mock<IPartie>();
            talon = new Talon(partieMock.Object);
        }

        [TestMethod]
        public void QuandUneCarteEstJoueeLaCouleurDuJeuEstLaCouleurDeLaCarte()
        {
            partieMock.Raise(partie => partie.CarteJouee -= null, new Joueur("Joueur 1"), new Carte(Valeur.Deux, Couleur.Rouge));

            Assert.AreEqual(Couleur.Rouge, talon.CouleurJeu);
            Assert.IsNull(talon.JoueurChoixCouleur);
        }

        [TestMethod]
        public void QuandUneCarteJokerEstJoueeIlNyAPasDeCouleurEnJeuEtUnJoueurDoitChoisirUneCouleur()
        {
            partieMock.Raise(partie => partie.CarteJouee -= null, new Joueur("Joueur 1"), new Carte(Valeur.Joker, Couleur.Noir));

            Assert.IsNull(talon.CouleurJeu);
            Assert.IsNotNull(talon.JoueurChoixCouleur);
            Assert.AreEqual("Joueur 1", talon.JoueurChoixCouleur.Nom);
        }
    }
}