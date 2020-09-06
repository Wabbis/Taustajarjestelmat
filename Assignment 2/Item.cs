using System;

public class Item {
    public Guid id { get; set; }
    public int level { get; set; }

    public Item(Guid _id, int _level) {
        id = _id;
        level = _level;
    }

}