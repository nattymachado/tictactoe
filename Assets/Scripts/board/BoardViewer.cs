using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardViewer : MonoBehaviour {

    private Camera _camera = null;
    private Vector3 _limitOfWorld;

    // Use this for initialization
    void Start () {
        _camera = Camera.main;
        _limitOfWorld = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
