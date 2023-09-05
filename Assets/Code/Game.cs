using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    private List<Player> Players;
    private int _currentPlayerIdx;
    private Player CurrentPlayer { get => Players[_currentPlayerIdx]; }
    // Start is called before the first frame update
    void Start()
    {
        Player[] players = transform.GetComponentsInChildren<Player>();
        Players = players.ToList();
        CurrentPlayer.NewTurn();
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentPlayer.Update();
    }

    public void NextPlayer()
    {
        ++_currentPlayerIdx;
        if (_currentPlayerIdx == Players.Count)
        {
            _currentPlayerIdx = 0;
        }
        CurrentPlayer.NewTurn();
    }

}
