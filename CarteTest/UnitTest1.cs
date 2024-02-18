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
        public void Test1()
        {
            var l1 = "C - 3 - 4";
            var l2 = "M - 1 - 1";
            var l3 = "M - 2 - 2";
            var l4 = "T - 0 - 3 - 2";
            var l5 = "T - 1 - 3 - 1";
            var lignes = new List<string>() { l1, l2, l3, l4, l5 };
            var carte = new Carte();
            carte.LireCarte(lignes);
            Assert.AreEqual(carte.Tresors.Count, 2);
            Assert.AreEqual(carte.Montagnes.Count, 2);
            Assert.AreEqual(carte.Largeur, 3);
            Assert.AreEqual(carte.Hauteur, 4);
        }
    }
}