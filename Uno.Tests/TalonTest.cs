using Moq;
using Xunit;
using Uno.Interfaces;
using Uno.ValueObjects;

namespace Uno.Tests
{
    public class TalonTest
    {
        private readonly Mock<IPartie> partieMock;
        private readonly Talon talon;

        public TalonTest()
        {
            partieMock = new Mock<IPartie>();
            talon = new Talon(partieMock.Object);
        }

        [Fact]
        public void QuandUneCarteEstJoueeLaCouleurDuJeuEstLaCouleurDeLaCarte()
        {
            partieMock.Raise(partie => partie.CarteJouee -= null, new Joueur("Joueur 1"), new Carte(Valeur.Deux, Couleur.Rouge));

            Assert.Equal(Couleur.Rouge, talon.CouleurJeu);
            Assert.Null(talon.JoueurChoixCouleur);
        }

        [Fact]
        public void QuandUneCarteJokerEstJoueeIlNyAPasDeCouleurEnJeuEtUnJoueurDoitChoisirUneCouleur()
        {
            partieMock.Raise(partie => partie.CarteJouee -= null, new Joueur("Joueur 1"), new Carte(Valeur.Joker, Couleur.Noir));

            Assert.Null(talon.CouleurJeu);
            Assert.NotNull(talon.JoueurChoixCouleur);
            Assert.Equal("Joueur 1", talon.JoueurChoixCouleur.Nom);
        }
    }
}