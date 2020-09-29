using System;
using System.ComponentModel.DataAnnotations;
public enum ItemType { SWORD, SHIELD, AXE, MACE, STAFF }

public class Item {
    
    public Guid id { get; set; }
    public string name { get; set; }
    [Range(1, 99)]
    public int Level { get; set; }

    [DateFromThePast]
    public DateTime CreationTime { get; set; }

    [DataType(DataType.Custom)] // ??? No sure what to have here
    public ItemType Type { get; set; }

}