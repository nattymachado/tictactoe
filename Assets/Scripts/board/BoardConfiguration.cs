using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardConfiguration : MonoBehaviour {

    private static DifficultyOptions.Options _difficulty;
    private static Board _board;

    public DifficultyOptions.Options GetDifficulty() {
        return _difficulty;
    }

    public void SetDifficulty(DifficultyOptions.Options difficulty)
    {
        _difficulty=difficulty;
    }

    public Board GetBoard()
    {
        return _board;
    }


    public void SetBoard(Board board)
    {
        _board= board;
    }
}
