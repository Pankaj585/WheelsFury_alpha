using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerID : MonoBehaviour
{
    PhotonView PV; 
    public int ID { get; private set; }

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    public void SetID(int ID)
    {
        this.ID = ID;
        PV.RPC("SyncID", RpcTarget.All, (object)ID);
    }

    [PunRPC]
    void SyncID(object ID)
    {
        this.ID = (int)ID;
    }
}
