using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {

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

    public Dictionary<string, int> GetBoardDimensions()
    { 
    
        Debug.Log("Tô aui");
        Dictionary<string, int> Dimensions = new Dictionary<string, int>();
        Dimensions["lines"] = 3;
        Dimensions["columns"] = 3;
        return Dimensions;
    }


    public void initializePositions()
    {
        _positions = new int[3,3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }}; 
    }


}
