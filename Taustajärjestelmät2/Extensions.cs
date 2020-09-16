using System;
using System.Linq;

public static class Extensions {
	public static Item GetHighestLevelItem  (this Player p) {

        if(p.Items.Count == 0)
            return null;

        Item highest = p.Items[0]; 

        foreach (Item item in p.Items){
            if(item.Level > highest.Level)
                highest = item;
        }
        return highest;
	}

	public static Item[] GetItems (this Player p) 
    {
        Item[] items = new Item[p.Items.Count];
        for(int i = 0; i < p.Items.Count; i++)
        {
            items[i] = p.Items[i];
        }
        return items;
	}

    public static Item[] GetItemsLinq (this Player p) 
    {
        return p.Items.ToArray();
    }

    
    public static Item GetFirst (this Player p) 
    {
        if(p.Items.Count == 0)
        return null;
        return p.Items[0];
    }

    public static Item GetFirstLinq (this Player p) 
    {
        return p.Items.First();
    }

    public static void ProcessEachItem(Player player, Action<Item> process)
    {
        foreach(Item i in player.Items){
            process(i);
        }
    }

    public static void PrintItem(Item item)
    {
        Console.WriteLine("id: " + item.Id + " level: " + item.Level);
    }
}