
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkLobbyManager
{
    NetworkLobbyManager n = null;
    void Start() {
 
       n = new NetworkLobbyManager();
    }
    void Update()
    {
        //ready=n.CheckReadyToBegin();

    }
}