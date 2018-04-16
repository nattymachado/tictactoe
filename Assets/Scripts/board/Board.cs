using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : Object {

    private int[,] _positions;
    
    public int getPosition(int column, int line)
    {
        Debug.Log("Getting position");
        return _positions[line, column];
    }

    public void setPosition(int column, int line, int value)
    {
        Debug.Log("Setting position");
        _positions[line, column] = value;
    }


    public void initializePositions()
    {
        _positions = new int[3,3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }}; 
    }


}
