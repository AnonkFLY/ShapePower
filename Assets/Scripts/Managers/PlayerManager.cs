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
        var player = GameObject.Instantiate(playerPrefab[data.id]).GetComponent<PlayerController>();
        player.InitPlayerData(data);
        return player;
    }
}
