using Model;
using Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerTest
{
    [TestFixture]
    internal class Controller_Race_PlaceParticipants
    {
        private Competition _competition { get; set; }
        private Track track { get; set; }

        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
            _competition.Tracks.Enqueue(new Track("Track1", new SectionTypes[]
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
            track = _competition.NextTrack();
        }

        [Test]
        public void PlaceParticipants_AddParticipant()
        {
            _competition.Participants.Add(new Driver("test1", 0, new Ship(1, 2, 3, false), TeamColours.Blue));
            Race CurrentRace = new Race(track, _competition.Participants);
            CurrentRace.PlaceParticipantsOnStartgrid();
            List<Section> startGrids = CurrentRace.GetStartGrids();
            Assert.That(CurrentRace.GetSectionData(startGrids[0]).Left.Name, Is.EqualTo("test1"));
        }

        [Test]
        public void PlaceParticipants_AddParticipants()
        {
            _competition.Participants.Add(new Driver("test1", 0, new Ship(1, 2, 3, false), TeamColours.Blue));
            _competition.Participants.Add(new Driver("test2", 0, new Ship(1, 2, 3, false), TeamColours.Blue));
            _competition.Participants.Add(new Driver("test3", 0, new Ship(1, 2, 3, false), TeamColours.Blue));
            _competition.Participants.Add(new Driver("test4", 0, new Ship(1, 2, 3, false), TeamColours.Blue));
            Race CurrentRace = new Race(track, _competition.Participants);
            CurrentRace.PlaceParticipantsOnStartgrid();
            List<Section> startGrids = CurrentRace.GetStartGrids();
            Assert.That(CurrentRace.GetSectionData(startGrids[1]).Right.Name, Is.EqualTo("test4"));
        }
    }
}
