using Controller;
using SpaceRace;
using Model;
class Program
{
    static void Main(string[] args)
    {
        Data.Initialize();
        Data.NextRace();
        Data.NextRace();
        Visualize.Initialize(Data.CurrentRace);
        for (; ; )
        {
            Thread.Sleep(100);
        }

    }
}