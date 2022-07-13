using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Netcode;
using System;

// DESKTOP

public enum Activity
{
    Annotating,
    AR,
    VR
}

public class PlayerInfo : NetworkBehaviour
{
    Dictionary<ulong, (string, Activity)> players = new Dictionary<ulong, (string, Activity)>();
    NetworkVariable<int> playerCount = new NetworkVariable<int>(0);

    private void Start()
    {
        playerCount.OnValueChanged += OnPlayerJoin;
        NetworkManager.OnClientDisconnectCallback += LeaveLobbyServerRpc;
    }

    private void OnPlayerJoin(int previousValue, int newValue)
    {
    }

    [ServerRpc(RequireOwnership = false)]
    void JoinLobbyServerRpc(ulong id, string username)
    {
        players.Add(id, (username, Activity.Annotating));
        Debug.Log(username + " joined the lobby.");
        playerCount.Value++;

        //TODO UI
    }

    [ServerRpc(RequireOwnership = false)]
    void LeaveLobbyServerRpc(ulong id)
    {
        Debug.Log(players[id].Item1 + " left the lobby.");
        players.Remove(id);
        playerCount.Value--;
        //TODO UI
    }

    [ServerRpc(RequireOwnership = false)]
    void ChangeDisplayedActivityServerRpc(ulong id, Activity activity)
    {
        (string, Activity) playerData = players[id];
        playerData.Item2 = activity;
        players[id] = playerData;
        Debug.Log(playerData.Item1 + " switched to activity " + activity.ToString());
    }

}
