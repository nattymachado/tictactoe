using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardManager: MonoBehaviour {


    private BoardConfiguration _configuration = null;
    private SpriteRenderer[] positionsRenderer = null;
    
    public Sprite Circle = null;
    public Sprite Cross = null;
    private readonly object player1;


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
        initializeBoardPositions();
        board.InitializePositions();
    }

    private void initializeBoardPositions()
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
        seeBoard(board);
        Player player1 = _configuration.GetPlayer1();
        Player player2 = _configuration.GetPlayer2();

        if (_configuration.GetCurrentPlayer() ==  player1 && player1.GetPlayerType() == PlayerType.HumanPlayer)
        {
            positionsRenderer[positionId].sprite = player1.GetSymbol();
            board.SetPosition(line, column, player1);
            _configuration.SetCurrentPlayer(player2);
        } else if (player2.GetPlayerType() == PlayerType.HumanPlayer)
        {
            positionsRenderer[positionId].sprite = player2.GetSymbol();
            board.SetPosition(line, column, player2);
            _configuration.SetCurrentPlayer(player1);
        }
    }

    private void seeBoard(Board board)
    {
        for (int line = 0; line < 3; line++)
        {
            for (int column = 0; column < 3; column++)
            {
                Debug.Log("Position:" + board.GetPosition(line, column));
            }
        }
    }

    

    // Update is called once per frame
    private void Update () {
        Board board = _configuration.GetBoard();
        if (_configuration.GetCurrentPlayer().GetPlayerType() == PlayerType.AIPlayer)
        {
            Debug.Log("Agora é a vez da IA");
            
            AIPlayer player = (AIPlayer)_configuration.GetCurrentPlayer();
            Dictionary<string, int> move = player.MakePlay(board);
            Player player1 = _configuration.GetPlayer1();
            Player player2 = _configuration.GetPlayer2();
            int positionId = move["line"] * 3 + move["column"] + 1;
            if (_configuration.GetCurrentPlayer() == player1)
            {
                positionsRenderer[positionId].sprite = player1.GetSymbol();
                board.SetPosition(move["line"], move["column"], player1);
                _configuration.SetCurrentPlayer(player2);
            }
            else
            {
                positionsRenderer[positionId].sprite = player2.GetSymbol();
                board.SetPosition(move["line"], move["column"], player2);
                _configuration.SetCurrentPlayer(player1);
            }
            Debug.Log("FIM da jogada");
        }
        

    }
}
