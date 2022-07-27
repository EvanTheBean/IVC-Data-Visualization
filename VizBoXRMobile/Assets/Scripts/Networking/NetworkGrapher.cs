using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class NetworkGrapher : NetworkBehaviour
{
    SkyboxValues skyboxVals;

    private void Start()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += SetupSceneSkybox;
    }

    [ClientRpc]
    void SendSkyboxClientRpc(SkyboxValues skybox)
    {
        skyboxVals = skybox;
        SetupSkybox(skybox);
    }
    
    void SetupSceneSkybox(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "AR") SetupSkybox(skyboxVals);
    }

    void SetupSkybox(SkyboxValues skybox)
    {
        RenderSettings.skybox.SetColor("_Sky_Color", skybox.skyColor);
        RenderSettings.skybox.SetColor("_Horizon_Color", skybox.horizonColor);
        RenderSettings.skybox.SetFloat("_Stars", skybox.stars);
    }
}
