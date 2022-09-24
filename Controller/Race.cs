using Model;

namespace Controller
{
    public class Race
    {
        public Race(Track track, List<IParticipant> participants)
        {
            this.Track = track;
            this.Participants = participants;
            this._random = new Random(DateTime.Now.Millisecond);

        }
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private Random _random;
        private Dictionary<Section, SectionData> _positions;

        public SectionData GetSectionData(Section section)
        {
            foreach(var position in _positions)
            {
                if (position.Key == section)
                {
                    return position.Value;
                }
            }
            _positions.Add(section, new SectionData());
            return _positions.Values.Last();        
        }

        public void RandomizeEquipment()
        {
            foreach (IParticipant participant in Participants)
            {
                participant.Equipment.Quality = _random.Next();
                participant.Equipment.Performance = _random.Next();
            }
        }
    }
}
