namespace CarteTresors.BO
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
        // A par défaut 
        public string Initiale { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Mouvements { get; set; }
        public Orientation Orientation { get; set; }
        public int Tresor { get; set; }

    }
}
