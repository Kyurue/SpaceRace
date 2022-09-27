using System.Numerics;

namespace Model
{
    public enum SectionTypes
    {
        Straight,
        LeftCorner,
        RightCorner,
        StartGrid,
        Finish
    }
    public enum Rotation
    {
        North,
        East,
        South,
        West
    }
    public class Section
    {
        public Section()
        {
        }

        public Section(SectionTypes sectionTypes)
        {
            SectionType = sectionTypes;
        }

        public SectionTypes SectionType { get; set; }
        public int[] Location { get; set; }
        public Rotation direction { get; set; }
    }
}
