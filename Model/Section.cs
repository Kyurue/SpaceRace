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
    public class Section
    {
        public Section()
        {
        }

        public Section(SectionTypes sectionTypes)        {
            SectionTypes = sectionTypes;

        }

         public SectionTypes SectionTypes { get; set; }
    }
}
