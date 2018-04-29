using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardConfigurationGetter {


    private static string _configurationZoneName = "ConfigurationZone";


    public static BoardConfiguration getConfigurationObject()
    {

        Scene configurationZone = SceneManager.GetSceneByName(_configurationZoneName);
        GameObject configurationObject = configurationZone.GetRootGameObjects()[0];
        BoardConfiguration _boardConfiguration = configurationObject.GetComponent<BoardConfiguration>();
        return _boardConfiguration;
    }


}
