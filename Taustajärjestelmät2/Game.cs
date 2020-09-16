using System;
using System.Collections.Generic;
using System.Linq;

public class Game<T> where T : IPlayer
{
    private List<T> _players;

    public Game(List<T> players) {
        _players = players;
    }

    public T[] GetTop10Players() {
        T[] topList = new T[10];

        List<T> SortedList = _players.OrderBy(o=>o.Score).ToList();
        SortedList.Reverse();

        for(int i  = 0; i < topList.Length; i++)
        {
            topList[i] = SortedList[i];
        }
        // ... write code that returns 10 players with highest scores
        return topList;
    }


}