using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AIPlayer

{

    private int _playerIdentifier = 1;
    private int _bestMove = 0;

    public int MakePlay(Game game, DifficultyOptions.Options difficulty)
    {
        GetBestMove(game, -99, 99, 8, difficulty);
        
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

    private int GetBestMove(Game game, int alpha, int beta, int depth, DifficultyOptions.Options difficulty)
    {

        if (game.IsOver  || depth == 0)
            return GetScore(game.Winner, game.Player1.Id, difficulty);

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
                score=GetBestMove(cloneGame, alpha,beta, depth -1, difficulty);
                if (i == 0)
                {
                    bestScore = score;
                    bestMove = pMoves[i];
                }
                if (game.CurrentPlayer.Type == PlayerType.AIPlayer)
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

    private int getRamdomNumber(DifficultyOptions.Options difficulty)
    {
        int randomNumber;
        if (difficulty == DifficultyOptions.Options.Easy)
        {
            randomNumber = (Random.Range(0, 2) * 2 - 1) * 10;
            Debug.Log(randomNumber);
            return randomNumber;
        } else
        {
            randomNumber = Random.Range(0, 100);
            if (randomNumber > 75)
            {

                Debug.Log(1);
                return 10;
            } else
            {
                Debug.Log(-1);
                return -10;
            }

        }
        
    }

    

    private int GetScore(int winner, int aiPlayerIdentifier, DifficultyOptions.Options difficulty)
    {
        int score;
        if (winner == 1)
        {
            score = 10;
            if (difficulty == DifficultyOptions.Options.Easy)
            {
                score = score * getRamdomNumber(difficulty);
            }
            
        } else if (winner == 2)
        {
            score = -10;
            if (difficulty != DifficultyOptions.Options.Hard)
            {
                score = score * getRamdomNumber(difficulty);
            }
        } else
        {
            score = 0;
        }
        return score;
    }

}