using System;
using System.Collections.Generic;
[Serializable]
public class Player
{
    public Player() {
        items = new List<Item>();
        tags = new List<String>();
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }
    public int Level { get; set; }
    public bool IsBanned { get; set; }
    public DateTime CreationTime { get; set; }
    public List<Item> items { get; set; }
    public List<String> tags { get; set; }

}

public class ListofPlayers{
    public List<Player> players = new List<Player>();
    
}