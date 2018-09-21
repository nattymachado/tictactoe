using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Linq;
using System.Text;

public class NetworkMenu : MonoBehaviour {


    private string _roomName =  "";
    private bool _isConnected = false;
    public Button StartServerLan;
    public Button JoinLan;
    public float DiscoveryUpdatePeriod = 0.5f;
    private float _timeToRefreshMatch = 0;
    public Dropdown networkMatchesDropwork;
    private const int _maxRooms = 10;
    private List<NetworkBroadcastResult> _matches = new List<NetworkBroadcastResult>();
    private List<Dropdown.OptionData> _optionMatchesList = new List<Dropdown.OptionData>();
    private LocalController _localController;
    private Dictionary<int, NetworkBroadcastResult> _currentMatchesData = new Dictionary<int, NetworkBroadcastResult>();


    void Start()
    {
        _localController = NetworkConfigurationGetter.getConfigurationObject();
        AddListeners();


    }

    private void AddListeners()
    {
        StartServerLan.onClick.AddListener(CreateLanMatch);
        JoinLan.onClick.AddListener(JoinOnRoom);
    }

    

    public void LoadGameScene()
    {
        ///NetworkManagerSpecific.singleton.ServerChangeScene("WhoStartScene");
    }

    private void Update()
    {
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

    private bool CheckIfIsEqual(byte[] data1, byte[] data2)
    {
        Debug.Log(System.Text.Encoding.Default.GetString(data1));
        return System.Text.Encoding.Default.GetString(data1) == System.Text.Encoding.Default.GetString(data1);
    }

    private void RefreshMatches()
    {
        // filter matches
        _matches.Clear();
        _optionMatchesList.Clear();
        foreach (var match in NetworkManagerSpecific.Discovery.broadcastsReceived.Values)
        {
            if (_matches.Count >= 10)
            {
                break;
            }
            
            if (_matches.Any(item => CheckIfIsEqual(item.broadcastData, match.broadcastData)))
            {
                continue;
            }
            _optionMatchesList.Add(new Dropdown.OptionData(Encoding.Unicode.GetString(match.broadcastData)));
            _matches.Add(match);
        }


        networkMatchesDropwork.ClearOptions();
        networkMatchesDropwork.AddOptions(_optionMatchesList);

        if (_matches.Count == 0)
        {
            JoinLan.enabled = false;
            return;
        }
        else
        {
            JoinLan.enabled = true;
        }
        for (var i = 0; i < _matches.Count; i++)
        {
            _currentMatchesData[i] = _matches[i];
        }


    }

    public void CreateLanMatch()
    {
        NetworkManagerSpecific.Discovery.StopBroadcast();
        _roomName = Random.Range(100000, 1000000).ToString();
        NetworkManagerSpecific.Discovery.broadcastData = _roomName;
        NetworkManagerSpecific.Discovery.StartAsServer();
        NetworkManagerSpecific.singleton.StartHost();
        _isConnected = true;
        LoadGameScene();
    }

    private void JoinOnRoom()
    {
        var matchData = _currentMatchesData[networkMatchesDropwork.value];

        NetworkManagerSpecific.singleton.networkAddress = matchData.serverAddress;
        NetworkManagerSpecific.singleton.StartClient();

        NetworkManagerSpecific.Discovery.StopBroadcast();
        _isConnected = true;
        LoadGameScene();
    }

}
