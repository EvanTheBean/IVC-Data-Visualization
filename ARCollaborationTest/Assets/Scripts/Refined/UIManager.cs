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

    int warningCooldown = 0;

    public void DisplayWarning(string warning)
    {
        if (warningCooldown > 0)
        {
            warningCooldown = 0;
            return;
        }

        DebugCanvas.Instance.Log("DisplayWarning");
        warningPanel.SetActive(true);
        warningText.text = warning;
        warningCooldown++;
    }
}
