using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Driver : IParticipant
    {

        public Driver(string name, int points, Ship ship, TeamColours teamcolor)
        {
            Name = name;
            Points = points;
            Equipment = ship;
            Teamcolour = teamcolor;
        }

        public string Name { get ; private set; }
        public int Points { get; set; }
        public IEquipment Equipment { get; set; }
        public TeamColours Teamcolour { get; set; }
    }
}
