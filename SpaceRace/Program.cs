using Controller;
using SpaceRace;
using Model;
class Program
{
    static void Main(string[] args)
    {
        Data.Initialize();
        Data.NextRace();
        Visualize.Initialize(Data.CurrentRace);
        Data.CurrentRace.DriversChanged += Visualize.OnDriversChanged;
        Data.CurrentRace.Start();
        for (; ; )
        {
            Thread.Sleep(100);
        }

    }
}