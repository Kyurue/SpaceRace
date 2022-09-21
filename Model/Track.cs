namespace Model
{
    public class Track
    {

        public Track(string name, SectionTypes[] sections)
        {
            Name = name;
            Sections = ConvertToLinkedList(sections);
        }

        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }

        public LinkedList<Section> ConvertToLinkedList(SectionTypes[] sections)
        {
            if (sections != null)
            {
                return null;
            }
            LinkedList<Section> linkedList = new LinkedList<Section>();
            foreach (var section in sections)
            {
                linkedList.AddLast(new Section(section));
            }
            return linkedList;
        }
    }
}
