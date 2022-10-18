using Model;
using System.Drawing;

namespace Controller
{
    public class Race
    {
        public Race(Track track, List<IParticipant> participants)
        {
            this.Track = track;
            this.Participants = participants;
            this._random = new Random(DateTime.Now.Millisecond);
            this.Positions = new Dictionary<Section, SectionData>();
            PlaceParticipantsOnStartgrid();
        }
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private Random _random;
        private Dictionary<Section, SectionData> Positions;


        public SectionData GetSectionData(Section section)
        {
            foreach(var position in Positions)
            {
                if (position.Key == section)
                {
                    return position.Value;
                }
            }
            Positions.Add(section, new SectionData());
            return Positions.Values.Last();        
        }

        /// <summary>
        /// Randomizes participant equipment. 
        /// </summary>
        public void RandomizeEquipment()
        {
            foreach (IParticipant participant in Participants)
            {
                participant.Equipment.Quality = _random.Next();
                participant.Equipment.Performance = _random.Next();
            }
        }

        /// <summary>
        /// Place participants on startgrid
        /// </summary>
        public void PlaceParticipantsOnStartgrid()
        {
            int sectionIndex = 0;
            List<Section> startGrids = GetStartGrids();
            for (int i = 0; i < Participants.Count; i++)
            {
                if (i % 2 == 0) { 
                    PlaceParticipants(startGrids[sectionIndex], Participants[i], false);
                } else { 
                    PlaceParticipants(startGrids[sectionIndex], Participants[i], true);
                    sectionIndex++;
                }
            }

        }

        /// <summary>
        /// Place participants on startgrid
        /// </summary>
        public void PlaceParticipants(Section section, IParticipant participant, bool right)
        {
            if (right)
                GetSectionData(section).Right = participant;
            else
                GetSectionData(section).Left = participant;
                
        }

        /// <summary>
        /// Get all startgrids.
        /// </summary>
        /// <returns>list with startgrids</returns>
        public List<Section> GetStartGrids()
        {
            List<Section> startGrids = Track.Sections.Where(Section => Section.SectionType == SectionTypes.StartGrid).ToList();
            startGrids.Reverse();
            return startGrids;
        }
    }
}
