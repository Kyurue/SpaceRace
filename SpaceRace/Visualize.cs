using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpaceRace
{
    public static class Visualize
    {
        #region graphics
        private static string[] _starthorizontal = { "-------", ".      ", ". | |  ", ".      ", ". | |  ", ".      ", "-------" };
        private static string[] _straight = { "-------", "       ", " #   # ", "       ", " #   # ", "       ", "-------" };
        private static string[] _finishHorizontal = { "-------", "      *", "      *", "      *", "      *", "      *", "-------" };
        private static string[] _leftCorner = { "/------", "-      ", "-      ", "-      ", "-      ", "-      ", "-     -" };
        private static string[] _rightCorner = { "-------", "      -", "      -", "      -", "      -", "      -", "-     -" };
        #endregion

        internal static void Initialize()
        {

        }

        public static void DrawTrack(Track track)
        {
            int[] location = new int[2] {0, 0};
            Rotation rotation = Rotation.East;
            foreach (Section section in track.Sections)
            {
                section.direction = rotation;
                section.Location = location;
                switch (section.SectionType)
                {
                    case SectionTypes.RightCorner:
                        rotation = Rotate(rotation, true);
                        break;
                    case SectionTypes.LeftCorner:
                        rotation = Rotate(rotation, false);
                        break;
                }
                Console.WriteLine("Direction: " + section.direction);
                Console.WriteLine("X: " + section.Location[0]);
                Console.WriteLine("Y: " + section.Location[1]);
                Console.WriteLine(" ");
                location = ChangeLocation(location, rotation);
            }
            
        }
        
        public static Rotation Rotate(Rotation rotation, Boolean RightCorner)
        {
            if(RightCorner)
            {
                switch (rotation)
                {
                    case Rotation.North :
                        return Rotation.East;
                    case Rotation.East :
                        return Rotation.South;
                    case Rotation.South:
                        return Rotation.West;
                    case Rotation.West:
                        return Rotation.North;
                }
            }
            switch (rotation)
            {
                case Rotation.North:
                    return Rotation.West;
                case Rotation.East:
                    return Rotation.North;
                case Rotation.South:
                    return Rotation.East;
            }
            return Rotation.South;
        }

        public static int[] ChangeLocation(int[] location, Rotation rotation)
        {
            switch(rotation)
            {
                case Rotation.North:
                    location[1] -= 1;
                    return location;
                case Rotation.South:
                    location[1] += 1;
                    return location;
                case Rotation.East:
                    location[0] += 1;
                    return location;
            }
            location[0] -= 1;
            return location;
        }
        
        public static void FixPosition(Track track)
        {
            foreach(Section section in track.Sections)
            {
                Console.WriteLine("Direction: " + section.direction);
                Console.WriteLine("X: " + section.Location[0]);
                Console.WriteLine("Y: " + section.Location[1]);
                Console.WriteLine(" ");
            }
        }
    }
}
