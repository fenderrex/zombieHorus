using UnityEngine;
using UnityEngine.Networking;
public class PlayerObject : NetworkLobbyPlayer{
    NetworkLobbyPlayer n = new NetworkLobbyPlayer();
    PlayerController p = new PlayerController();
    public void OnClientEnterLobby() {
        print("in lobby");
    }
    // Use this for initialization
    void Start () {
        
        //n.OnClientReady(true);
        print("client ready!");
    }
	
	// Update is called once per frame
	void Update () {
        print("update");
	}
   
}
