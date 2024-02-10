namespace CarteTresors.BO
{
    public class Carte
    {
        public string Initiale { get; set; }
        public int Largeur { get; set; }
        public int Hauteur { get; set; }
        // Défini à 0 par défaut mais peut changer 
        public int DebutHorizontal { get; set; }
        // Défini à 0 par défaut mais peut changer 
        public int DebutVertical { get; set; }
        public List<Montagne> Montagnes { get; set; }
        public List<Tresor> Tresors { get; set; }

        void SetCarteMontagne(Carte carte, string[]? ligne)
        {
            var montagne = new Montagne();
            montagne.Y = int.Parse(ligne[1]);
            montagne.X = int.Parse(ligne[2]);
            carte.Montagnes.Add(montagne);
        }

        void SetCarteTresor(Carte carte, string[]? ligne)
        {
            var tresor = new Tresor();
            tresor.Y = int.Parse(ligne[1]);
            tresor.X = int.Parse(ligne[2]);
            tresor.Quantite = int.Parse(ligne[3]);
            carte.Tresors.Add(tresor);
        }

        void SetCarteDimensions(Carte carte, string[]? ligne)
        {
            carte.Largeur = int.Parse(ligne[1]);
            carte.Hauteur = int.Parse(ligne[2]);
        }
        public Carte() { }

        public Carte LireCarte(List<string> lignes)
        {
            var carte = new Carte();
            carte.Montagnes = new List<Montagne>();
            carte.Tresors = new List<Tresor>();
            foreach (var l in lignes)
            {
                var ligne = l.Replace(" ", "").Split('-');
                if (l.StartsWith("C"))
                {
                    SetCarteDimensions(carte, ligne);
                }
                else if (l.StartsWith("M"))
                {
                    SetCarteMontagne(carte, ligne);
                }
                else if (l.StartsWith("T"))
                {
                    SetCarteTresor(carte, ligne);
                }
            }
            return carte;
        }

        public List<String> DessinerCarte(Carte carte)
        {
            var liste = new List<string>();
            for (int i = 0; i < carte.Hauteur; i++)
            {
                var s = "";
                for (int j = 0; j < carte.Largeur; j++)
                {
                    var m = carte.Montagnes.Where(p => p.X == i && p.Y == j).FirstOrDefault();
                    var t = carte.Tresors.Where(p => p.X == i && p.Y == j).FirstOrDefault();
                    if (m != null)
                        s += "M     ";
                    else if (t != null)
                        s += "T(" + t.Quantite + ")  ";
                    else
                        s += ".    ";

                }
                liste.Add(s);
            }
            return liste;
        }


    }
}
