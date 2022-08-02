using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

// MOBILE VERSION

// === Network Grapher === // ( NETWORK OBJECT )
// Purpose: Communicate data that isn't specific to graphs
//
//      Currently communicates:
//          Skybox
//
// Note: Desktop version of this script also manages network spawning for graphs.


public class NetworkGrapher : NetworkBehaviour
{
    SkyboxValues skyboxVals; //values to set the skybox shader to

    private void Start()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += SetupSceneSkybox;
    }

    [ClientRpc] 
    void SendSkyboxClientRpc(SkyboxValues skybox) // Called by host (see Desktop version of NetworkGrapher)
    {
        skyboxVals = skybox;
        SetupSkybox(skybox);
    }
    
    void SetupSceneSkybox(Scene scene, LoadSceneMode mode) // Called on scene load
    {
        if (scene.name != "AR") SetupSkybox(skyboxVals);
    }
    
    void SetupSkybox(SkyboxValues skybox) // Sets skybox material variables
    {
        RenderSettings.skybox.SetColor("_Sky_Color", skybox.skyColor);
        RenderSettings.skybox.SetColor("_Horizon_Color", skybox.horizonColor);
        RenderSettings.skybox.SetFloat("_Stars", skybox.stars);
    }
}
