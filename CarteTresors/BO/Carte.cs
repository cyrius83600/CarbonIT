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
        public List<Aventurier> Aventuriers { get; set; }

        // On récupère les montagnes
        void SetCarteMontagne(string[]? ligne)
        {
            var montagne = new Montagne();
            montagne.Y = int.Parse(ligne[1]);
            montagne.X = int.Parse(ligne[2]);
            Montagnes.Add(montagne);
        }
        void SetCarteAventurier(string[]? ligne)
        {
            var aventurier = new Aventurier();
            aventurier.Nom = ligne[1];
            aventurier.Y = int.Parse(ligne[2]);
            aventurier.X = int.Parse(ligne[3]);
            SetOrientation(ligne, aventurier);
            aventurier.Mouvements = ligne[5];
            Aventuriers.Add(aventurier);
        }

        private void SetOrientation(string[]? ligne, Aventurier aventurier)
        {
            var orientation = ligne[4];
            if (orientation == "S") aventurier.Orientation = Orientation.Sud;
            if (orientation == "N") aventurier.Orientation = Orientation.Nord;
            if (orientation == "E") aventurier.Orientation = Orientation.Est;
            if (orientation == "O") aventurier.Orientation = Orientation.Ouest;
        }

        // On récupère les trésors
        void SetCarteTresor(string[]? ligne)
        {
            var tresor = new Tresor();
            tresor.Y = int.Parse(ligne[1]);
            tresor.X = int.Parse(ligne[2]);
            tresor.Quantite = int.Parse(ligne[3]);
            Tresors.Add(tresor);
        }

        // On récupère les dimensions de la carte
        void SetCarteDimensions(string[]? ligne)
        {
            Largeur = int.Parse(ligne[1]);
            Hauteur = int.Parse(ligne[2]);
        }
        public Carte() { }
        // On extrait les montagnes, trésors et aventuriers du fichier text
        public void LireCarte(List<string> lignes)
        {
            Montagnes = new List<Montagne>();
            Tresors = new List<Tresor>();
            Aventuriers = new List<Aventurier>();
            foreach (var l in lignes)
            {
                var ligne = l.Replace(" ", "").Split('-');
                if (l.StartsWith("C"))
                    SetCarteDimensions(ligne);
                else if (l.StartsWith("M"))
                    SetCarteMontagne(ligne);
                else if (l.StartsWith("T"))
                    SetCarteTresor(ligne);
                else if (l.StartsWith("A"))
                    SetCarteAventurier(ligne);
            }
        }

        public void RemplirCarte()
        {
            var count = 0;
            var positionsAventurier = new Dictionary<string, Position>();
            // On renseigne la position initiale dans le dictionnaire
            // Et on charge le trésor si l'aventurier est dessus
            foreach (var av in Aventuriers)
            {
                positionsAventurier.Add(av.Nom, new Position() { orientation = av.Orientation, X = av.X, Y = av.Y });
                av.setTresor(this);
            }
            var maxCount = Aventuriers.Select(p => p.Mouvements.Length).Max();
            while (count < maxCount)
            {
                foreach (var aventurier in Aventuriers)
                {
                    if (count < aventurier.Mouvements.Length)
                        aventurier.Deplacement(this, count);
                    if (count == aventurier.Mouvements.Length - 1)
                        aventurier.isFinChemin = true;
                }
                // A chaque déplacement, on vérifie 
                ValiderDeplacement(positionsAventurier);
                count++;
            }

        }

        public List<string> DessinerCarte()
        {
            var lignes = new List<string>();
            for (int i = 0; i < Hauteur; i++)
            {
                var s = "";
                for (int j = 0; j < Largeur; j++)
                {
                    s = DessinerCase(i, s, j);
                }
                lignes.Add(s);
            }
            return lignes;
        }

        private string DessinerCase(int i, string s, int j)
        {
            var m = Montagnes.Where(p => p.X == i && p.Y == j).FirstOrDefault();
            var t = Tresors.Where(p => p.X == i && p.Y == j).FirstOrDefault();
            var av = Aventuriers.Where(p => p.X == i && p.Y == j).FirstOrDefault();
            if (m != null)
                s += "M     ";
            else if (t != null)
                s += "T(" + t.Quantite + ")  ";
            else if (av != null)
                s += "A(" + av.Nom + ")  ";
            else
                s += ".    ";
            return s;
        }

        private void ValiderDeplacement(Dictionary<string, Position> positionsAventurier)
        {
            var cases = Aventuriers.Select(p => new Coordonnee() { X = p.X, Y = p.Y }).Distinct().ToList();
            foreach (var av in cases)
            {
                // S'il n'y a pas de doublon ou si on prend le premier aventurier
                // dans l'ordre d'apparition on valide déplacement
                var doublons = Aventuriers.Where(p => p.X == av.X && p.Y == av.Y).OrderBy(p => p.Ordre).ToList();
                var first = doublons[0];
                positionsAventurier[first.Nom] = new Position() { orientation = first.Orientation, X = first.X, Y = first.Y };
                // S'il y a des doublons tous les aventuriers reviennent à leur position initiale
                if (doublons.Count > 1)
                {
                    for (int i = 1; i < doublons.Count; i++)
                    {
                        var position = positionsAventurier[doublons[i].Nom];
                        doublons[i].X = position.X;
                        doublons[i].Y = position.Y;
                        doublons[i].Orientation = position.orientation;
                    }
                }
            }
        }




    }
}
