using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] GameObject playerInfoPrefab;
    public void StartLobby()
    {
        GameObject info = Instantiate(playerInfoPrefab);
        info.GetComponent<NetworkObject>().Spawn(false);
    }    
}
