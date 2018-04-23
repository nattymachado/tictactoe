using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardManager: MonoBehaviour {


    private BoardConfiguration _configuration = null;
    private SpriteRenderer[] positionsRenderer = null;  
    
    public Sprite Circle = null;
    public Sprite Cross = null;
    private readonly object player1;
    private int gameIsEnded = 0;
    private bool waitingAI = false;


    // Use this for initialization
    private void Start () {

        _configuration = BoardConfigurationGetter.getConfigurationObject();
        Board board = new Board();
        _configuration.SetBoard(board);
        Player player1 = new AIPlayer(Cross);
        Player player2 = new HumanPlayer(Circle);
        _configuration.SetPlayer1(player1);
        _configuration.SetPlayer2(player2);
        _configuration.SetCurrentPlayer(player2);
        InitializeBoardPositions();

    }

    private void InitializeBoardPositions()
    {
        positionsRenderer = GetComponentsInChildren<SpriteRenderer>();

        for (int position=1; position<positionsRenderer.Length; position++)
        {
            positionsRenderer[position].sprite = null;
        }
    }

    // Update is called once per frame
    public void ClickBehavior(int positionId, int line, int column)
    {
        Board board = _configuration.GetBoard();
        Player player1 = _configuration.GetPlayer1();
        Player player2 = _configuration.GetPlayer2();
        if (board.GetPosition(line, column) == 0 && waitingAI == false)
        {
            if (_configuration.GetCurrentPlayer() == player1 && player1.GetPlayerType() == PlayerType.HumanPlayer)
            {
                SetPlayerSpriteOnPosition(positionId, player1);
                board.SetPosition(line, column, 1);
                _configuration.SetCurrentPlayer(player2);
            }
            else if (player2.GetPlayerType() == PlayerType.HumanPlayer)
            {
                SetPlayerSpriteOnPosition(positionId, player2);
                board.SetPosition(line, column, 2);
                _configuration.SetCurrentPlayer(player1);
            }
        }
    }

    private void SetPlayerSpriteOnPosition(int positionId, Player player)
    {
        if (positionsRenderer[positionId].sprite == null)
        {
            positionsRenderer[positionId].sprite = player.GetSymbol();
        }
    }
    
    // Update is called once per frame
    private void Update () {
        Board board = _configuration.GetBoard();
        
        if (_configuration.GetCurrentPlayer().GetPlayerType() == PlayerType.AIPlayer && waitingAI == false)
        {
            if (gameIsEnded == 0)
            {
                gameIsEnded = board.IsGameEnded()["isEnded"];
            }
            if (gameIsEnded == 0)
            {
                Debug.Log("Agora é a vez da IA");
                waitingAI = true;

                AIPlayer player = (AIPlayer)_configuration.GetCurrentPlayer();
                Dictionary<string, int> move = player.MakePlay(board);
                Player player1 = _configuration.GetPlayer1();
                Player player2 = _configuration.GetPlayer2();
                Debug.Log("Line:" + move["line"]);
                Debug.Log("Column:" + move["column"]);
                int positionId = move["line"] * 3 + move["column"] + 1;
                Debug.Log("PositionId: " + positionId);
                if (_configuration.GetCurrentPlayer() == player1)
                {
                    SetPlayerSpriteOnPosition(positionId, player1);
                    board.SetPosition(move["line"], move["column"], 1);
                    _configuration.SetCurrentPlayer(player2);
                }
                else
                {
                    SetPlayerSpriteOnPosition(positionId, player2);
                    board.SetPosition(move["line"], move["column"], 2);
                    _configuration.SetCurrentPlayer(player1);
                }
                Debug.Log("FIM da jogada");
                waitingAI = false;
            }
            
        }
        

    }
}
