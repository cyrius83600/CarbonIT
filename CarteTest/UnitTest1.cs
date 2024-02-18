using CarteTresors.BO;

namespace CarteTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void ExampleEnonceTest()
        {
            var l1 = "C - 3 - 4";
            var l2 = "M - 1 - 1";
            var l3 = "M - 2 - 2";
            var l4 = "T - 0 - 3 - 2";
            var l5 = "T - 1 - 3 - 1";
            var l6 = "A - Lara - 0 - 0 - S - AADADAGGA";


            var lignes = new List<string>() { l1, l2, l3, l4, l5, l6 };

            var carte = new Carte();
            carte.LireCarte(lignes);
            carte.RemplirCarte();
            var aventurier = carte.Aventuriers[0];
            Assert.AreEqual(aventurier.X, 1);
            Assert.AreEqual(aventurier.Y, 0);
            Assert.AreEqual(aventurier.Tresor, 1);
        }

        [Test]
        public void AvancerDepuisSud()
        {
            var l1 = "C - 3 - 4";
            var l2 = "M - 1 - 1";
            var l3 = "M - 2 - 2";
            var l4 = "T - 0 - 3 - 2";
            var l5 = "T - 1 - 3 - 1";
            var l6 = "A - Lara - 1 - 1 - S - AA";

            var lignes = new List<string>() { l1, l2, l3, l4, l5, l6 };

            var carte = new Carte();
            carte.LireCarte(lignes);
            carte.RemplirCarte();
            var aventurier = carte.Aventuriers[0];
            Assert.AreEqual(aventurier.Orientation, Orientation.Sud);
            Assert.AreEqual(aventurier.Y, 1);
            Assert.AreEqual(aventurier.X, 3);
        }

        [Test]
        public void BloquerMontagne()
        {
            var l1 = "C - 3 - 4";
            var l2 = "M - 1 - 1";
            var l3 = "M - 2 - 2";
            var l4 = "T - 0 - 3 - 2";
            var l5 = "T - 1 - 3 - 1";
            var l6 = "A - Lara - 0 - 1 - E - A";

            var lignes = new List<string>() { l1, l2, l3, l4, l5, l6 };

            var carte = new Carte();
            carte.LireCarte(lignes);
            carte.RemplirCarte();
            var aventurier = carte.Aventuriers[0];
            Assert.AreEqual(aventurier.Orientation, Orientation.Est);
            Assert.AreEqual(aventurier.Y, 1);
            Assert.AreEqual(aventurier.X, 1);
        }
    }
}