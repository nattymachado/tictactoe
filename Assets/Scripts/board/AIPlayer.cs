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
        GetBestMove(game, -99, 99);
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

    private int GetBestMove(Game game, int alpha, int beta)
    {
        if (game.IsOver)
            return GetScore(game.Winner, game.Player1Id);

        List<int> scores = new List<int>();
        List<int> moves = new List<int>();
        List<int> pMoves = new List<int>();
        int bestScore=0;
        int score;
        int bestMove=0;
        pMoves = game.GetPossibleMoves();
        if (pMoves.Count > 0)
        {
            for (int i = 0; i < pMoves.Count; i++)
            {
                Game cloneGame = game.Clone();
                cloneGame.CurrentPlayer = game.CurrentPlayer;
                cloneGame.MakeMove(pMoves[i]);
                score=GetBestMove(cloneGame, alpha,beta);
                if (i == 0)
                {
                    bestScore = score;
                    bestMove = pMoves[i];
                }
                if (game.CurrentPlayer == 1)
                {
                    //MAX
                    if (bestScore < score)
                    {
                        bestScore = score;
                        bestMove = pMoves[i];
                    }
                    if (score >= beta) break;
                    alpha = alpha > score ? alpha  : score;
                } else
                {
                    //MIN
                    if (bestScore > score)
                    {
                        bestScore = score;
                        bestMove = pMoves[i];
                    }
                    if (score <= alpha) break;
                    beta = beta < score ? beta : score;
                }
            }

            _bestMove = bestMove;
            return bestScore;
         
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