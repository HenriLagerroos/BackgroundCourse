using System;
using System.Threading.Tasks;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
           // if(args == null || args.Length == 0){
           //     throw new Exception("Must enter an argument");
            //}
            //Console.WriteLine("Hello World!" + args[0]);

            Console.WriteLine("offline / realtime ? ");
            string rules = Console.ReadLine();
            ICityBikeDataFetcher fetcher;
            if(string.Compare(rules, "offline") == 0)
            {
                fetcher = new OfflineCityBikeDataFetcher();
            }
           else if(string.Compare(rules, "realtime") == 0)
            {
                fetcher = new RealTimeCityBikeDataFetcher();
            }
            else
            {
                Console.WriteLine("Bye");
                return;
            }

            var fetchTask = fetcher.Fetch();

            Task.WaitAll(fetchTask); // block while the task completes

            BikeRentalStationList stationList = fetchTask.Result;
            //Console.WriteLine(list);

            Console.WriteLine("Type station: ");
            string station = Console.ReadLine();

        try {
            int bikeCount = stationList.GetBikeCountInStation(station);
            Console.WriteLine("Bikes available in " + station + ": " + bikeCount);
        }
        catch (ArgumentNullException) 
        {
            Console.WriteLine ("Please provide at least one argument");
        }
        catch (NotFoundException) 
        {
            Console.WriteLine ("We could not find station!");
        }
        catch (Exception) 
        {
            Console.WriteLine ("Name has numbers!");
        }
        }
    }
}
