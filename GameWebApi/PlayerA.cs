
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Player
{
    public Player()
    {
        Items = new List<Item>();
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }
    public int Level { get; set; }
    public bool IsBanned { get; set; }
    public DateTime CreationTime { get; set; }
    public List<Item> Items {get; set;}

    public static implicit operator Task<object>(Player v)
    {
        throw new NotImplementedException();
    }
}

public class NewPlayer
{
    public string Name { get; set; }
}

public class ModifiedPlayer
{
    public int Score { get; set; }
    public int Level { get; set; }
}