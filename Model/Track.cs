namespace Model
{
    public class Track
    {

        public Track(string name, int rounds, SectionTypes[] sections)
        {
            Name = name;
            Rounds = rounds;
            Sections = ConvertToLinkedList(sections);
        }

        public string Name { get; set; }

        public int Rounds { get; set; }
        public LinkedList<Section> Sections { get; set; }

        public LinkedList<Section> ConvertToLinkedList(SectionTypes[] sections)
        {
            if (sections != null)
            {
                LinkedList<Section> linkedList = new LinkedList<Section>();
                foreach (var section in sections)
                {
                    linkedList.AddLast(new Section(section));
                }
                return linkedList;
            }
            return null;
        }
    }
}
