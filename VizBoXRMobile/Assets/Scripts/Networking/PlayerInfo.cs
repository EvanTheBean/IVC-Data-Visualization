using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Netcode;

// MOBILE VERSION

// === Player Info === // ( NETWORK OBJECT )
// Purpose: Send user's username and current activity to host
//
// Note: Instantiated by NetworkManager (when spawned by host)

public class PlayerInfo : NetworkBehaviour
{
    public string username { get; private set; } = "";

    ulong id; // unique id assigned by Unity Networking
    Activity currentActivity = Activity.Annotating;

    void Start()
    {
        DontDestroyOnLoad(this); // (all network objects need to be DontDestroyLoad to prevent network errors)

        if (!IsHost) 
        {
            id = NetworkManager.LocalClientId;
            username = FindObjectOfType<LobbyJoinUIHandler>().GetUsername();
            JoinLobbyServerRpc(id, username); // Tells the server that you joined
        }
    }

    #region Lobby Join / Leave
    [ServerRpc(RequireOwnership = false)]
    void JoinLobbyServerRpc(ulong id, string username)
    {
        // Tells the server that the user joined the lobby
        // See Desktop version of script for implementation
    }

    [ServerRpc(RequireOwnership = false)]
    void LeaveLobbyServerRpc(ulong id)
    {
    }
    #endregion

    [ClientRpc]
    void StartLobbyClientRpc()
    {
        FindObjectOfType<RelayLobbyManager>().OnLobbyStart.Invoke();
    }

    #region Activity Switching
    public void SwitchActivity(Activity activity)
    {
        if (activity == currentActivity) return;

        currentActivity = activity;
        ChangeDisplayedActivityServerRpc(id, activity);
    }

    [ServerRpc(RequireOwnership = false)]
    void ChangeDisplayedActivityServerRpc(ulong id, Activity activity)
    {

    }
    #endregion
}

public enum Activity
{
    // Activity that the player is currently doing
    Annotating,
    AR,
    VR
}