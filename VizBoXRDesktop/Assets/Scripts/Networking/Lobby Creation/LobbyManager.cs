using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Events;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] GameObject playerInfoPrefab;
    PlayerInfo playerInfo;

    public UnityEvent OnLobbyStart;

    public void CreateLobby()
    {
        GameObject info = Instantiate(playerInfoPrefab);
        info.GetComponent<NetworkObject>().Spawn(false);
        playerInfo = info.GetComponent<PlayerInfo>();
        OnLobbyStart.Invoke();
    }

    public void StartLobby()
    {
        playerInfo.StartLobby();
    }
}
