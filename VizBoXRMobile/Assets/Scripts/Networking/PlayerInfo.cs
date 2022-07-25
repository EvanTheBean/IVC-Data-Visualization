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

    NetworkVariable<int> playerCount = new NetworkVariable<int>(0);
    // Start is called before the first frame update
    void Start()
    {
        playerCount.OnValueChanged += OnPlayerJoin;
        DontDestroyOnLoad(this);
        if (!IsHost)
        {
            id = NetworkManager.LocalClientId;
            username = FindObjectOfType<UsernameManager>().GetUsername();
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

    private void OnApplicationQuit()
    {
        playerCount.Dispose();
    }

}