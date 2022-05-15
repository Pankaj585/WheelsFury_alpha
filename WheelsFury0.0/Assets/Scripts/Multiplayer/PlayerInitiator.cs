using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerInitiator : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] List<Transform> spawnPoints;

    [HideInInspector] public GameObject localPlayerObject;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnPoints == null || spawnPoints.Count < 2)
            return;

        int localPlayerNumber = 0;
        foreach(Player p in PhotonNetwork.PlayerList)
        {
            if (!p.IsLocal)
                localPlayerNumber++;
            else
            {
                localPlayerObject = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[localPlayerNumber].position, spawnPoints[localPlayerNumber].rotation);
                localPlayerObject.GetComponentInChildren<PlayerNetworkManager>().SetCamera(true);
                localPlayerObject.GetComponent<PlayerID>().SetID(localPlayerNumber);
                break;
            }

        }

        
    }   
}
