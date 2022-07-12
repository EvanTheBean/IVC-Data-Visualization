using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(RelayLobbyManager))]
public class UsernameManager : MonoBehaviour
{
    string username = "";
    [SerializeField] TextMeshProUGUI nameInput;
    RelayLobbyManager lobbyManager;

    private void Awake()
    {
        lobbyManager = GetComponent<RelayLobbyManager>();
        username = PlayerPrefs.GetString("username");
        nameInput.text = username;
    }

    public void JoinLobby()
    {
        if (username == "") return;
        PlayerPrefs.SetString("username", username);
        lobbyManager.Join();
    }

    public void OnLobbyJoin()
    {

    }
}
