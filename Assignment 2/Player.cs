using System;
using System.Collections.Generic;

public class Player : IPlayer {

    public Guid id { get; set; }
    public int score { get; set; }
    public List<Item> items { get; set; }
    public Player() {
        items = new List<Item>();
    }

    
}