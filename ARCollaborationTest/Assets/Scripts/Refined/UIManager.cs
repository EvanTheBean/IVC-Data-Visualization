using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using System;

public class UIManager : MonoBehaviour
{
    static public UIManager Singleton;

    [SerializeField] GameObject warningPanel;
    [SerializeField] TextMeshProUGUI warningText;

    [Space(5)]
    [SerializeField] GameObject lobbyPanel;

    private void Awake()
    {
        if (Singleton != null && Singleton != this)
        {
            Destroy(this);
        }
        else
        {
            Singleton = this;
        }
    }

    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += HideLobbyPanel;
    }

    private void HideLobbyPanel(ulong obj)
    {
        lobbyPanel.SetActive(false);
    }

    public void DisplayWarning(string warning)
    {
        DebugCanvas.Instance.Log("DisplayWarning called");
        warningPanel.SetActive(true);
        warningText.text = warning;
    }
}
