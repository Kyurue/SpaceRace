namespace Model
{
    public class Competition
    {
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; }

        public Competition()
        {
            Participants = new List<IParticipant>();
            Tracks = new Queue<Track>();
        }
        public Track NextTrack()
        {
            if(Tracks.Count > 0)
            {
                Track currenttrack = Tracks.Dequeue();
                return currenttrack;
            }
            return null;   
        }
    }
}
