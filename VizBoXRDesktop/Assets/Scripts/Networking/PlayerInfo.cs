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
public class PlayerEvent : UnityEvent<string, Activity> { };

public class PlayerInfo : NetworkBehaviour
{
    Dictionary<ulong, (string, Activity)> players = new Dictionary<ulong, (string, Activity)>();
    NetworkVariable<int> playerCount = new NetworkVariable<int>(0);

    public PlayerEvent OnPlayerJoin;
    public PlayerEvent OnPlayerLeave;
    public PlayerEvent OnChangeActivity;

    private void Start()
    {
        NetworkManager.OnClientDisconnectCallback += LeaveLobbyServerRpc;
    }

    [ServerRpc(RequireOwnership = false)]
    void JoinLobbyServerRpc(ulong id, string username)
    {
        players.Add(id, (username, Activity.Annotating));
        Debug.Log(username + " joined the lobby.");
        playerCount.Value++;
        OnPlayerJoin.Invoke(username, Activity.Annotating);
    }

    [ServerRpc(RequireOwnership = false)]
    void LeaveLobbyServerRpc(ulong id)
    {
        Debug.Log(players[id].Item1 + " left the lobby.");
        OnPlayerLeave.Invoke(players[id].Item1, Activity.Annotating);
        players.Remove(id);
        playerCount.Value--;
        
    }

    [ServerRpc(RequireOwnership = false)]
    void ChangeDisplayedActivityServerRpc(ulong id, Activity activity)
    {
        (string, Activity) playerData = players[id];
        playerData.Item2 = activity;
        players[id] = playerData;
        OnChangeActivity?.Invoke(playerData.Item1, activity);
        Debug.Log(playerData.Item1 + " switched to activity " + activity.ToString());
    }

}
