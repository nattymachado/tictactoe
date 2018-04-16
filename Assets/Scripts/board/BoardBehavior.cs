using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBehavior : MonoBehaviour {


    private Board board;

	// Use this for initialization
	private void Start () {

        board = new Board();
        board.initializePositions();

        Debug.Log(board.getPosition(1, 1));
		
	}

    public void ClickBehavior()
    {
        board.setPosition(1, 1, 1);
    }
	
	// Update is called once per frame
	private void Update () {
		
	}
}
