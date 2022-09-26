using Controller;
using SpaceRace;

class Program
{
    static void Main(string[] args)
    {
        Data.Initialize();
        Data.NextRace();
        Visualize.SetLocation(Data.CurrentRace.Track);
        Visualize.DrawTrack(Data.CurrentRace.Track);
        for (; ; )
        {
            Thread.Sleep(100);
        }

    }
}