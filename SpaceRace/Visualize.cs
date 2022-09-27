using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace SpaceRace
{
    public static class Visualize
    {
        #region graphics
        private static string[] _empty = { "       ", "       ", "       ", "       ", "       ", "       ", "       " };
        private static string[] _starthorizontal = { "-------", "       ", "       ", "       ", "       ", "       ", "-------" };
        private static string[] _straighthorizontal = { "-------", "       ", "       ", " -- -- ", "       ", "       ", "-------" };
        private static string[] _straightvertical = { "-     -", "-  -  -", "-  -  -", "-     -", "-  -  -", "-  -  -", "-------" };
        private static string[] _finishHorizontal = { "-------", "      *", "      *", "      *", "      *", "      *", "-------" };
        private static string[] _leftCorner = { "/------", "-      ", "-      ", "-      ", "-      ", "-      ", "-     -" };
        private static string[] _rightCorner = { "-------", "      -", "      -", "      -", "      -", "      -", "-     -" };
        #endregion

        internal static void Initialize()
        {

        }

        public static void DrawTrack(Track track)
        {
            int[] highestValue = new int[2] {0, 0};
            foreach (Section section in track.Sections)
            {
                if (section.Location[0] > highestValue[0]) highestValue[0] = section.Location[0];
                if (section.Location[1] > highestValue[1]) highestValue[1] = section.Location[1];
            }
            for(int i = 0; i < highestValue[0]; i++)
            {
                for(int u = 0; u < highestValue[1]; u++)
                {

                }
            }
        }

        public static void SetLocation(Track track)
        {
            int[] location = new int[2] { 0, 0 };
            int[] lowestValue = new int[2] { 0, 0 };
            Rotation rotation = Rotation.East;
            foreach (Section section in track.Sections)
            {
                section.direction = rotation;
                section.Location = new int[] { location[0], location[1] };
                switch (section.SectionType)
                {
                    case SectionTypes.RightCorner:
                        rotation = Rotate(rotation, true);
                        break;
                    case SectionTypes.LeftCorner:
                        rotation = Rotate(rotation, false);
                        break;
                }
                if (location[0] < lowestValue[0]) lowestValue[0] = location[0];
                if(location[1] < lowestValue[1]) lowestValue[1] = location[1];
                location = ChangeLocation(location, rotation);
            }
            Console.WriteLine("X: " + lowestValue[0]);
            Console.WriteLine("Y: " + lowestValue[1]);
            ShiftSections(lowestValue, track);
        }

        private static void ShiftSections(int[] lowestValue, Track track)
        {
            foreach (Section section in track.Sections)
            {
                section.Location[0] = section.Location[0] + Math.Abs(lowestValue[0]);
                section.Location[1] = section.Location[1] + Math.Abs(lowestValue[1]);
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
    }
}
