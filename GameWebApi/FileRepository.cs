using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class FileRepository //: IRepository
{
    private const string filePath = "game-dev.txt";

#region Player
    public async Task<Player> CreatePlayer(Player player)
    {
        List<Player> response = (await GetAllPlayers()).ToList();
        response.Add(player);
        File.WriteAllText(filePath, JsonConvert.SerializeObject(response));
        return player;
    }


    public async Task<Player> DeletePlayer(Guid id)
    {
        List<Player> response = (await GetAllPlayers()).ToList();
        
        for(int i = 0; i < response.Count; i++ )
        {
            if(response[i].Id == id)
            {
                response.Remove(response[i]);
                File.WriteAllText(filePath, JsonConvert.SerializeObject(response));
                return response[i];
            }

        }
        return null;
    }

    public async Task<Player> GetPlayer(Guid id)
    {
        Player[] response = await GetAllPlayers();
        
        for(int i = 0; i < response.Length; i++ )
        {
            if(response[i].Id == id)
            {
                return response[i];
            }

        }
        return null;
    }

    public async Task<Player[]> GetAllPlayers()
    {
        if (!File.Exists(filePath)) 
            return new Player[0];
        string json = File.ReadAllText(filePath);
        if(json.Length == 0)
            return new Player[0];
        return JsonConvert.DeserializeObject<Player[]>(json);    
    }



    public async Task<Player> UpdatePlayer(Player player)
    {
        Player[] response = await GetAllPlayers();
        
        for(int i = 0; i < response.Length; i++ )
        {
            if(response[i].Id == player.Id)
            {
                response[i].Score = player.Score;
                return response[i];
            }

        }
        return null;
    }
#endregion

}