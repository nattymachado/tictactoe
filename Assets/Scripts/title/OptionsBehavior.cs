using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class OptionsBehavior : MonoBehaviour {

    private Dropdown _gameModeDropdown;
    private Dropdown _difficultyDropdown;
    private Text _difficultyText;
    private Button _startButton;
    private BoardConfiguration _configuration;
    private string _boardSceneName = "BoardScene";
    private string _optionsSceneName = "OptionsScene";

    // Use this for initialization
    private void Start () {
        
        _gameModeDropdown = GameObject.Find("GameModeDropdown").GetComponent<Dropdown>();
        List<Dropdown.OptionData> optionDataList = new List<Dropdown.OptionData>();
        optionDataList.Add(new Dropdown.OptionData("Select a Game Mode ..."));
        for (int position=0; position<GameModeOptions.options.Length; position++)
        {
            optionDataList.Add(new Dropdown.OptionData(GameModeOptions.options[position].Label));
        }
        _gameModeDropdown.AddOptions(optionDataList);
        _gameModeDropdown.onValueChanged.AddListener(delegate {
            GameModeDropdownChanged(_gameModeDropdown);
        });

        _difficultyDropdown = GameObject.Find("DifficultyDropdown").GetComponent<Dropdown>();
        _difficultyDropdown.onValueChanged.AddListener(delegate {
            DifficultyDropdownChanged(_gameModeDropdown);
        });
        _difficultyDropdown.gameObject.SetActive(false);
        _difficultyText = GameObject.Find("DifficultyText").GetComponent<Text>();
        _difficultyText.gameObject.SetActive(false);

        _startButton = GameObject.Find("StartButton").GetComponent<Button>();
        _startButton.onClick.AddListener(delegate {
            StartGame(_startButton);
        });

        _configuration = BoardConfigurationGetter.getConfigurationObject();

    }

    private void GameModeDropdownChanged(Dropdown dropdown)
    {
        if ((dropdown.value) == GameModeOptions.options[0].Value)
        {
            _difficultyDropdown.gameObject.SetActive(true);
            _difficultyText.gameObject.SetActive(true);
        } else
        {
            _difficultyDropdown.gameObject.SetActive(false);
            _difficultyText.gameObject.SetActive(false);
        }
        
        _configuration.GameModeOption = new GameModeOption("oi", _gameModeDropdown.value);
    }

    private void DifficultyDropdownChanged(Dropdown dropdown)
    {
        _configuration.Difficulty = (DifficultyOptions.Options) dropdown.value;
    }

    private void StartGame(Button startButton)
    {
        StartCoroutine(SceneLoader.LoadScene(_boardSceneName));
        StartCoroutine(SceneLoader.UnloadScene(_optionsSceneName));
    }
}

