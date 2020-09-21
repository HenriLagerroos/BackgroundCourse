using System;
using System.ComponentModel.DataAnnotations;

public enum ItemType
{
    SWORD,
    POTION,
    SHIELD
}

public class Item
{
    public Guid Id { get; set;}
   [Range(1,99)]
    public int Level { get; set;}
    [EnumDataType(typeof(ItemType))]  
    public ItemType Type { get; set;}
    [ValidateDate]
    public DateTime CreationDate{ get; set;}
}

public class NewItem
{
    [EnumDataType(typeof(ItemType))]  
    public ItemType Type { get; set;}
    [Range(1,99)]
    public int Level { get; set;}
}

public class ModifiedItem
{
    [Range(1,99)]
    public int Level { get; set;}
}