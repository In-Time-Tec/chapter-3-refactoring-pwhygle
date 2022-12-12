namespace TheatricalPlayersRefactoringKata
{
    public enum PlayType
    {
        Tragedy,
        Comedy,
    }
    public class Play
    {
        private string _name;
        private PlayType _type;

        public string Name { get => _name; set => _name = value; }
        public PlayType Type { get => _type; set => _type = value; }

        public Play(string name, PlayType type) {
            this._name = name;
            this._type = type;
        }
    }
}
