using Model;
using System.Diagnostics;
using System.Drawing;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private Random _random;
        private Dictionary<Section, SectionData> Positions;
        private Timer _timer;
        public event EventHandler<DriversChangedEventArgs> DriversChanged;

        public Race(Track track, List<IParticipant> participants)
        {
            this.Track = track;
            this.Participants = participants;
            this._random = new Random(DateTime.Now.Millisecond);
            this.Positions = new Dictionary<Section, SectionData>();
            _timer = new Timer();
            _timer.Interval = 500;
            _timer.Elapsed += OnTimedEvent;
            PlaceParticipantsOnStartgrid();
        }

        private void OnTimedEvent(Object sender, ElapsedEventArgs e)
        {
            MoveAllParticipants();
            DriversChanged?.Invoke(this, new DriversChangedEventArgs(Data.CurrentRace.Track));
        }

        private void MoveAllParticipants()
        {
            LinkedListNode<Section> currentSectionNode = Track.Sections.Last;

            while (currentSectionNode != null)
            {
                MoveParticipantsSectionData(currentSectionNode.Value, (currentSectionNode.Next != null) ? currentSectionNode.Next.Value : Track.Sections.First?.Value);
                currentSectionNode = currentSectionNode.Previous;
            }
        }

        private void MoveParticipantsSectionData(Section currentSection, Section nextSection)
        {
            SectionData currentSectionData = GetSectionData(currentSection);
            SectionData nextSectionData = GetSectionData(nextSection);

            if(currentSectionData.Left != null && !currentSectionData.Left.Equipment.IsBroken)
            {
                currentSectionData.DistanceLeft += GetParticipantDistance(currentSectionData.Left);
            }
            if(currentSectionData.Right != null && !currentSectionData.Right.Equipment.IsBroken)
            {
                currentSectionData.DistanceRight += GetParticipantDistance(currentSectionData.Right);
            }

            if(currentSectionData.DistanceLeft >= 100)
            {
                if(nextSectionData.Left == null)
                {
                    MoveParticipant(currentSectionData, nextSectionData, true, true);
                } else if (nextSectionData.Right == null)
                {
                    MoveParticipant(currentSectionData, nextSectionData, true, false);
                } 
            } else if(currentSectionData.DistanceRight >= 100)
            {
                if (nextSectionData.Right == null)
                {
                    MoveParticipant(currentSectionData, nextSectionData, false, false);
                }
                else if (nextSectionData.Left == null)
                {
                    MoveParticipant(currentSectionData, nextSectionData, false, true);
                }
            }
        }

        private void MoveParticipant(SectionData currentSectionData, SectionData nextSectionData, bool startsLeft, bool endsLeft)
        {
            if(startsLeft)
            {
                if(endsLeft)
                {
                    nextSectionData.Left = currentSectionData.Left;
                    nextSectionData.DistanceLeft = currentSectionData.DistanceLeft - 100;
                } else
                {
                    nextSectionData.Right = currentSectionData.Left;
                    nextSectionData.DistanceRight = currentSectionData.DistanceLeft - 100;
                }
                currentSectionData.Left = null;
                currentSectionData.DistanceLeft = 0;
            } else {
                if (endsLeft)
                {
                    nextSectionData.Left = currentSectionData.Right;
                    nextSectionData.DistanceLeft = currentSectionData.DistanceRight - 100;
                }
                else
                {
                    nextSectionData.Right = currentSectionData.Right;
                    nextSectionData.DistanceRight = currentSectionData.DistanceRight - 100;
                }
                currentSectionData.Right = null;
                currentSectionData.DistanceRight = 0;
            }
        }

        private int GetParticipantDistance(IParticipant participant)
        {
            return participant.Equipment.Performance * participant.Equipment.Speed;
        }

        /// <summary>
        /// start the timer
        /// </summary>
        public void StartTimer()
        {
            _timer.Start();
        }

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
            if (right) { 
                GetSectionData(section).Right = participant;
            } else { 
                GetSectionData(section).Left = participant;
            }
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
