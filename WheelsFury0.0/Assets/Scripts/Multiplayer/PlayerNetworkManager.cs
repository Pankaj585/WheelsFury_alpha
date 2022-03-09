using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerNetworkManager : MonoBehaviourPunCallbacks
{

    [SerializeField] GameObject cameraObject;

    private void Awake()
    {
        cameraObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCamera(bool isActive)
    {
        cameraObject.SetActive(isActive);
    }
}
