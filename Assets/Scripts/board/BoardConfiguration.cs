using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardConfiguration : MonoBehaviour {

    private static DifficultyOptions.Options _difficulty;
    private static GameModeOption _gameModeOption;

    public DifficultyOptions.Options Difficulty
    {
        get
        {
            return _difficulty;
        }
        set
        {
            _difficulty = value;
        }
    }

    public GameModeOption GameModeOption
    {
        get
        {
            return _gameModeOption;
        }
        set
        {
            _gameModeOption = value;
        }
    }

}
