//http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

// HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.

public class RealTimeCityBikeDataFetcher : ICityBikeDataFetcher

{
    // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
    static readonly HttpClient client = new HttpClient();
    public async Task<BikeRentalStationList> Fetch()
    {

    // Call asynchronous network methods in a try/catch block to handle exceptions.
    try	
    {
            //HttpResponseMessage response = await client.GetAsync("http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental");
            //response.EnsureSuccessStatusCode();
            //string responseBody = await response.Content.ReadAsStringAsync();
        // Above three lines can be replaced with new helper method below
         string responseBody = await client.GetStringAsync("http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental");

        //Console.WriteLine(responseBody);

        return JsonConvert.DeserializeObject<BikeRentalStationList>(responseBody);
        //Console.WriteLine(stationList.stations[0].name + " " + stationList.stations[0].bikesAvailable);
    }
    catch(HttpRequestException e)
    {
        Console.WriteLine("\nException Caught!");	
        Console.WriteLine("Message :{0} ",e.Message);
        return null;
    }
    }
}

