using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyInitiator : MonoBehaviour {

    public Object prefab;

    void Start()
    {
        for (int i = 0; i < 50; i++)
        {
            Instantiate(prefab, new Vector3(i * 2.0F, 0, 0), Quaternion.identity);
        }
    }


    void Awake()
    {
        prefab = Resources.Load("butterfly");
    }
}
