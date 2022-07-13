using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// === Username Manager === //
// Handles the username input and also remembers last username entered

[RequireComponent(typeof(RelayLobbyManager))]
public class UsernameManager : MonoBehaviour
{
    string username = "";
    [SerializeField] TMP_InputField nameInput;
    RelayLobbyManager lobbyManager;

    private void Start()
    {
        lobbyManager = GetComponent<RelayLobbyManager>();
        username = PlayerPrefs.GetString("username");
        nameInput.text = username;
    }


    // Called by join lobby button
    public void JoinLobby()
    {
        username = nameInput.text;
        if (username == "") return;
        PlayerPrefs.SetString("username", username);
        lobbyManager.Join();
    }

    public string GetUsername()
    {
        return username;
    }
}
