using System;
using System.Collections.Generic;

public class PlayerForAnotherGame : IPlayer {

    public Guid id { get; set; }
    public int score { get; set; }
    public List<Item> items { get; set; }
    public PlayerForAnotherGame() {
        items = new List<Item>();
    }

    
}