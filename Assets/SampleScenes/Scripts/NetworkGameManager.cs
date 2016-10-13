using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;
using System.Collections;
using System.Collections.Generic;

public class NetworkGameManager : NetworkBehaviour
{
    static public List<NetworkSpaceship> sShips = new List<NetworkSpaceship>();
    static public NetworkGameManager sInstance = null;

    public GameObject uiScoreZone;
    public Font uiScoreFont;
    
    [Header("Gameplay")]
    //Those are sorte dby level 0 == lowest etc...
    public Vector3[] spawnPointsT1;//todo this should be in a prefab
    public Vector3[] spawnPointsT2;
    [Tooltip("team one")]
    public GameObject[] spawnT1;
    [Tooltip("team two")]
    public GameObject[] spawnT2;
    [Header("Enviorment")]

    public AnimationCurve timeScale;
    public AnimationCurve[] panetMovment;


    [Space]
    public Light sun;
    public Light moon;
    public int mapSize=500;
    public ParticleSystem stars;
    //protected bool _spawningAsteroid = true;
    protected bool _spawningMobs = true;
    public float _time = 0;
    public float _delta = 0;
    bool day = true;
    public float tick = 1;
    protected bool _running = true;
    public virtual bool SpawnPlayer(GameObject playerBuild,Vector3[] sl)
    {
        //playerBuild.;
        return SpawnPlayer(playerBuild, sl[Random.Range(0, sl.Length)]);
        
    }
    public virtual bool SpawnPlayer(GameObject playerBuild,Vector3 pos)
    {

        return false;
    }
    void Awake()
    {
        print("awake");
        _delta = Time.time;
        sInstance = this;

    }

    void Start()
    {
        
        print("2");
        if (isServer)
        {
            print("3");
            
        }

        //for(int i = 0; i < sShips.Count; ++i)
        //{
        //    sShips[i].Init();
        //}
    }

    [ServerCallback]
    void FixedUpdate() {
        _delta = Time.time - _delta;
        _time += Time.time * (tick * .03f) * Time.deltaTime;
        if (_time > 24)
            _time = 0;

    }
    [ServerCallback]
    void Update()
    {
        //panetMovment
        StartCoroutine(SpawnCoroutine());
        //if (!_running || sShips.Count == 0)
        //   return;
        print("update");
        bool allDestroyed = true;
        for (int i = 0; i < sShips.Count; ++i)
        {
            allDestroyed &= (sShips[i].lifeCount == 0);
        }

        if(allDestroyed)
        {
            StartCoroutine(ReturnToLoby());
        }
        _delta = Time.time;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        print("4");
        foreach (GameObject obj in spawnT1)
        {
            ClientScene.RegisterPrefab(obj);
        }
    }

    IEnumerator ReturnToLoby()
    {
        _running = false;
        yield return new WaitForSeconds(3.0f);
        LobbyManager.s_Singleton.ServerReturnToLobby();
    }
    IEnumerator SpawnCoroutine()
    {
        moon.transform.position = new Vector3(panetMovment[0].Evaluate(Time.time * timeScale.Evaluate(Time.time)), panetMovment[1].Evaluate(Time.time * timeScale.Evaluate(Time.time)), panetMovment[2].Evaluate(Time.time * timeScale.Evaluate(Time.time)));//   sunMovment
        sun.transform.position = new Vector3(panetMovment[0].Evaluate(Time.time * timeScale.Evaluate(Time.time)), -panetMovment[1].Evaluate(Time.time * timeScale.Evaluate(Time.time)), panetMovment[2].Evaluate(Time.time * timeScale.Evaluate(Time.time)));//   sunMovment

        const float MIN_TIME = 5.0f;
        const float MAX_TIME = 10.0f;
        while (_time > 12 &(_spawningMobs))
        {
                
            stars.gameObject.active = true;
            stars.Play();
            if (Mathf.Abs(panetMovment[1].Evaluate(Time.time * timeScale.Evaluate(Time.time) )+panetMovment[1].Evaluate(Time.time * timeScale.Evaluate(Time.time)))>mapSize-(mapSize*.1f)) {


            }
            foreach (Vector3 obj in spawnPointsT2)
            {
                Collider[] testpoint= Physics.OverlapSphere(obj, 2.0f);
                if (testpoint.Length > 0) //test if free to spawn  
                {
                    //spawn point blocked
                    print("the blast doors are holding!");//not what this code really means....

                }
                else
                {
                    GameObject anamal = spawnT2[Random.Range(0, spawnT2.Length)];
                    anamal.transform.position = obj;
                    anamal.active = true;
                    anamal.GetComponent(typeof(AI));
                    print("the night is atacking!!");
                    break;
                }
               

            }
            yield return new WaitForSeconds(Random.Range(MIN_TIME, MAX_TIME));
            

        }
        while (_time < 12)
        {
            //stars.Stop();
            //stars.enableEmission = false;
            stars.gameObject.active = false;
            
            yield return new WaitForSeconds(Random.Range(MIN_TIME, MAX_TIME));
            print("the day is healing!!");
        }

    }
    IEnumerator AsteroidCoroutine()
    {
        const float MIN_TIME = 5.0f;
        const float MAX_TIME = 10.0f;

        while (_spawningMobs)
        {
            yield return new WaitForSeconds(Random.Range(MIN_TIME, MAX_TIME));

            Vector2 dir = Random.insideUnitCircle;
            Vector3 position = Vector3.zero;

            if(Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {//make it appear on the side
                position = new Vector3( Mathf.Sign(dir.x)* Camera.main.orthographicSize * Camera.main.aspect, 
                                        0, 
                                        dir.y * Camera.main.orthographicSize);
            }
            else
            {//make it appear on the top/bottom
                position = new Vector3(dir.x * Camera.main.orthographicSize * Camera.main.aspect, 
                                        0,
                                        Mathf.Sign(dir.y) * Camera.main.orthographicSize);
            }

            //offset slightly so we are not out of screen at creation time (as it would destroy the asteroid right away)
            position -= position.normalized * 0.1f;
            

            GameObject ast = Instantiate(spawnT1[spawnT1.Length - 1], position, Quaternion.Euler(Random.value * 360.0f, Random.value * 360.0f, Random.value * 360.0f)) as GameObject;

            NetworkAsteroid asteroid = ast.GetComponent<NetworkAsteroid>();
            asteroid.SetupStartParameters(-position.normalized * 1000.0f, Random.insideUnitSphere * Random.Range(500.0f, 1500.0f));

            NetworkServer.Spawn(ast);
        }
    }


    public IEnumerator WaitForRespawn(NetworkSpaceship ship)
    {
        yield return new WaitForSeconds(4.0f);

        ship.Respawn();
    }
}
