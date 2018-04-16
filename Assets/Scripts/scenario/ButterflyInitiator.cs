using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyInitiator : MonoBehaviour {

    private Object _prefab;
    public int _numberOfButterflies = 1;


    void Start()
    {
        for (int i = 0; i < _numberOfButterflies; i++)
        {
            Instantiate(_prefab, new Vector3(i * 2.0F, 0, 0), Quaternion.identity);
        }
    }


    void Awake()
    {
        _prefab = Resources.Load("butterfly");
    }
}
