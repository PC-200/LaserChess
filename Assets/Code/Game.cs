using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    private List<Player> Players;
    private int _currentPlayerIdx;
    private Board board;
    private Player CurrentPlayer { get => Players[_currentPlayerIdx]; }
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        Player[] players = transform.GetComponentsInChildren<Player>();
        Players = players.ToList();
        CurrentPlayer.NewTurn();
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentPlayer.GameUpdate();
    }

    public void CheckForGameOver()
    {
        Player humanPlayer = Players[0];
        Player AiPlayer = Players[1];

        int commandUnits = board.Pieces.Count(p=>p.Player == AiPlayer && p.Name == "CommandUnit");
        if (commandUnits == 0)
        {
            PlayerWon(humanPlayer);
            return;
        }
        bool droneAtZero = board.Pieces.Any(p=>p.Player == AiPlayer && p.Name == "Drone" && p.Position.y == 0);
        if (droneAtZero) 
        {
            PlayerWon(AiPlayer);
            return;
        }
        int humanUnits = board.Pieces.Count(p => p.Player == humanPlayer);
        if (humanUnits == 0) 
        {
            PlayerWon(AiPlayer);
        }
        
    }
    public void PlayerWon(Player player)
    {
        Debug.Log($"Player {player.Name} has won!");
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
