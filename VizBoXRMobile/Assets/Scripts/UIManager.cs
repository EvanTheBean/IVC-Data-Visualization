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

    int warningCooldown = 0;

    public void DisplayWarning(string warning)
    {
        if (warningCooldown > 0)
        {
            warningCooldown = 0;
            return;
        }

        warningPanel.SetActive(true);
        warningText.text = warning;
        warningCooldown++;
    }
}
