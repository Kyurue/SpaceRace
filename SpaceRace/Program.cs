using Controller;
class Program
{
    static void Main(string[] args)
    {
        Data.Initialize();
        Data.NextRace();
        Console.Write(Data.CurrentRace.Track.Name);
        for (; ; )
        {
            Thread.Sleep(100);
        }


    }
}