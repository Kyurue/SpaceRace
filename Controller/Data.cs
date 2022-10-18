using Model;
using System.Runtime.CompilerServices;

namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; set; }
        public static Race CurrentRace { get; set; }
        public static void Initialize()
        {
            Competition = new Competition();
            Participants();
            AddTracks();
        }

        public static void NextRace()
        {
            Track CurrentTrack = Competition.NextTrack();
            if(CurrentTrack != null)
            {
                CurrentRace = new Race(CurrentTrack, Competition.Participants);
            }
        }


        private static void Participants()
        {
            Competition.Participants.Add(new Driver("Rick", 0, new Ship(1, 2, 3, false), TeamColours.Blue));
            Competition.Participants.Add(new Driver("Morty", 0, new Ship(2, 2, 2, false), TeamColours.Yellow));
            Competition.Participants.Add(new Driver("Alien", 0, new Ship(2, 3, 1, false), TeamColours.Green));
            Competition.Participants.Add(new Driver("Alien2", 0, new Ship(2, 3, 1, false), TeamColours.Green));
        }

        private static void AddTracks()
        {
            Competition.Tracks.Enqueue(new Track("Track1", new SectionTypes[]
                                    {
                                        SectionTypes.StartGrid,
                                        SectionTypes.StartGrid, 
                                        SectionTypes.Finish,
                                        SectionTypes.RightCorner,
                                        SectionTypes.LeftCorner,
                                        SectionTypes.RightCorner,
                                        SectionTypes.RightCorner,
                                        SectionTypes.Straight,
                                        SectionTypes.Straight,
                                        SectionTypes.Straight,
                                        SectionTypes.Straight,
                                        SectionTypes.RightCorner,
                                        SectionTypes.Straight,
                                        SectionTypes.RightCorner,
                                    }));

            Competition.Tracks.Enqueue(new Track("Track2", new SectionTypes[]
                                   {
                                        SectionTypes.StartGrid,
                                        SectionTypes.StartGrid,
                                        SectionTypes.Finish,
                                        SectionTypes.RightCorner,
                                        SectionTypes.RightCorner,
                                        SectionTypes.Straight,
                                        SectionTypes.Straight,
                                        SectionTypes.Straight,
                                        SectionTypes.RightCorner,
                                        SectionTypes.RightCorner,
                                   }));
        }
    }
}
