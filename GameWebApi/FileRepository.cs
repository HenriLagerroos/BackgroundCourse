using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class FileRepository : IRepository
{
    private const string filePath = "game-dev.txt";

    public async Task<Player> Create(Player player)
    {
        List<Player> response = (await GetAll()).ToList();
        response.Add(player);
        File.WriteAllText(filePath, JsonConvert.SerializeObject(response));
        return player;
    }

    public async Task<Player> Delete(Guid id)
    {
        List<Player> response = (await GetAll()).ToList();
        
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

    public async Task<Player> Get(Guid id)
    {
        Player[] response = await GetAll();
        
        for(int i = 0; i < response.Length; i++ )
        {
            if(response[i].Id == id)
            {
                return response[i];
            }

        }
        return null;
    }

    public async Task<Player[]> GetAll()
    {
        if (!File.Exists(filePath)) 
            return new Player[0];
        string json = File.ReadAllText(filePath);
        if(json.Length == 0)
            return new Player[0];
        return JsonConvert.DeserializeObject<Player[]>(json);    
    }

    public async Task<Player> Modify(Guid id, ModifiedPlayer player)
    {
        Player[] response = await GetAll();
        
        for(int i = 0; i < response.Length; i++ )
        {
            if(response[i].Id == id)
            {
                response[i].Score = player.Score;
                return response[i];
            }

        }
        return null;
    }
}