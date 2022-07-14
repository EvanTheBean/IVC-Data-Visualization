using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Events;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] GameObject playerInfoPrefab;

    public UnityEvent OnLobbyStart;

    public void StartLobby()
    {
        GameObject info = Instantiate(playerInfoPrefab);
        info.GetComponent<NetworkObject>().Spawn(false);
        OnLobbyStart.Invoke();
    }    
}
