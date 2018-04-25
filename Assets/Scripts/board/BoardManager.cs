using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardManager: MonoBehaviour {


    private BoardConfiguration _configuration = null;
    private SpriteRenderer[] positionsRenderer = null;  
    
    public Sprite Circle = null;
    public Sprite Cross = null;
    private bool waitingAI = false;
    private Game _game = null;
    public Sprite player1Sprite = null;
    public Sprite player2Sprite = null;
    private AIPlayer _aiPalyer = new AIPlayer();


    // Use this for initialization
    private void Start () {

        _configuration = BoardConfigurationGetter.getConfigurationObject();
        _game = new Game(1, 2);
        _game.CurrentPlayer = 1;
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
    public void ClickBehavior(int positionId)
    {
        Board board = _game.Board;
        int line = ((positionId-1) / 3);
        int column = ((positionId - 1) % 3);
        if (board.GetPosition(line, column) == 0)
        {
            if (_game.CurrentPlayer == 2)
            {
                SetPlayerSpriteOnPosition(positionId, player2Sprite);
                board.SetPosition(line, column, 2);
                _game.CurrentPlayer = 1;
            }
        }
    }

    private void SetPlayerSpriteOnPosition(int positionId, Sprite sprite)
    {
        if (positionsRenderer[positionId].sprite == null)
        {
            positionsRenderer[positionId].sprite = sprite;
        }
    }
    
    // Update is called once per frame
    private void Update () {
       if (_game.CurrentPlayer == 1)
        {
            
            int bestChoice = _aiPalyer.MakePlay(_game);
            SetPlayerSpriteOnPosition(bestChoice, player1Sprite);
            bestChoice = bestChoice - 1;
            _game.Board.SetPosition((bestChoice/3), (bestChoice%3), 1);
            _game.Board.seeBoard(_game.Board);
            _game.CurrentPlayer = 2;

        }
        

    }


    
}
