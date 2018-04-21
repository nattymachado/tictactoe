using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType { AIPlayer, HumanPlayer };

public abstract class Player {

    private PlayerType _playerType;
    private Sprite _symbol;

    public Player(PlayerType type, Sprite symbol)
    {
        _symbol = symbol;
        _playerType = type;
    }

    public PlayerType GetPlayerType()
    {
        return _playerType;
    }

    public void SetPlayerType(PlayerType type)
    {
        _playerType = type;
    }

    public Sprite GetSymbol()
    {
        return _symbol;
    }

    public void SetSymbol(Sprite symbol)
    {
        _symbol = symbol;
    }

    abstract public void MakePlay(Board board);

}

public class AIPlayer : Player
{
    public AIPlayer(Sprite symbol) : base(PlayerType.AIPlayer, symbol)
    {
    }

    public override void MakePlay(Board board)
    {
        Debug.Log("Sou a IA .. vou pensar");
        int depthTree = 8;

        Debug.Log(board.IsGameEnded());
        this.ExecuteMinMax(board, 8, this);


    }

    private void ExecuteMinMax(Board board, int depthTree,  Player currentPlayer)
    {
        Board cloneBoard = new Board();
        cloneBoard.SetPositions(board.GetPositions());
        Debug.Log(GetEmptyPosition(board));

       /* Dictionary<string, object> gameResult = board.IsGameEnded();
        int value;
        if ((bool) gameResult["isEnded"] == true || depthTree == 0)
        {
            return CheckScore((Player)gameResult["winner"]);
        } else if (currentPlayer != this)
        {
            //MIN
            value = 99;
            value = 
        } else
        {
            //MAX
            value = -99;
        }*/
    }

    private List<Dictionary<string, int>> GetEmptyPosition(Board board)
    {
        List<Dictionary<string,int>> emptyPositions = new List<Dictionary<string, int>>();
        Dictionary<string, int> dimensions = board.GetBoardDimensions();
        for (int line=0; line < dimensions["lines"]; line++)
        {
            for (int column = 0; column < dimensions["columns"]; column++)
            {
                if (board.GetPosition(line, column) == null)
                {
                    Dictionary<string, int> position = new Dictionary<string, int>();
                    position["line"] = line;
                    position["column"] = column;
                    Debug.Log("EMPTY: " + line + "-" + column);
                    emptyPositions.Add(position);
                }
            }
        }
        return emptyPositions;
    }

    private int CheckScore(Player winner)
    {
        if (winner.GetPlayerType() == PlayerType.AIPlayer)
        {
            return +1;
        } else if (winner.GetPlayerType() == PlayerType.HumanPlayer)
        {
            return -1;
        } else
        {
            return 0;
        }
    }

}

public class HumanPlayer : Player
{
    public HumanPlayer(Sprite symbol) : base(PlayerType.HumanPlayer, symbol)
    {
    }

    public override void MakePlay(Board board)
    {
        Debug.Log("Sou o Humano");
        Debug.Log(board.IsGameEnded());
    }

}