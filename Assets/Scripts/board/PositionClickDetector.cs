using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionClickDetector : MonoBehaviour
{
    public int positionId = 0;

    private void OnMouseDown()
    {
        BoardBehavior boardBehavior = GetComponentInParent<BoardBehavior>();
        Debug.Log(positionId);
        boardBehavior.ClickBehavior(positionId);
    }
}
