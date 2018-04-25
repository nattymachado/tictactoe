using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Game 
{

    private Board _board = null;
    private int _player1Id = 0;
    private int _player2Id = 0;
    private int _winner = 0;
    private bool _isOver = false;
    private int _currentPlayer = 0;

    public Game(int player1Id, int player2Id)
    {
        _board = new Board();
        _player1Id = player1Id;
        _player2Id = player2Id;

    }

    public int Player1Id
    {
        get
        {
            return _player1Id;

        }
    }

    public int Player2Id
    {
        get
        {
            return _player2Id;

        }
    }

    public bool IsOver
    {
        get
        {
            if (!_isOver)
            {
                if ((CheckGameEndingLines() != 0) || (CheckGameEndingDiagonal() != 0) || (CheckGameEndingColumns() != 0))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            } else
            {
                return _isOver;
            }
            
        }
    }

    public int Winner
    {
        get
        {
            return _winner;

        }
    }

    public Board Board
    {
        get
        {
            return _board;

        }
    }

    public int CurrentPlayer
    {
        get
        {
            return _currentPlayer;

        }
        set
        {
            _currentPlayer=value;

        }
    }

    public List<int> GetPossibleMoves()
    {
        List<int> possibleMoves = new List<int>();
        
        for (int line = 0; line < 3; line++)
        {
            for (int column = 0; column < 3; column++)
            {
                if (_board.GetPosition(line, column) == 0)
                {
                    possibleMoves.Add((line*3) + column + 1);
                }
            }
        }
        return possibleMoves;
    }

    private int CheckGameEndingColumns()
    {
        for (int column = 0; column < 3; column++)
        {
            if ((_board.GetPosition(0, column) != 0) && (_board.GetPosition(0, column) == _board.GetPosition(1, column)) && (_board.GetPosition(1, column) == _board.GetPosition(2, column)))
            {
                _winner = _board.GetPosition(0, column);
            }
        }
        return _winner;
    }

    private int CheckGameEndingDiagonal()
    {
        if ((_board.GetPosition(0, 0) != 0) && (_board.GetPosition(0, 0) == _board.GetPosition(1, 1)) && (_board.GetPosition(1, 1) == _board.GetPosition(2, 2)))
        {
            _winner = _board.GetPosition(0, 0);
        }
        else if ((_board.GetPosition(0, 2) != 0) && (_board.GetPosition(0, 2) == _board.GetPosition(1, 1)) && (_board.GetPosition(1, 1) == _board.GetPosition(2, 0)))
        {
            _winner = _board.GetPosition(0, 2);
        }
        return _winner;
    }

    private int CheckGameEndingLines()
    {
        for (int line = 0; line < 3; line++)
        {

            if ((_board.GetPosition(line, 0) != 0) && (_board.GetPosition(line, 0) == _board.GetPosition(line, 1)) && (_board.GetPosition(line, 1) == _board.GetPosition(line, 2)))
            {
                _winner = _board.GetPosition(line, 0);
            }
        }
        return _winner;
    }

    public Game Clone()
    {
        Game newGame = new Game(this.Player1Id, this.Player2Id);
        newGame.Board.SetPositions(this.Board.GetPositions());
        return newGame;
    }

    public void MakeMove(int move)
    {
        move = move - 1;
        int line = (move / 3);
        int column = (move % 3);
        Board.SetPosition(line, column, _currentPlayer);
        _currentPlayer = _currentPlayer == 1 ?  2 : 1;
    }

}
