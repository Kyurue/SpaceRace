using Controller;
using SpaceRace;

class Program
{
    static void Main(string[] args)
    {
        Data.Initialize();
        Data.NextRace();
        Visualize.DrawTrack(Data.CurrentRace.Track);
        for (; ; )
        {
            Thread.Sleep(100);
        }

    }
}