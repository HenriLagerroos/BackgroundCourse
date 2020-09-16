using System.Threading.Tasks;

public interface ICityBikeDataFetcher
{
    public Task<BikeRentalStationList> Fetch();
}