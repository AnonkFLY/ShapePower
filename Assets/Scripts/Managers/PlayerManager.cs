using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerManager 
{
    [SerializeField]
    private GameObject[] playerPrefab;
    public PlayerController CreatePlayer(RoleBase data)
    {
        var player = GameObject.Instantiate(playerPrefab[data.id]);
        var controller = player.GetComponent<PlayerController>();
        controller.InitPlayerData(data);
        return controller;
    }
}
