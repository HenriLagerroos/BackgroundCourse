using System;
using System.Linq;

public class BikeRentalStationList 
{
    public BikeRentalStation[] stations;

    public int GetBikeCountInStation(string name)
    {
	    if (name == null || name.Length == 0) throw new ArgumentNullException ("name null");
        if(name.Any(c => char.IsDigit(c))) throw new Exception ("name has numbers");

        foreach(BikeRentalStation station in stations)
        {
            if(string.Compare(station.name, name) == 0){
                return station.bikesAvailable;
            }
        }
        throw new NotFoundException("not found");
    }
}