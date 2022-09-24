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
            var current = track.Sections.First;
            while (current != null)
            {
                current.Value.direction = rotation;
                current.Value.Location = new int[]{ location[0], location[1] }; // <---- fixed you bug on this line
                switch (current.Value.SectionType)
                {
                    case SectionTypes.RightCorner:
                        rotation = Rotate(rotation, true);
                        break;
                    case SectionTypes.LeftCorner:
                        rotation = Rotate(rotation, false);
                        break;
                }
                if (location[0] < 0 || location[1] < 0)
                    location = ShiftSections(current, track.Sections);
                location = ChangeLocation(location, rotation);
                current = current.Next;
            }
            FixPosition(track);
        }

        /// <summary>
        /// function added by Jens.
        /// the goal of this function is to shift all previous sections by 1 in either the X or Y axes
        /// </summary>
        /// <param name="current">
        /// the last section you wish to change its position of
        /// all sections bevore and this one will be updated
        /// </param>
        /// <param name="list">
        /// the full list of section you wish to edit
        /// </param>
        /// <returns>
        /// returns an updated position to continue off
        /// </returns>
        private static int[] ShiftSections(LinkedListNode<Section> current, LinkedList<Section> list)
        {
            var currentSection = list.First;
            while (currentSection != null && currentSection.Value != current.Next.Value)
            {
                for (int i = 0; i <= 1; i++)
                    currentSection.Value.Location[i] = current.Value.Location[i] < 0 ? currentSection.Value.Location[i] + 1 : currentSection.Value.Location[i];

                currentSection = currentSection.Next;
            }
            return current.Value.Location;
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
