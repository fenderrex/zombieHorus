using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
[RequireComponent(typeof(NetworkTransform))]
[RequireComponent(typeof(Rigidbody))]
public class player : NetworkSpaceship {
    //CreateBullets,Respawn,kill,LocalDestroy,EnableSpaceShip,FixedUpdate,Update,OnDestroy,Init,Start,Awake
    // Use this for initialization
    public override void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        Renderer[] rends = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rends)
            r.material.color = color;

        //We don't want to handle collision on client, so disable collider there
        _collider.enabled = isServer;


        if (NetworkGameManager.sInstance != null)
        {//we MAY be awake late (see comment on _wasInit above), so if the instance is already there we init
            Init();
        }
    }


    public override void Init()
    {
        if (_wasInit)
            return;

        GameObject scoreGO = new GameObject(playerName + "score");
        scoreGO.transform.SetParent(NetworkGameManager.sInstance.uiScoreZone.transform, false);
        _scoreText = scoreGO.AddComponent<Text>();
        _scoreText.alignment = TextAnchor.MiddleCenter;
        _scoreText.font = NetworkGameManager.sInstance.uiScoreFont;
        _scoreText.resizeTextForBestFit = true;
        _scoreText.color = color;
        _wasInit = true;

        UpdateScoreLifeText();
    }
    public override void CreateBullets()
    {

    }
    // Update is called once per frame
    public override void Update () {
	
	}
    // =========== NETWORK FUNCTIONS
    [Command]
    public override void CmdFire(Vector3 position, Vector3 forward, Vector3 startingVelocity)
    {
        if (!isClient) //avoid to create bullet twice (here & in Rpc call) on hosting client
            CreateBullets();

        RpcFire();
    }

    //
    [Command]
    public override void CmdCollideAsteroid()
    {
        Kill();
    }

    [ClientRpc]
    public override void RpcFire()
    {
        CreateBullets();
    }


    //called on client when the player die, spawn the particle (this is only cosmetic, no need to do it on server)
    [ClientRpc]
    public override void RpcDestroyed()
    {
        LocalDestroy();
    }

    [ClientRpc]
    public override void RpcRespawn()
    {
        EnableSpaceShip(true);

        killParticle.gameObject.SetActive(false);
        killParticle.Stop();
    }
}

