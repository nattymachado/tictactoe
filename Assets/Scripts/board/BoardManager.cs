using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardManager: MonoBehaviour {


    private BoardConfiguration _configuration = null;
    private SpriteRenderer[] _positionsRenderer = null;
    private float _totalTime = 0;
    private float _infoSpeed = 3;
    private Vector3 _limitOfWorld;
    private Camera _camera = null;

    public Sprite Circle = null;
    public Sprite Cross = null;
    public Sprite CrossWithCircle = null;
    public Sprite CircleWithCircle = null;
    private bool _finishingGame = false;
    private Game _game = null;
    private AIPlayer _aiPalyer = new AIPlayer();
    

    // Use this for initialization
    private void Start () {

        _configuration = BoardConfigurationGetter.getConfigurationObject();
        Player player1 = null;
        Player player2 = null;
        if (_configuration.GameModeOption.Value == 1)
        {
            //"Player X Computer"
            player1 = new Player(1, PlayerType.AIPlayer, Cross);

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
        _camera = Camera.main;
        _limitOfWorld = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
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

    private void FindWinner()
    {

        Sprite winnerSprite = null;
        if (_game.Winner == 1 && _positionsRenderer != null)
        {
            winnerSprite = CrossWithCircle;
        } else
        {
            winnerSprite = CircleWithCircle;
        }
        if (_game.WinnerPositions.Length > 0)
        {
            for (int position = 0; position < _game.WinnerPositions.Length; position++)
            {
                _positionsRenderer[(_game.WinnerPositions[position]-1)].sprite = winnerSprite;
            }
        }
        
    }

    private void CheckRestart()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(SceneLoader.LoadScene("OptionsScene"));
            StartCoroutine(SceneLoader.UnloadScene("BoardScene"));

        }
    }

    private void EndGame()
    {
        _totalTime += Time.deltaTime;
        Transform infoTransform = GameObject.Find("info").GetComponentInChildren<Transform>();
        infoTransform.localScale = new Vector3(40f, 40f, 1f);
        
        Vector3 position = infoTransform.position;
        float newY = position.y;
        float newX = position.x;
        newY -= _infoSpeed * Time.deltaTime;
        newX += (_infoSpeed * 1.38f) * Time.deltaTime;

        Debug.Log(position.y);
        Debug.Log(-_limitOfWorld.y);
        if (newY > (-_limitOfWorld.y))
        {
            position.Set(position.x, newY, position.z);
        }
        if (newX < (_limitOfWorld.x))
        {
            position.Set(newX, position.y, position.z);
        }
        infoTransform.position = position;
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
                    board.SetPosition(line, column, 2);
                    _game.CurrentPlayer = (_game.CurrentPlayer == _game.Player1) ? _game.Player2 : _game.Player1;
                }
            }
        }
        
    }

    private void SetPlayerSpriteOnPosition(int positionId, Sprite sprite)
    {
        if (_positionsRenderer != null && _positionsRenderer[(positionId-1)].sprite == null)
        {
            _positionsRenderer[(positionId-1)].sprite = sprite;
        }
    }
    
    // Update is called once per frame
    private void Update () {
        if (_game != null && (_game.IsOver || _game.GetPossibleMoves().Count == 0))
        {
            if (_finishingGame == false)
            {
                StartCoroutine(Timer.WaitATime(5));
                
                FindWinner();
                _finishingGame = true;
            }
            EndGame();
            CheckRestart();
        } else
        {
            if (_game != null && _game.CurrentPlayer.Type == PlayerType.AIPlayer)
            {

                int bestChoice = _aiPalyer.MakePlay(_game, _configuration.Difficulty);
                SetPlayerSpriteOnPosition(bestChoice, _game.CurrentPlayer.Symbol);
                bestChoice = bestChoice - 1;
                _game.Board.SetPosition((bestChoice / 3), (bestChoice % 3), 1);
                _game.Board.seeBoard(_game.Board);
                _game.CurrentPlayer = (_game.CurrentPlayer == _game.Player1) ? _game.Player2 : _game.Player1;

            }
        }
        
        

    }


    
}
