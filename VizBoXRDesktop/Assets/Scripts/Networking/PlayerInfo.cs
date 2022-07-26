using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Events;
using System;
// DESKTOP

public enum Activity
{
    Annotating,
    AR,
    VR
}

[Serializable]
public class PlayerEvent : UnityEvent<ulong, string, Activity> { };

public class PlayerInfo : NetworkBehaviour
{
    Dictionary<ulong, (string, Activity)> players = new Dictionary<ulong, (string, Activity)>();

    public PlayerEvent OnPlayerJoin;
    public PlayerEvent OnPlayerLeave;
    public PlayerEvent OnChangeActivity;

    private void Start()
    {
        DontDestroyOnLoad(this);
        NetworkManager.OnClientDisconnectCallback += LeaveLobbyServerRpc;
    }

    [ServerRpc(RequireOwnership = false)]
    void JoinLobbyServerRpc(ulong id, string username)
    {
        players.Add(id, (username, Activity.Annotating));
        Debug.Log(username + " joined the lobby.");
        OnPlayerJoin.Invoke(id, username, Activity.Annotating);
    }

    [ServerRpc(RequireOwnership = false)]
    void LeaveLobbyServerRpc(ulong id)
    {
        Debug.Log(players[id].Item1 + " left the lobby.");
        OnPlayerLeave?.Invoke(id, players[id].Item1, Activity.Annotating);
        players.Remove(id);        
    }

    [ServerRpc(RequireOwnership = false)]
    void ChangeDisplayedActivityServerRpc(ulong id, Activity activity)
    {
        (string, Activity) playerData = players[id];
        playerData.Item2 = activity;
        players[id] = playerData;
        OnChangeActivity?.Invoke(id, playerData.Item1, activity);
        Debug.Log(playerData.Item1 + " switched to activity " + activity.ToString());
    }

    public void StartLobby()
    {
        StartLobbyClientRpc();
    }

    [ClientRpc]
    void StartLobbyClientRpc()
    {
    }

}
