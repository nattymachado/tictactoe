using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardConfiguration : MonoBehaviour {

    private static DifficultyOptions.Options _difficulty;
    private static Board _board;
    private Player _player1;
    private Player _player2;
    private Player _currentPlayer;

    public DifficultyOptions.Options GetDifficulty() {
        return _difficulty;
    }

    public void SetDifficulty(DifficultyOptions.Options difficulty)
    {
        _difficulty=difficulty;
    }

    public Player GetCurrentPlayer()
    {
        return _currentPlayer;
    }

    public void SetCurrentPlayer(Player currentPlayer)
    {
        _currentPlayer = currentPlayer;
    }

    public Player GetPlayer1()
    {
        return _player1;
    }

    public void SetPlayer1(Player player)
    {
        _player1 = player;
    }

    public Player GetPlayer2()
    {
        return _player2;
    }

    public void SetPlayer2(Player player)
    {
        _player2 = player;
    }
    public Board GetBoard()
    {
        return _board;
    }

    public void SetBoard(Board board)
    {
        _board = board;
    }
}
