using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpaceRace
{
    public static class Visualize
    {
        #region graphics
        private static string[] _startHorizontal = {    "--------------", "             |", "          |   ", "       |      ", "    |         ", " |            ", "--------------" };
        private static string[] _straightHorizontal = { "--------------", "              ", "              ", "              ", "              ", "              ", "--------------" };
        private static string[] _straightVertical = {   "-            -", "-            -", "-            -", "-            -", "-            -", "-            -", "-            -" };
        private static string[] _finishHorizontal = {   "--------------", " *            ", " *            ", " *            ", " *            ", " *            ", "--------------" };
        private static string[] _Corner1 = {            "/-------------", "-             ", "-             ", "-             ", "-             ", "-             ", "-            /" };
        private static string[] _Corner2 = {           @"-------------\", "             -", "             -", "             -", "             -", "             -",@"\            -" };
        private static string[] _Corner3 = {           @"-            \", "-             ", "-             ", "-             ", "-             ", "-             ",@"\-------------" };
        private static string[] _Corner4 = {            "/            -", "             -", "             -", "             -", "             -", "             -", "-------------/" };
        #endregion

        internal static void Initialize(Track track)
        {
            SetLocation(track);
            DrawTrack(track);
        }

        internal static string[] getTrack(SectionTypes section, Rotation direction)
        {
            switch(section)
            {
                case SectionTypes.StartGrid:
                    return _startHorizontal;
                case SectionTypes.Straight:
                    if (direction == Rotation.West || direction == Rotation.East)
                    {
                        return _straightHorizontal;
                    } 
                    return _straightVertical;
                case SectionTypes.LeftCorner:
                    switch(direction)
                    {
                        case Rotation.North:
                            return _Corner2;
                        case Rotation.East:
                            return _Corner4;
                        case Rotation.South:
                            return _Corner3;
                        case Rotation.West:
                            return _Corner1;
                    }
                    break;
                case SectionTypes.RightCorner:
                    switch (direction)
                    {
                        case Rotation.North:
                            return _Corner1;
                        case Rotation.East:
                            return _Corner2;
                        case Rotation.South:
                            return _Corner4;
                        case Rotation.West:
                            return _Corner3;
                    }
                    break;
            }
            return _finishHorizontal;
        }
        
        internal static void PrintTrack(string[]? section, int x, int y)
        {
            if(section != null)
            {
                for (int i = 0; i < section.Length; i++)
                {
                    Console.SetCursorPosition(x * 14, (y * 7) + i);
                    Console.WriteLine(section[i]);
                }
            }
        }

        public static void DrawTrack(Track track)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            int[] highestValue = new int[2] {0, 0};
            foreach (Section section in track.Sections)
            {
                if (section.Location[0] > highestValue[0]) highestValue[0] = section.Location[0];
                if (section.Location[1] > highestValue[1]) highestValue[1] = section.Location[1];
            }
            Console.SetWindowSize(highestValue[0] * 18, highestValue[1] * 15);
            for (int y = 0; y <= highestValue[1]; y++)
            {
                for (int x = 0; x <= highestValue[0]; x++)
                {
                    var currentSection = track.Sections.First;
                    bool foundSection = false;
                    while (currentSection != null && foundSection == false)
                    {
                        if (currentSection.Value.Location[0] == x && currentSection.Value.Location[1] == y)
                        {
                            PrintTrack(getTrack(currentSection.Value.SectionType, currentSection.Value.direction), x, y);
                            foundSection = true;
                        }
                        currentSection = currentSection.Next;
                        if (foundSection == false) PrintTrack(null, x, y);
                    }
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
                if (location[1] < lowestValue[1]) lowestValue[1] = location[1];
                location = ChangeLocation(location, rotation);
            }
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

        public static Rotation Rotate(Rotation rotation, bool RightCorner)
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
