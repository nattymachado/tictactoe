using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardManager: MonoBehaviour {


    private BoardConfiguration _configuration = null;
    private SpriteRenderer[] _positionsRenderer = null;
    private float _totalTime = 0;
    private string _optionSceneName = "OptionsScene";
    private string _boardSceneName = "BoardScene";

    public Sprite Circle = null;
    public Sprite Cross = null;
    public Sprite CrossWithCircle = null;
    public Sprite CircleWithCircle = null;
    private bool _finishingGame = false;
    private Game _game = null;
    

    private void Start () {

        _configuration = BoardConfigurationGetter.getConfigurationObject();
        Player player1 = null;
        Player player2 = null;
        if (_configuration.GameModeOption.Value == 1)
        {
            player1 = new AIPlayer(1, PlayerType.AIPlayer, Cross);

        } else
        {
            player1 = new Player(1, PlayerType.HumanPlayer, Cross);
        }
        player2 = new Player(2, PlayerType.HumanPlayer, Circle);
        _game = new Game(player1, player2);
        if (_configuration.Starter == 1)
        {
            _game.CurrentPlayer = player1;
        } else
        {
            _game.CurrentPlayer = player2;
        }
        _finishingGame = false;
        InitializeBoardPositions();

    }

    private void InitializeBoardPositions()
    {
        _positionsRenderer = GameObject.Find("positions").GetComponentsInChildren<SpriteRenderer>();

        for (int position=0; position<_positionsRenderer.Length; position++)
        {
            _positionsRenderer[position].sprite = null;
        }
    }

    private void FindingWinner()
    {
        Sprite winnerSprite = null;
        if (_game.Winner == 1)
        {
            winnerSprite = CrossWithCircle;
        } else
        {
            winnerSprite = CircleWithCircle;
        }
        Debug.Log(_game.Winner);
        if (_game.WinnerPositions != null && _game.WinnerPositions.Length > 0)
        {
            for (int position = 0; position < _game.WinnerPositions.Length; position++)
            {
                _positionsRenderer[_game.WinnerPositions[position]-1].sprite = winnerSprite;
            }
        }

    }

    private void EndGame()
    {
        _totalTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(SceneLoader.LoadScene(_optionSceneName));
            StartCoroutine(SceneLoader.UnloadScene(_boardSceneName));
        }
    }

        
    public void ClickBehavior(int positionId)
    {
        if (!_game.IsOver)
        {
            Board board = _game.Board;
            int line = ((positionId - 1) / 3);
            int column = ((positionId - 1) % 3);
            if (board.GetPosition(line, column) == 0)
            {
                if (_game.CurrentPlayer.Type == PlayerType.HumanPlayer)
                {
                    SetPlayerSpriteOnPosition(positionId, _game.CurrentPlayer.Symbol);
                    board.SetPosition(line, column, _game.CurrentPlayer.Id);
                    _game.CurrentPlayer = (_game.CurrentPlayer == _game.Player1) ? _game.Player2 : _game.Player1;
                }
            }
        }
        
    }

    private void SetPlayerSpriteOnPosition(int positionId, Sprite sprite)
    {
        if (_positionsRenderer[positionId-1].sprite == null)
        {
            _positionsRenderer[positionId-1].sprite = sprite;
        }
    }
    
    private void Update () {
        if (_game != null && (_game.IsOver || _game.GetPossibleMoves().Count == 0))
        {
            if (_finishingGame == false)
            {
                StartCoroutine(Timer.WaitATime(5));
                
                FindingWinner();
                _finishingGame = true;
                Debug.Log(_configuration.Difficulty);
            }
            EndGame();
            
        } else
        {
            if (_game != null && _game.CurrentPlayer.Type == PlayerType.AIPlayer)
            {
                AIPlayer aiPlayer = (AIPlayer)_game.CurrentPlayer;
                int bestChoice = aiPlayer.MakePlay(_game, _configuration.Difficulty);
                SetPlayerSpriteOnPosition(bestChoice, _game.CurrentPlayer.Symbol);
                bestChoice = bestChoice - 1;
                _game.Board.SetPosition((bestChoice / 3), (bestChoice % 3), 1);
                _game.Board.seeBoard(_game.Board);
                _game.CurrentPlayer = (_game.CurrentPlayer == _game.Player1) ? _game.Player2 : _game.Player1;
            }
        }
        
        

    }


    
}
