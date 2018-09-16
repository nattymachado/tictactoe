using System;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManagerSpecific : NetworkManager {

    public static event Action<NetworkConnection> onServerConnect;

    public static NetworkDiscovery Discovery
    {
        get
        {
            Debug.Log("Getting");
            return singleton.GetComponent<NetworkDiscovery>();
        }
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        if (conn.address == "localClient")
        {
            return;
        }

        Debug.Log("Client connected! Address: " + conn.address);

        if (onServerConnect != null)
        {
            onServerConnect(conn);
        }
    }

    public override void OnClientError(NetworkConnection conn, int errorCode)
    {
        base.OnClientError(conn, errorCode);
    }
    /*
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Detect when a client connects to the Server
    public override void OnClientConnect(NetworkConnection connection)
    {
        
        //Output text to show the connection on the client side
        Debug.Log(ClientScene.reconnectId);
        Debug.Log("Client Side : Client " + connection.connectionId + " Connected!");

    }


    //Detect when a client connects to the Server
    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        
        ClientScene.Ready(conn);
        ClientScene.AddPlayer(0);
    }*/
    
}
