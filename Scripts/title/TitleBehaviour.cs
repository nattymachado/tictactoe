using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleBehaviour : MonoBehaviour {

    private string _optionsSceneName = "OptionsScene";
    private string _titleSceneName = "TitleScene";
    private SpriteRenderer _renderer = null;
    public Sprite StartTitleSprite = null;


    // Use this for initialization
    private void Start () {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = StartTitleSprite;
    }


    private void ShowModeChoicesOptions()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(SceneLoader.LoadScene(_optionsSceneName));
            StartCoroutine(SceneLoader.UnloadScene(_titleSceneName));

        }
    }

    // Update is called once per frame
    private void Update () {

        ShowModeChoicesOptions();
    }
}
