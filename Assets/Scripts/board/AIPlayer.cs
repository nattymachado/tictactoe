using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType { AIPlayer, HumanPlayer };



public class AIPlayer

{

    private int _playerIdentifier = 1;
    private int _bestMove = 0;

    public int MakePlay(Game game)
    {
        GetBestMove(game);
        Debug.Log("BestMove:" + _bestMove);

        return _bestMove;
    }

    private Dictionary<string, int> SetPositionScore(int line, int column, int value)
    {
        Dictionary<string, int> positionScore = new Dictionary<string, int>();
        positionScore["column"] = column;
        positionScore["line"] = line;
        positionScore["value"] = value;
        return positionScore;
    }

    /*private Dictionary<string, int> ExecuteMinMax(Board board, int depthTree,  bool isAI, int line, int column)
    {


        Dictionary<string, int> gameResult = board.IsGameEnded();
        
        Dictionary<string, int> value = new Dictionary<string, int>();
        if (gameResult["isEnded"] == 1 || depthTree <= 0)
        {
            return SetPositionScore(line, column, CheckScore(gameResult["winner"]));
        }
        else
        {
            List<Dictionary<string, int>> emptyPositions = GetEmptyPosition(board);
            
            if (isAI == false)
            {
                //MIN
                value["value"] = 99;
                value["column"] = 99;
                value["line"] = 99;
                for (int i = 0; i < emptyPositions.Count; i++)
                {
                    Board newBoard = new Board();
                    newBoard.SetPositions(board.GetPositions());
                    newBoard.SetPosition(emptyPositions[i]["line"], emptyPositions[i]["column"], 2);
                    Dictionary<string, int> newValue = ExecuteMinMax(newBoard, depthTree-1, !isAI, emptyPositions[i]["line"], emptyPositions[i]["column"]);
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
                    Board newBoard = new Board();

                    newBoard.SetPositions(board.GetPositions());
                    newBoard.SetPosition(emptyPositions[i]["line"], emptyPositions[i]["column"], 1);
                    Dictionary<string, int> newValue = ExecuteMinMax(newBoard, depthTree-1, !isAI, emptyPositions[i]["line"], emptyPositions[i]["column"]);
                    if (value["value"] < newValue["value"])
                    {
                      value = newValue;
                    }
                }
            }
        }
        
        return value;
    }*/

    private int GetBestScore(List<int> scores)
    {
        int value = -999;
        int index = -999;
        for (int i=0; i< scores.Count; i++)
        {
            if (value < scores[i])
            {
                value = scores[i];
                index = i;
            }
        }
        return index;

    }

    private int GetWorseScore(List<int> scores)
    {
        int value = 999;
        int index = 999;
        for (int i = 0; i < scores.Count; i++)
        {
            if (value > scores[i])
            {
                value = scores[i];
                index = i;
            }
        }
        return index;
    }

    private int GetBestMove(Game game)
    {
        if (game.IsOver)
            return GetScore(game.Winner, game.Player1Id);

        List<int> scores = new List<int>();
        List<int> moves = new List<int>();
        List<int> pMoves = new List<int>();

        pMoves = game.GetPossibleMoves();
        if (pMoves.Count > 0)
        {
            for (int i = 0; i < pMoves.Count; i++)
            {
                Game cloneGame = game.Clone();
                cloneGame.CurrentPlayer = game.CurrentPlayer;
                cloneGame.MakeMove(pMoves[i]);
                scores.Add(GetBestMove(cloneGame));
                moves.Add(pMoves[i]);
            }

            if (game.CurrentPlayer == 1)
            {
                int indexMax = GetBestScore(scores);
                _bestMove = moves[indexMax];
                return scores[indexMax];
            }
            else
            {
                int indexMin = GetWorseScore(scores);
                _bestMove = moves[indexMin];
                return scores[indexMin];
            }
        } else
        {
            return 0;
        }
        

    }

    

    private int GetScore(int winner, int aiPlayerIdentifier)
    {
        if (winner == 1)
        {
            return 10;
        } else if (winner == 2)
        {
            return -10;
        } else
        {
            return 0;
        }
    }

}