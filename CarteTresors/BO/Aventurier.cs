﻿namespace CarteTresors.BO
{
    public enum Orientation
    {
        Nord,
        Sud,
        Est,
        Ouest
    }
    public class Aventurier
    {
        public string Initiale { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Mouvements { get; set; }
        public Orientation Orientation { get; set; }
        public int Tresor { get; set; }
        public string Nom { get; set; }
        public bool isFinChemin { get; set; }
        public int Ordre { get; set; }
        public void DeplacementEst()
        {
            this.Y++;
            this.Orientation = Orientation.Est;
        }

        public void DeplacementOuest()
        {
            this.Y--;
            this.Orientation = Orientation.Ouest;
        }
        public void DeplacementNord()
        {
            this.X--;
            this.Orientation = Orientation.Nord;
        }
        public void DeplacementSud()
        {
            this.X++;
            this.Orientation = Orientation.Sud;
        }
        public void AvancerDepuisSud(Carte carte, char deplacement, Montagne? montagneEst, Montagne? montagneOuest, Montagne? montagneNord, Montagne? montagneSud)
        {
            if (deplacement == 'A')
            {
                if (montagneSud != null)
                    return;
                else if (X < carte.Hauteur - 1)
                    DeplacementSud();
                else if (X == carte.Hauteur - 1)
                {
                    if (montagneOuest != null)
                        return;
                    if (Y > 0)
                        DeplacementOuest();
                    else if (Y == 0)
                    {
                        if (montagneEst != null)
                            return;
                        else if (Y < carte.Largeur - 1)
                            DeplacementEst();
                    }
                }
            }
            else if (deplacement == 'D')
            {
                if (montagneOuest != null) return;
                if (Y > 0)
                    DeplacementOuest();
                else
                {
                    if (montagneEst != null)
                        return;
                    if (Y < carte.Largeur - 1)
                        DeplacementEst();
                    else if (Y == carte.Largeur - 1)
                    {
                        if (montagneSud != null) return;
                        if (Y == 0 && X < carte.Hauteur - 1)
                            DeplacementSud();
                    }

                }
            }
            else if (deplacement == 'G')
            {
                if (montagneEst != null) return;
                if (Y < carte.Largeur - 1)
                    DeplacementEst();
                else
                {
                    if (Y == carte.Largeur - 1)
                    {
                        if (montagneOuest != null) return;
                        if (Y > 0)
                            this.DeplacementOuest();
                        if (this.X < carte.Hauteur - 1 && montagneSud == null)
                            this.DeplacementSud();

                    }
                }
            }
        }
        public void AvancerDepuisOuest(Carte carte, char deplacement, Montagne? montagneEst, Montagne? montagneOuest, Montagne? montagneNord, Montagne? montagneSud)
        {
            if (deplacement == 'A')
            {
                if (montagneOuest != null) return;
                if (Y > 0)
                    DeplacementOuest();
                else if (Y == 0)
                {
                    if (montagneNord != null) return;
                    if (X > 0)
                        DeplacementNord();
                    else
                    {
                        if (montagneSud != null) return;
                        else if (X == 0 && X < carte.Hauteur - 1)
                            DeplacementSud();
                    }
                }
            }
            if (deplacement == 'D')
            {
                if (montagneNord != null) return;
                if (X > 0)
                    DeplacementNord();
                else if (X == 0)
                {
                    if (montagneSud != null) return;
                    if (X < carte.Hauteur - 1)
                        DeplacementSud();
                    else
                    {
                        if (montagneOuest != null) return;
                        if (Y > 0)
                            DeplacementOuest();
                    }
                }
            }
            else if (deplacement == 'G')
            {
                if (montagneSud != null) return;
                if (X < carte.Hauteur - 1)
                    DeplacementSud();
                else if (X == carte.Hauteur - 1)
                {
                    if (montagneNord != null) return;
                    if (X > 0)
                        DeplacementNord();
                    else
                    {
                        if (montagneOuest != null) return;
                        if (Y > 0 && montagneEst == null)
                            DeplacementOuest();
                    }
                }

            }
        }
        public void AvancerDepuisNord(Carte carte, char deplacement, Montagne? montagneEst, Montagne? montagneOuest, Montagne? montagneNord, Montagne? montagneSud)
        {
            if (deplacement == 'A')
            {
                if (montagneNord != null) return;
                if (X > 0)
                    DeplacementNord();
                else if (X == 0)
                {
                    if (montagneEst != null) return;
                    if (Y < carte.Largeur - 1)
                        DeplacementEst();
                    else if (Y == carte.Largeur - 1)
                    {
                        if (montagneOuest != null) return;
                        if (Y > 0)
                            DeplacementOuest();
                    }
                }
            }
            if (deplacement == 'D')
            {
                if (montagneEst != null) return;
                if (Y < carte.Largeur - 1)
                    DeplacementEst();
                else if (Y == carte.Largeur - 1)
                {
                    if (montagneOuest != null) return;
                    if (Y > 0)
                        DeplacementOuest();
                    else
                    {
                        if (montagneNord != null) return;
                        if (Y > 0)
                            DeplacementNord();
                    }
                }
            }
            if (deplacement == 'G')
            {
                if (montagneOuest != null) return;
                if (Y > 0)
                    DeplacementOuest();
                else
                {
                    if (montagneEst != null) return;
                    if (Y < carte.Largeur - 1)
                        DeplacementEst();
                    else if (Y == carte.Largeur - 1)
                    {
                        if (montagneNord != null) return;
                        if (X > 0)
                            DeplacementNord();
                    }
                }
            }
        }
        public void AvancerDepuisEst(Carte carte, char deplacement, Montagne? montagneEst, Montagne? montagneOuest, Montagne? montagneNord, Montagne? montagneSud)
        {
            if (deplacement == 'A')
            {
                if (montagneEst != null) return;
                if (Y < carte.Largeur - 1)
                    DeplacementEst();
                else
                {
                    if (Y == carte.Largeur - 1)
                    {
                        if (montagneSud != null) return;
                        if (X < carte.Hauteur - 1)
                            DeplacementSud();
                        else
                        {
                            if (montagneNord != null) return;
                            if (X > 0)
                            {
                                DeplacementNord();
                            }
                        }

                    }
                }
            }
            if (deplacement == 'G')
            {
                if (montagneNord != null) return;
                if (X > 0)
                    DeplacementNord();
                else if (X == 0)
                {
                    if (montagneSud != null) return;
                    if (X < carte.Hauteur - 1)
                        DeplacementSud();
                    else
                    {
                        if (montagneEst != null) return;
                        if (Y < carte.Largeur - 1)
                            DeplacementEst();
                    }
                }
            }
            if (deplacement == 'D')
            {
                if (montagneSud != null) return;
                if (X < carte.Hauteur - 1)
                    DeplacementSud();
                else if (X == carte.Hauteur - 1)
                {
                    if (montagneNord != null) return;
                    if (X > 0)
                        DeplacementNord();
                    else if (X == 0)
                    {
                        if (montagneEst == null) return;
                        if (Y < carte.Largeur - 1)
                            DeplacementEst();
                    }
                }
            }
        }
        public void setTresor(Carte carte)
        {
            Tresor? tresor = carte.Tresors.FirstOrDefault(p => p.X == X && p.Y == Y);
            if (tresor != null && tresor.Quantite > 0)
            {
                Tresor++;
                tresor.Quantite--;
            }
        }

        public void Deplacement(Carte carte, int count)
        {
            var Montagnes = carte.Montagnes;
            var deplacement = Mouvements[count];
            var montagneEst = Montagnes.FirstOrDefault(p => p.X == X && p.Y == Y + 1);
            var montagneOuest = Montagnes.FirstOrDefault(p => p.X == X && p.Y == Y - 1);
            var montagneNord = Montagnes.FirstOrDefault(p => p.X == X - 1 && p.Y == Y);
            var montagneSud = Montagnes.FirstOrDefault(p => p.X == X + 1 && p.Y == Y);
            if (Orientation == Orientation.Sud)
            {
                AvancerDepuisSud(carte, deplacement, montagneEst, montagneOuest, montagneNord, montagneSud);
            }
            else if (Orientation == Orientation.Ouest)
            {
                AvancerDepuisOuest(carte, deplacement, montagneEst, montagneOuest, montagneNord, montagneEst);
            }
            else if (Orientation == Orientation.Nord)
            {
                AvancerDepuisNord(carte, deplacement, montagneEst, montagneOuest, montagneNord, montagneEst);
            }
            else if (Orientation == Orientation.Est)
            {
                AvancerDepuisEst(carte, deplacement, montagneEst, montagneOuest, montagneNord, montagneOuest);
            }
            setTresor(carte);
        }

    }
}
