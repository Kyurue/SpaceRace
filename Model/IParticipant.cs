namespace Model
{
    public enum TeamColours
    {
        Red,
        Green,
        Yellow,
        Grey,
        Blue
    }
    public interface IParticipant
    {
        public string Name { get; }
        public int Points { get; set; }
        public IEquipment Equipment { get; set; }
        public TeamColours Teamcolour { get; set; }
    }
}
