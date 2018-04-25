using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Board {

    public Board() {
        InitializePositions();
    }

    private int[,] _positions;


    public int GetPosition(int line, int column)
    {
        return _positions[line, column];
    }

    public int[,] GetPositions()
    {
        return _positions;
    }

    public void SetPosition(int line, int column, int player)
    {
        _positions[line, column] = player;
    }

    public void SetPositions(int[,] positions)
    {
        for (int line = 0; line < 3; line++)
        {
            for (int column = 0; column < 3; column++)
            {
                _positions[line, column] = positions[line, column];
            }
        }
        
    }

    public Dictionary<string, int> GetBoardDimensions()
    {

        Dictionary<string, int> Dimensions = new Dictionary<string, int>();
        Dimensions["lines"] = 3;
        Dimensions["columns"] = 3;
        return Dimensions;
    }


    public void InitializePositions()
    {
        _positions = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
    }

    public bool CheckEmptyPositions()
    {
        for (int line = 0; line < 3; line++)
        {
            for (int column = 0; column < 3; column++)
            {
                if (this.GetPosition(line, column) == 0)
                {
                    return true;
                }
            }
        }
        return false;
    }


    public void seeBoard(Board board)
    {
        Debug.Log(board.GetPosition(0, 0)  + "|" + board.GetPosition(0, 1) + "|" + board.GetPosition(0, 2));
        Debug.Log(board.GetPosition(1, 0) + "|" + board.GetPosition(1, 1) + "|" + board.GetPosition(1, 2));
        Debug.Log(board.GetPosition(2, 0) + "|" + board.GetPosition(2, 1) + "|" + board.GetPosition(2, 2));
    }

    /*public Dictionary<string, int> IsGameEnded()
    {
        Dictionary<string, int> GameResult = new Dictionary<string, int>();
        GameResult["isEnded"] = 0;
        GameResult["winner"] = 0;

        int winner = CheckGameEndingColumns();
        if (( winner == 0))
        {
            winner = CheckGameEndingLines();
            if ( winner == 0)
            {
                winner = CheckGameEndingDiagonal();
                if (winner == 0)
                {
                    if (CheckEmptyPositions())
                    {
                        Debug.Log("TEM OPCOES");
                        return GameResult;
                    }
                }
            }
        }
        GameResult["isEnded"] = 1;
        GameResult["winner"] = winner;
        return GameResult;
    }

    private int CheckGameEndingColumns()
    {
        for (int column = 0; column < 3; column++)
        {
            if ((this.GetPosition(0, column) != 0) && (this.GetPosition(0, column) == this.GetPosition(1, column)) && (this.GetPosition(1, column) == this.GetPosition(2, column)))
            {
                return this.GetPosition(0, column);
            }
        }
        return 0;
    }

    private int CheckGameEndingDiagonal()
    {
        if ((this.GetPosition(0, 0) != 0) && (this.GetPosition(0, 0) == this.GetPosition(1, 1)) && (this.GetPosition(1, 1) == this.GetPosition(2, 2)))
        {
            return this.GetPosition(0, 0);
        }
        else if ((this.GetPosition(0, 2) != 0) && (this.GetPosition(0, 2) == this.GetPosition(1, 1)) && (this.GetPosition(1, 1) == this.GetPosition(2, 0)))
        {
            return this.GetPosition(0, 2);
        }
        return 0;
    }

    private int CheckGameEndingLines()
    {
        for (int line = 0; line < 3; line++)
        {

            if ((this.GetPosition(line, 0) != 0) && (this.GetPosition(line, 0) == this.GetPosition(line, 1)) && (this.GetPosition(line, 1) == this.GetPosition(line, 2)))
            {
                return this.GetPosition(line, 0);
            }
        }
        return 0;
    }*/

    
}
