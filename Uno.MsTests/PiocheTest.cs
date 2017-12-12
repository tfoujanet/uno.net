using System;
using System.Linq;
using Uno;
using Uno.Helpers;
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
            Assert.AreEqual(108, pioche.ListeCartes.Count);
        }

        [TestMethod]
        public void LeJeuContient4JokersEt4CartesPlus4()
        {
            Assert.AreEqual(4, pioche.CartesValeurs(Valeur.Joker).Count());
            Assert.AreEqual(4, pioche.CartesValeurs(Valeur.Plus4).Count());
        }

        [TestMethod]
        public void LeJeuContient8CartesPlus2()
        {
            var cartesPlus2 = pioche.CartesValeurs(Valeur.Plus2);
            Assert.AreEqual(8, cartesPlus2.Count());
            Assert.AreEqual(2, cartesPlus2.Count(_ => _.Couleur == Couleur.Bleu));
            Assert.AreEqual(2, cartesPlus2.Count(_ => _.Couleur == Couleur.Rouge));
            Assert.AreEqual(2, cartesPlus2.Count(_ => _.Couleur == Couleur.Vert));
            Assert.AreEqual(2, cartesPlus2.Count(_ => _.Couleur == Couleur.Jaune));
        }

        [TestMethod]
        public void LeJeuContient8CartesChangementSens()
        {
            var cartesChangementSens = pioche.CartesValeurs(Valeur.ChangementSens);
            Assert.AreEqual(8, cartesChangementSens.Count());
            Assert.AreEqual(2, cartesChangementSens.Count(_ => _.Couleur == Couleur.Bleu));
            Assert.AreEqual(2, cartesChangementSens.Count(_ => _.Couleur == Couleur.Rouge));
            Assert.AreEqual(2, cartesChangementSens.Count(_ => _.Couleur == Couleur.Vert));
            Assert.AreEqual(2, cartesChangementSens.Count(_ => _.Couleur == Couleur.Jaune));
        }

        [TestMethod]
        public void LeJeuContient8CartesPasseTour()
        {
            var cartesPasseTour = pioche.CartesValeurs(Valeur.PasseTour);
            Assert.AreEqual(8, cartesPasseTour.Count());
            Assert.AreEqual(2, cartesPasseTour.Count(_ => _.Couleur == Couleur.Bleu));
            Assert.AreEqual(2, cartesPasseTour.Count(_ => _.Couleur == Couleur.Rouge));
            Assert.AreEqual(2, cartesPasseTour.Count(_ => _.Couleur == Couleur.Vert));
            Assert.AreEqual(2, cartesPasseTour.Count(_ => _.Couleur == Couleur.Jaune));
        }

        [TestMethod]
        public void LeJeuContient19CartesNumeroteesRouge()
        {
            var cartesRouges = pioche.CartesCouleur(Couleur.Rouge);
            Assert.AreEqual(19, cartesRouges.CarteNumerotees().Count());
        }

        [TestMethod]
        public void LeJeuContient19CartesNumeroteesJaune()
        {
            var cartesJaunes = pioche.CartesCouleur(Couleur.Jaune);
            Assert.AreEqual(19, cartesJaunes.CarteNumerotees().Count());
        }

        [TestMethod]
        public void LeJeuContient19CartesNumeroteesBleues()
        {
            var cartesBleues = pioche.CartesCouleur(Couleur.Bleu);
            Assert.AreEqual(19, cartesBleues.CarteNumerotees().Count());
        }

        [TestMethod]
        public void LeJeuContient19CartesNumeroteesVertes()
        {
            var cartesVertes = pioche.CartesCouleur(Couleur.Vert);
            Assert.AreEqual(19, cartesVertes.CarteNumerotees().Count());
        }

        [TestMethod]
        public void QuandOnTireUneCarteElleDisparaitDeLaPioche()
        {
            var carte = pioche.TirerCarte();

            CollectionAssert.DoesNotContain(pioche.ListeCartes, carte);
            Assert.AreEqual(107, pioche.ListeCartes.Count);
        }
    }
}