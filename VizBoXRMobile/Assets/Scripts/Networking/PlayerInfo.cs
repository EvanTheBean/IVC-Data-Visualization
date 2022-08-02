using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Netcode;

// MOBILE
public enum Activity
{
    Annotating,
    AR,
    VR
}

public class PlayerInfo : NetworkBehaviour
{
    public string username { get; private set; } = "";

    ulong id;
    Activity currentActivity = Activity.Annotating;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        if (!IsHost)
        {
            id = NetworkManager.LocalClientId;
            username = FindObjectOfType<LobbyJoinUIHandler>().GetUsername();
            JoinLobbyServerRpc(id, username);
        }
    }

    private void OnPlayerJoin(int previousValue, int newValue)
    {
    }

    public void SwitchActivity(Activity activity)
    {
        if (activity == currentActivity) return;

        currentActivity = activity;
        ChangeDisplayedActivityServerRpc(id, activity);
    }

    [ServerRpc(RequireOwnership = false)]
    void JoinLobbyServerRpc(ulong id, string username)
    {

    }

    [ServerRpc(RequireOwnership = false)]
    void ChangeDisplayedActivityServerRpc(ulong id, Activity activity)
    {

    }

    [ServerRpc(RequireOwnership = false)]
    void LeaveLobbyServerRpc(ulong id)
    {
    }


    [ClientRpc]
    void StartLobbyClientRpc()
    {
        FindObjectOfType<RelayLobbyManager>().OnLobbyStart.Invoke();
    }
}