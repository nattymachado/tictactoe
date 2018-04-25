using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardConfiguration : MonoBehaviour {

    private static DifficultyOptions.Options _difficulty;

    public DifficultyOptions.Options GetDifficulty() {
        return _difficulty;
    }

    public void SetDifficulty(DifficultyOptions.Options difficulty)
    {
        _difficulty=difficulty;
    }

}
