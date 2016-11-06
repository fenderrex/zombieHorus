using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Scanner : MonoBehaviour {
    public char value = '-';
    public List<Vector3> camefrom = new List<Vector3>();
    public float fscore = 9999999999f;
    public float gscore= 9999999999f;
    public Color errorC = new Color();
    public Color openC = new Color();
    public Color closedC = new Color();
    public Color closedStaticC = new Color();
    public Color pathC = new Color();
    public Mesh trailMesh = new Mesh();
    public float destructTime = 1f;
    // Dictionary<Vector3, float> gScore = new Dictionary<Vector3, float>();
    // Use this for initialization
    void Start() {
        //Cloak();
        StartCoroutine(disgard(destructTime));
    }

    // Update is called once per frame
    void Update() {
        //  dic1.Count == dic2.Count && !dic1.Except(dic2).Any()

    }
    public List<Vector3> neighbors()
    {
        List < Vector3 > test= new List<Vector3>();
        for (int px = -1; px <= 1; px++)
        {
            for (int py = -1; py <= 1; py++)
            {
                for (int pz = -1; pz <= 1; pz++)
                {
                    //todo see if the ground is "walkable" given a walking script that is deffined in the parrent
                    if ((px == 0 & py == 0 & pz == 0) == false)//don't test the test block
                    {
                        test.Add( this.transform.position + new Vector3((float)px, (float)py, (float)pz));
         }}}}

        return test;
    }

    public void Cloak()
    {
        Mesh mesh = new Mesh();
        this.GetComponent<MeshFilter>().mesh = mesh;
    }
    public void Show()
    {
        
        this.GetComponent<MeshFilter>().mesh = trailMesh;
    }
    public char Look(Vector3 point, System.Action<char> var)
    {
        //Collider[] hitColliders = Physics.OverlapSphere(point, .9f);
        //if (hitColliders.Length >= 0)
        /// {
        //    value= 'X';
        // }
        //        new Physics.OverlapBox(point,new  Vector3(.8f, .8f, .8f), new Quaternion.Euler(new Vector3(0f,0f,0f)), playerLayer);

        //(string)point.x + "-" + (string)point.y + "-" + (string)point.z;
        transform.position = point;
       // print("looking at");
        //print(point);
     
        StartCoroutine(look((int)point.x,(int)point.y,(int)point.z, (x) => var(x)));
       // color co = this.GetComponent< color >() as color;
       // co.ObjectColor = (value == '-' ? closedC : openC);
        //print(value);
        return value;//This is a non threadsefe mode!!!! 

    }
    public IEnumerator disgard(float time)
    {
        yield return new WaitForSeconds(time);

        BoxCollider a =GetComponent<BoxCollider>() as BoxCollider;
        a.enabled = false;
    }
    public IEnumerator look(int x, int y, int z, System.Action<char> var)
    {

        transform.localPosition = new Vector3((float)x, (float)y, (float)z);
        yield return new WaitForSeconds(1f);
        //Scaner node = scaner.GetComponent(typeof(Scaner)) as Scaner;
        var(value);
        //return new repNode { length = 4, height = 5, width = 4,};
    }
    void OnTriggerEnter(Collider other)
    {
        //print(other.transform.name);
        //print(transform.position);
        //print(other.transform.position);
        print("enter");
        color co = this.GetComponent<color>() as color;
        
        print(other.transform.name);
        path pth = GetComponentInParent<path>();
        Rigidbody bob=other.gameObject.GetComponent<Rigidbody>();
        //todo dont triger on other test nodes
        if (bob.isKinematic)//if the prop is not ment to move keep it in the data bace
        {//dynaimic
            co.ObjectColor = closedC;
            value = 'x';
            
        }
        else
        {
            
            value = 'X';
            co.ObjectColor = closedStaticC;

        }

        //lock sets into static frame 
        pth.closeSet.Add(transform.position);// = gameObject;
        if (pth.nodes.ContainsKey(transform.position)==true)//todo make into a function
        {
            pth.nodes[transform.position] = gameObject;
        }
        else
        {
            pth.nodes.Add(transform.position, gameObject);//make sure the node is in the list
        }
        pth.openSet.Remove(transform.position);
        //  print(other.transform.tag);
    }
    void OnTriggerExit(Collider other)
    {
        value = '_';
        print("exiting");
        color co = this.GetComponent<color>() as color;
        co.ObjectColor = openC;
        print(other.transform.tag);
        path pth = GetComponentInParent<path>();
        pth.openSet.Add(transform.position); //
        if (pth.nodes.ContainsKey(transform.position)==true)
        {
            pth.nodes[transform.position] = gameObject;
        }
        else
        {
            pth.nodes.Add(transform.position, gameObject);//make sure the node is in the list
        }
        pth.closeSet.Remove(transform.position);

    }
    void OnCollisionStay(Collision other)
    {
        print(other.transform.tag);
        //value = 'X';// blocked

    }
    void OnCollisionExit(Collision other)
    {
        value = '_';// newground this may be usefull for physics
    }

    //settings manager

  //  dic1.Count == dic2.Count && !dic1.Except(dic2).Any()


}
