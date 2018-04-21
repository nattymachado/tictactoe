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

}

public class AIPlayer : Player
{
    public AIPlayer(Sprite symbol) : base(PlayerType.AIPlayer, symbol)
    {
    }

    public Dictionary<string, int>  MakePlay(Board board)
    {
        int depthTree = 8;
        
        Board cloneBoard = new Board();
        cloneBoard.InitializePositions();
        //cloneBoard.SetPositions(board.GetPositions());
        return this.ExecuteMinMax(cloneBoard, depthTree, true, 0, 0);


    }

    private Dictionary<string, int> SetPositionScore(int line, int column, int value)
    {
        Dictionary<string, int> positionScore = new Dictionary<string, int>();
        positionScore["column"] = column;
        positionScore["line"] = line;
        positionScore["value"] = value;
        return positionScore;
    }
    
    private Dictionary<string, int> ExecuteMinMax(Board board, int depthTree,  bool isAI, int line, int column)
    {
        

        Dictionary<string, object> gameResult = board.IsGameEnded();
        Dictionary<string, int> value = new Dictionary<string, int>();
        if ((bool)gameResult["isEnded"] == true || depthTree == 0)
        {
            Debug.Log("ACABOU");
            return SetPositionScore(line, column, CheckScore((Player)gameResult["winner"]));
        }
        else
        {
            List<Dictionary<string, int>> emptyPositions = GetEmptyPosition(board);
            Debug.Log(emptyPositions.Count);
            
            if (!isAI)
            {
                //MIN
                value["value"] = 99;
                value["column"] = 99;
                value["line"] = 99;
                for (int i = 0; i < emptyPositions.Count; i++)
                {
                    Debug.Log(emptyPositions[i]["line"] + "x" + emptyPositions[i]["column"]);
                    Board newBoard = new Board();
                    newBoard.SetPositions(board.GetPositions());
                    newBoard.SetPosition(emptyPositions[i]["line"], emptyPositions[i]["column"], new HumanPlayer(null));
                    Dictionary<string, int>  newValue = ExecuteMinMax(newBoard, depthTree - 1, true, emptyPositions[i]["line"], emptyPositions[i]["column"]);
                    if (value["value"] > newValue["value"])
                    {
                        value = newValue;
                    }
                }

            }
            else
            {
                //MAX
                value["value"] = -99;
                value["column"] = -99;
                value["line"] = -99;
                for (int i = 0; i < emptyPositions.Count; i++)
                {
                    Debug.Log(emptyPositions[i]["line"] + "x" + emptyPositions[i]["column"]);
                    Board newBoard = new Board();
                    newBoard.SetPositions(board.GetPositions());
                    newBoard.SetPosition(emptyPositions[i]["line"], emptyPositions[i]["column"], this);
                    Dictionary<string, int> newValue = ExecuteMinMax(newBoard, depthTree - 1, false, emptyPositions[i]["line"], emptyPositions[i]["column"]);
                    if (value["value"] < newValue["value"])
                    {
                        value = newValue;
                    }
                }
            }
        }
        Debug.Log("Value:" + value["value"]);
        Debug.Log("Line:" + value["line"]);
        Debug.Log("Column:" + value["column"]);
        return value;
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
                    //Debug.Log("EMPTY: " + line + "-" + column);
                    emptyPositions.Add(position);
                }
            }
        }
        return emptyPositions;
    }

    private int CheckScore(Player winner)
    {
        if (winner != null && winner.GetPlayerType() == PlayerType.AIPlayer)
        {
            return +1;
        } else if (winner != null && winner.GetPlayerType() == PlayerType.HumanPlayer)
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

}