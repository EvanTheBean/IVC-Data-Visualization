using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIPlayerList : MonoBehaviour
{
    PlayerInfo playerInfo;

    [SerializeField] GameObject playerNamePrefab;
    [SerializeField] Transform playerNameParent;
    Dictionary<ulong, GameObject> nameUIObjects = new Dictionary<ulong, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<LobbyManager>().OnLobbyStart.AddListener(SetUp);
    }

    private void SetUp()
    {
        playerInfo = FindObjectOfType<PlayerInfo>();
        playerInfo.OnPlayerJoin.AddListener(OnPlayerJoin);
        playerInfo.OnPlayerLeave.AddListener(OnPlayerLeave);
    }

    private void OnPlayerJoin(ulong id, string user, Activity activity)
    {
        GameObject obj = Instantiate(playerNamePrefab, playerNameParent);
        obj.GetComponentInChildren<TextMeshProUGUI>().text = user;
        nameUIObjects.Add(id, obj);
    }

    private void OnPlayerLeave(ulong id, string user, Activity activity)
    {
        Destroy(nameUIObjects[id]);
        nameUIObjects.Remove(id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
