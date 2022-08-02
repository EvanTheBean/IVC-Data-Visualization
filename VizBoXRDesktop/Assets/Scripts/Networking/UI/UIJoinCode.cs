using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIJoinCode : MonoBehaviour
{
    RelayLobbyManager lobbyManager;
    TextMeshProUGUI joinCodeText;

    private void Awake()
    {
        lobbyManager = FindObjectOfType<RelayLobbyManager>();
        joinCodeText = GetComponent<TextMeshProUGUI>();
    }

    public void DisplayJoinCode()
    {
        joinCodeText.text = lobbyManager.GetJoinCode();
    }
}
