using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Board {

    private Player[,] _positions;


    public Player GetPosition(int line, int column)
    {
        return _positions[line, column];
    }

    public Player[,] GetPositions()
    {
        return _positions;
    }

    public void SetPosition(int line, int column, Player player)
    {
        _positions[line, column] = player;
    }

    public void SetPositions(Player[,] positions)
    {
        _positions = positions;
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
        _positions = new Player[3, 3] { { null, null, null }, { null, null, null }, { null, null, null } };
    }

    public bool CheckEmptyPositions()
    {
        for (int line = 0; line < 3; line++)
        {
            for (int column = 0; column < 3; column++)
            {
                if (this.GetPosition(line, column) == null)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public Dictionary<string, object> IsGameEnded()
    {
        Dictionary<string, object> GameResult = new Dictionary<string, object>();
        GameResult["isEnded"] = false;
        GameResult["winner"] = null;

        Player winner = CheckGameEndingColumns();
        if (( winner == null))
        {
            winner = CheckGameEndingLines();
            if ( winner == null)
            {
                winner = CheckGameEndingDiagonal();
                if (winner == null)
                {
                    if (CheckEmptyPositions())
                    {
                        return GameResult;
                    }
                }
            }
        }

        GameResult["isEnded"] = true;
        GameResult["winner"] = winner;
        return GameResult;
    }

    private Player CheckGameEndingColumns()
    {
        for (int column = 0; column < 3; column++)
        {
            if ((this.GetPosition(0, column) != null) && (this.GetPosition(0, column) == this.GetPosition(1, column)) && (this.GetPosition(1, column) == this.GetPosition(2, column)))
            {
                return this.GetPosition(0, column);
            }
        }
        return null;
    }

    private Player CheckGameEndingDiagonal()
    {
        if ((this.GetPosition(0, 0) != null) && (this.GetPosition(0, 0) == this.GetPosition(1, 1)) && (this.GetPosition(1, 1) == this.GetPosition(2, 2)))
        {
            return this.GetPosition(0, 0);
        }
        else if ((this.GetPosition(0, 2) != null) && (this.GetPosition(0, 2) == this.GetPosition(1, 1)) && (this.GetPosition(1, 1) == this.GetPosition(2, 0)))
        {
            return this.GetPosition(0, 2);
        }
        return null;
    }

    private Player CheckGameEndingLines()
    {
        for (int line = 0; line < 3; line++)
        {

            if ((this.GetPosition(line, 0) != null) && (this.GetPosition(line, 0) == this.GetPosition(line, 1)) && (this.GetPosition(line, 1) == this.GetPosition(line, 2)))
            {
                return this.GetPosition(line, 0);
            }
        }
        return null;
    }
}
