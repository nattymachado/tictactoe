using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleBehaviour : MonoBehaviour {

    private string _boardSceneName = "BoardScene";
    private string _titleSceneName = "TitleScene";
    private DifficultyOptions.Options _modeChoice;
    private SpriteRenderer _renderer = null;
    private int _menuState = 0;
    public Sprite ModeChoicesSprite = null;
    public Sprite StartTitleSprite = null;


    // Use this for initialization
    private void Start () {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = StartTitleSprite;
        _menuState = 0;
    }

    private IEnumerator LoadBoardScene()
    {

        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name != _boardSceneName)
        {
            StartCoroutine(UnloadTitleScene());
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(_boardSceneName, LoadSceneMode.Additive);
            while (!loadOperation.isDone)
            {
                yield return null;
            }
        }
    }

    private IEnumerator UnloadTitleScene()
    {
        Scene titleScene = SceneManager.GetSceneByName(_titleSceneName);
        AsyncOperation loadOperation = SceneManager.UnloadSceneAsync(titleScene);
         while (!loadOperation.isDone)
         {
                yield return null;
         }
        
    }

    private void StartGame()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _modeChoice = DifficultyOptions.Options.Easy;
            } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                _modeChoice = DifficultyOptions.Options.Medium;
            } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
                _modeChoice = DifficultyOptions.Options.Hard;
            }
            StartCoroutine(LoadBoardScene());
            BoardConfiguration Configuration = BoardConfigurationGetter.getConfigurationObject();
            Configuration.SetDifficulty(_modeChoice);
        }
    }

    private void ShowModeChoicesOptions()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _renderer.sprite = ModeChoicesSprite;
            _menuState = 1;
        }
    }

    // Update is called once per frame
    private void Update () {

        switch (_menuState)
        {
            case 0:
                ShowModeChoicesOptions();
                break;
            case 1:
                StartGame();
                break;
        }
    }
}
