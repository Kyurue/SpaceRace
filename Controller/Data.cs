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
                CurrentRace.StartTimer();
            }
        }


        private static void Participants()
        {
            Competition.Participants.Add(new Driver("Rick", 0, new Ship(1, 4, 15, false), TeamColours.Blue));
            Competition.Participants.Add(new Driver("Tim", 0, new Ship(2, 10, 6, false), TeamColours.Yellow));
            Competition.Participants.Add(new Driver("Alien", 0, new Ship(2, 2, 30, false), TeamColours.Green));
            Competition.Participants.Add(new Driver("Kappa", 0, new Ship(2, 7, 8, false), TeamColours.Green));
            Competition.Participants.Add(new Driver("Micha", 0, new Ship(1, 5, 15, false), TeamColours.Blue));
            Competition.Participants.Add(new Driver("Liam", 0, new Ship(2, 20, 3, false), TeamColours.Yellow));
            Competition.Participants.Add(new Driver("Collin", 0, new Ship(2, 3, 30, false), TeamColours.Green));
            Competition.Participants.Add(new Driver("Youri", 0, new Ship(2, 7, 12, false), TeamColours.Green));
            Competition.Participants.Add(new Driver("Jantje", 0, new Ship(2, 4, 20, false), TeamColours.Green));
            Competition.Participants.Add(new Driver("Fien", 0, new Ship(2, 7, 10, false), TeamColours.Green));
        }

        private static void AddTracks()
        {
            Competition.Tracks.Enqueue(new Track("Track1", 3, new SectionTypes[]
                                    {
                                        SectionTypes.StartGrid,
                                        SectionTypes.StartGrid,
                                        SectionTypes.StartGrid,
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
                                        SectionTypes.Straight,
                                        SectionTypes.Straight,
                                        SectionTypes.Straight,
                                        SectionTypes.RightCorner,
                                        SectionTypes.Straight,
                                        SectionTypes.RightCorner,
                                    }));

            Competition.Tracks.Enqueue(new Track("Track2", 5, new SectionTypes[]
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
