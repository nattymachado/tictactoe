using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleBehaviour : MonoBehaviour {

	// Use this for initialization
	private void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {
		
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Scene activeScene = SceneManager.GetActiveScene();
            Debug.Log(activeScene.name);
            if (activeScene.name != "Main")
            {
                Scene mainScene = SceneManager.GetSceneByName("Main");
                SceneManager.LoadSceneAsync(mainScene.name);
                while (!mainScene.isLoaded)
                {
                    Debug.Log("Loading");
                }
                SceneManager.SetActiveScene(mainScene);
            }
        }
	}
}
