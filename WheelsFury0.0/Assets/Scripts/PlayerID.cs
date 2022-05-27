using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerID : MonoBehaviour
{
    PhotonView PV;
    OrbSpawner orbSpawner;
    public int ID { get; private set; }

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        orbSpawner = FindObjectOfType<OrbSpawner>();
    }
    public void SetID(int ID)
    {
        PV.RPC("SyncID", RpcTarget.All, (object)ID);
    }

    [PunRPC]
    void SyncID(object ID)
    {
        this.ID = (int)ID;
        orbSpawner.AddMyReference(this);
    }
}
