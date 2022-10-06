using Controller;
using SpaceRace;

class Program
{
    static void Main(string[] args)
    {
        Data.Initialize();
        Data.NextRace();
        Visualize.Initialize(Data.CurrentRace.Track);
        for (; ; )
        {
            Thread.Sleep(100);
        }

    }
}