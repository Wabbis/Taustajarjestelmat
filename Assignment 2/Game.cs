using System.Collections.Generic;
using System.Linq;
using System;
public class Game<T> where T : IPlayer
{
    private List<T> _players;

    public Game(List<T> players) {
        _players = players;
    }

    public T[] GetTop10Players() {
        T[] topPlayers = new T[10];
        _players.OrderBy(Player => Player.score);
        for(int i = 0 ; i < 10 ; i++) {
            Console.WriteLine(_players[i]);
            topPlayers[i] = _players[i];
        }

        return topPlayers;
        // ... write code that returns 10 players with highest scores
    }
}