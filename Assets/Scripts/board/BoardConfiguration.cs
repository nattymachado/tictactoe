using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardConfiguration : MonoBehaviour {

    private static DifficultyOptions.Options _difficulty = DifficultyOptions.Options.Hard;
    private static GameModeOption _gameModeOption = null;
    private static int _starter = 0;

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

    public int Starter
    {
        get
        {
            return _starter;
        }
        set
        {
            _starter = value;
        }
    }

}
