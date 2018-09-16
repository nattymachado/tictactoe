using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Linq;
using System.Text;

public class NetworkMenu : MonoBehaviour {


    private string _roomName =  "";
    private bool _isConnected = false;
    public Button StartLanButton;
    public float DiscoveryUpdatePeriod = 0.5f;
    private float _timeToRefreshMatch = 0;
    public Dropdown networkMatchesDropwork;
    private List<NetworkBroadcastResult> _matches = new List<NetworkBroadcastResult>();
    private List<Dropdown.OptionData> _optionMatchesList = new List<Dropdown.OptionData>();
    private LocalController _localController;


    void Start()
    {
        _localController = NetworkConfigurationGetter.getConfigurationObject();
        AddListeners();


    }

    private void AddListeners()
    {
        StartLanButton.onClick.AddListener(CreateLanMatch);
    }

    

    public void LoadGameScene()
    {
        //NetworkManagerSpecific.ServerChangeScene("BoardScene");
    }

    private void Update()
    {
        Debug.Log("_localController.NetworkType ");
        if (!_isConnected && _localController.NetworkType == "LAN")
        {
            _timeToRefreshMatch -= Time.deltaTime;
            if (_timeToRefreshMatch < 0)
            {
                RefreshMatches();

                _timeToRefreshMatch = DiscoveryUpdatePeriod;
            }
        }
    }

    private void RefreshMatches()
    {
        // filter matches
        Debug.Log("Estou aqui");
        _matches.Clear();
        _optionMatchesList.Clear();
        foreach (var match in NetworkManagerSpecific.Discovery.broadcastsReceived.Values)
        {
            _optionMatchesList.Add(new Dropdown.OptionData(Encoding.Unicode.GetString(match.broadcastData)));
            /* if (_matches.Any(item => EqualsArray(item.broadcastData, match.broadcastData)))
            {
                continue;
            }*/

            //Debug.Log(match.serverAddress);

            _matches.Add(match);
        }

        

        networkMatchesDropwork.AddOptions(_optionMatchesList);

        /*int i = 0;
        foreach (var match in _matches)
        {
            if (i >= 10)
            {
                break;
            }

            string matchName = Encoding.Unicode.GetString(match.broadcastData);
            //Debug.Log(matchName);

            _currentMatchesData[i] = match;

            _currentMatches[i].SetActive(true);
            _currentMatches[i].GetComponentInChildren<Text>().text = "Join match: " + matchName;
            i++;
        }
        for (; i < _currentMatches.Count; i++)
        {
            _currentMatches[i].SetActive(false);
        }*/

        Debug.Log(_matches);
    }

    public void CreateLanMatch()
    {
        NetworkManagerSpecific.Discovery.StopBroadcast();

        _roomName = "Natalia";
        NetworkManagerSpecific.Discovery.broadcastData = "Natalia";
        NetworkManagerSpecific.Discovery.StartAsServer();

        NetworkManagerSpecific.singleton.StartHost();

        Debug.Log("Estou conectada");
        _isConnected = true;
    }

}
