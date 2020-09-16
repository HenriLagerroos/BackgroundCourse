using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

// HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.

public class OfflineCityBikeDataFetcher : ICityBikeDataFetcher
{
    public async Task<BikeRentalStationList> Fetch()
    {
    string filePath = "bikedata.txt";
    string text = await File.ReadAllTextAsync(filePath);
    string[] textLines = text.Split(new[]{ Environment.NewLine }, StringSplitOptions.None);
    BikeRentalStationList btl = new BikeRentalStationList();
    btl.stations = new BikeRentalStation[textLines.Length];
    for(int i = 0; i <  btl.stations.Length; i++)
    {
        ///Console.WriteLine(textLines[i]);
        btl.stations[i] = new BikeRentalStation();
        int separator = textLines[i].IndexOf(':');
        btl.stations[i].name = textLines[i].Substring(0, separator - 1 );
        btl.stations[i].bikesAvailable = Int32.Parse (textLines[i].Substring(separator + 1));
    }

   // Console.WriteLine(text);

    return btl;
    }
}