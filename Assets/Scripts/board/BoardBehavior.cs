using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBehavior : MonoBehaviour {


    private BoardConfiguration _configuration = null;
    private SpriteRenderer[] positionsRenderer = null;
    public Sprite Circle = null;
    public Sprite Cross = null;

    // Use this for initialization
    private void Start () {

        _configuration = BoardConfigurationGetter.getConfigurationObject();
        Board board = new Board();
        _configuration.SetBoard(board);
        initializeBoardPositions();
    }

    private void initializeBoardPositions()
    {
        positionsRenderer = GetComponentsInChildren<SpriteRenderer>();

        for (int position=1; position<positionsRenderer.Length; position++)
        {
            positionsRenderer[position].sprite = null;
        }
    }

    // Update is called once per frame
    public void ClickBehavior(int positionId)
    {
        Debug.Log("Alguém clicou");
        Debug.Log(positionId);
        Debug.Log(positionsRenderer[positionId].sprite);
        positionsRenderer[positionId].sprite = Cross;

    }

    // Update is called once per frame
    private void Update () {
		
	}
}
