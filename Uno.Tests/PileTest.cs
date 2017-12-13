using Moq;
using Xunit;

namespace Uno.Tests
{
    public class PileTest
    {
        private readonly Mock<IPartie> partieMock;
        private readonly Pile pile;

        public PileTest()
        {
            partieMock = new Mock<IPartie>();
            pile = new Pile(partieMock.Object);
        }

        [Fact]
        public void QuandUneCarteEstJoueeLaCouleurDuJeuEstLaCouleurDeLaCarte()
        {
            partieMock.Raise(partie => partie.CarteJouee -= null, new Joueur("Joueur 1"), new Carte(Valeur.Deux, Couleur.Rouge));

            Assert.Equal(Couleur.Rouge, pile.CouleurJeu);
            Assert.Null(pile.JoueurChoixCouleur);
        }

        [Fact]
        public void QuandUneCarteJokerEstJoueeIlNyAPasDeCouleurEnJeuEtUnJoueurDoitChoisirUneCouleur()
        {
            partieMock.Raise(partie => partie.CarteJouee -= null, new Joueur("Joueur 1"), new Carte(Valeur.Joker, Couleur.Noir));

            Assert.Null(pile.CouleurJeu);
            Assert.NotNull(pile.JoueurChoixCouleur);
            Assert.Equal("Joueur 1", pile.JoueurChoixCouleur.Nom);
        }
    }
}