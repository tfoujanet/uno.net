using System;
using System.Linq;
using Uno;
using Uno.Tests.Helpers;
using Xunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Uno.MsTests
{
    [TestClass]
    public class PiocheTest
    {
        private readonly Pioche pioche;

        public PiocheTest()
        {
            pioche = new Pioche();
        }

        [TestMethod]
        public void LeJeuDoitComporter108Cartes()
        {
            Assert.Equal(108, pioche.ListeCartes.Count);
        }

        [TestMethod]
        public void LeJeuContient4JokersEt4CartesPlus4()
        {
            Assert.Equal(4, pioche.CartesValeurs(Valeur.Joker).Count());
            Assert.Equal(4, pioche.CartesValeurs(Valeur.Plus4).Count());
        }

        [TestMethod]
        public void LeJeuContient8CartesPlus2()
        {
            var cartesPlus2 = pioche.CartesValeurs(Valeur.Plus2);
            Assert.Equal(8, cartesPlus2.Count());
            Assert.Equal(2, cartesPlus2.Count(_ => _.Couleur == Couleur.Bleu));
            Assert.Equal(2, cartesPlus2.Count(_ => _.Couleur == Couleur.Rouge));
            Assert.Equal(2, cartesPlus2.Count(_ => _.Couleur == Couleur.Vert));
            Assert.Equal(2, cartesPlus2.Count(_ => _.Couleur == Couleur.Jaune));
        }

        [TestMethod]
        public void LeJeuContient8CartesChangementSens()
        {
            var cartesChangementSens = pioche.CartesValeurs(Valeur.ChangementSens);
            Assert.Equal(8, cartesChangementSens.Count());
            Assert.Equal(2, cartesChangementSens.Count(_ => _.Couleur == Couleur.Bleu));
            Assert.Equal(2, cartesChangementSens.Count(_ => _.Couleur == Couleur.Rouge));
            Assert.Equal(2, cartesChangementSens.Count(_ => _.Couleur == Couleur.Vert));
            Assert.Equal(2, cartesChangementSens.Count(_ => _.Couleur == Couleur.Jaune));
        }

        [TestMethod]
        public void LeJeuContient8CartesPasseTour()
        {
            var cartesPasseTour = pioche.CartesValeurs(Valeur.PasseTour);
            Assert.Equal(8, cartesPasseTour.Count());
            Assert.Equal(2, cartesPasseTour.Count(_ => _.Couleur == Couleur.Bleu));
            Assert.Equal(2, cartesPasseTour.Count(_ => _.Couleur == Couleur.Rouge));
            Assert.Equal(2, cartesPasseTour.Count(_ => _.Couleur == Couleur.Vert));
            Assert.Equal(2, cartesPasseTour.Count(_ => _.Couleur == Couleur.Jaune));
        }

        [TestMethod]
        public void LeJeuContient19CartesNumeroteesRouge()
        {
            var cartesRouges = pioche.CartesCouleur(Couleur.Rouge);
            Assert.Equal(19, cartesRouges.CarteNumerotees().Count());
        }

        [TestMethod]
        public void LeJeuContient19CartesNumeroteesJaune()
        {
            var cartesJaunes = pioche.CartesCouleur(Couleur.Jaune);
            Assert.Equal(19, cartesJaunes.CarteNumerotees().Count());
        }

        [TestMethod]
        public void LeJeuContient19CartesNumeroteesBleues()
        {
            var cartesBleues = pioche.CartesCouleur(Couleur.Bleu);
            Assert.Equal(19, cartesBleues.CarteNumerotees().Count());
        }

        [TestMethod]
        public void LeJeuContient19CartesNumeroteesVertes()
        {
            var cartesVertes = pioche.CartesCouleur(Couleur.Vert);
            Assert.Equal(19, cartesVertes.CarteNumerotees().Count());
        }
    }
}