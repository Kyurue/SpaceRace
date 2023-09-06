using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerTest
{
    [TestFixture]
    public class Model_Competition_NextTrackShould
    {
        private Competition _competition { get; set; }

        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
        }

        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            Track result = _competition.NextTrack();
            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {
            Track track = new Track("Track1", 2, new SectionTypes[]
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
                                   });
            _competition.Tracks.Enqueue(track);
            Track result = _competition.NextTrack();
            Assert.That(result, Is.EqualTo(track));
        }

        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue()
        {
            Track track = new Track("Track2", 3, new SectionTypes[]
                                  {
                                        SectionTypes.StartGrid,
                                        SectionTypes.LeftCorner,
                                        SectionTypes.Straight,
                                        SectionTypes.RightCorner,
                                        SectionTypes.Straight,
                                        SectionTypes.RightCorner,
                                        SectionTypes.Straight,
                                        SectionTypes.Straight,
                                        SectionTypes.LeftCorner,
                                        SectionTypes.Straight,
                                        SectionTypes.Finish
                                  });
            _competition.Tracks.Enqueue(track);
            Track result = _competition.NextTrack();
            result = _competition.NextTrack();
            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_TwoInQueue_ReturnNextTrack()
        {
            Track track1 = new Track("Track1", 4, new SectionTypes[]
                                  {
                                        SectionTypes.StartGrid,
                                        SectionTypes.Straight,
                                        SectionTypes.Straight,
                                        SectionTypes.LeftCorner,
                                        SectionTypes.Straight,
                                        SectionTypes.RightCorner,
                                        SectionTypes.Straight,
                                        SectionTypes.Finish
                                  });
            Track track2 = new Track("Track2", 1, new SectionTypes[]
                                  {
                                        SectionTypes.StartGrid,
                                        SectionTypes.LeftCorner,
                                        SectionTypes.Straight,
                                        SectionTypes.RightCorner,
                                        SectionTypes.Straight,
                                        SectionTypes.RightCorner,
                                        SectionTypes.Straight,
                                        SectionTypes.Straight,
                                        SectionTypes.LeftCorner,
                                        SectionTypes.Straight,
                                        SectionTypes.Finish
                                  });
            _competition.Tracks.Enqueue(track1);
            _competition.Tracks.Enqueue(track2);
            Track result = _competition.NextTrack();
            Assert.That(result, Is.EqualTo(track1));
            result = _competition.NextTrack();
            Assert.That(result, Is.EqualTo(track2));
        }
    }
}
