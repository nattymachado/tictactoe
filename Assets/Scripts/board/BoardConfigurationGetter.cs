using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardConfigurationGetter: MonoBehaviour {


    private static string _configurationZoneName = "ConfigurationZone";

    public static BoardConfiguration getConfigurationObject()
    {
        Scene ConfigurationZone = SceneManager.GetSceneByName(_configurationZoneName);
        GameObject ConfigurationObject = ConfigurationZone.GetRootGameObjects()[0];
        BoardConfiguration Configuration = ConfigurationObject.GetComponent<BoardConfiguration>();
        return Configuration;
    }


}
