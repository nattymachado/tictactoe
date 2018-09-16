﻿using System;
using UnityEngine;
using UnityEngine.Networking;

public class LocalController : MonoBehaviour
{

    private string _networkType = "";
    private bool isServer = false;

    public string NetworkType
    {
        get
        {
            return _networkType;
        }
        set
        {
            _networkType = value;
        }
    }

    public void StartDiscovery()
    {
        NetworkManagerSpecific.Discovery.Initialize();
        NetworkManagerSpecific.Discovery.StartAsClient();
        _networkType = "LAN";
        Debug.Log("Init discovery");
    }
    

}
