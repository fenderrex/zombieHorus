﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class path : MonoBehaviour {
    public Dictionary<Vector3, GameObject> nodes = new Dictionary<Vector3, GameObject>();
    public List<Vector3> openSet = new List<Vector3>();
    public List<Vector3> closeSet = new List<Vector3>();
    public GameObject scanner;
    public Dictionary<string, string> settings = new Dictionary<string, string>();
    public GameObject start;
    public GameObject end;
    public bool renderTrail;
    public float spawnSpeed=0;
    // Use this for initialization
    void Start()
    {
        //openSet.Add(start.Transform,)
        settings.Add("renderTrail", renderTrail ? "true" : "false");//set settings
       
    }
    void Awake()
    {

        StartCoroutine("CreateWorld");//start rendering scaner
    }

    // Update is called once per frame
    void Update()
    {
        Settings();
        Astar();
       
    }
    void Astar() {

   


    }
    void Settings()
    {


        if (settings["renderTrail"] != (renderTrail ? "true" : "false"))
        {
            settings["renderTrail"] = (renderTrail ? "true" : "false");
            print("settings update");
            if (settings["renderTrail"] == "false")
            {

                foreach (KeyValuePair<Vector3, GameObject> entry in nodes)
                {
                    Scanner script = entry.Value.GetComponent(typeof(Scanner)) as Scanner;
                    script.Cloak();

                }
            }
            else
            {
                foreach (KeyValuePair<Vector3, GameObject> entry in nodes)
                {
                    Scanner script = entry.Value.GetComponent(typeof(Scanner)) as Scanner;
                    script.Show();
                }
            }
        }

    }
    IEnumerator CreateWorld()
    {
        Vector3 pos1 = start.transform.position;
        Vector3 pos2 = end.transform.position;
        bool first = true;


        //int subx=
        for (float x = (pos1.x > pos2.x ? pos2.x : pos1.x); x <= (pos1.x > pos2.x ? pos1.x : pos2.x);x++)
        {
            //x=(pos1.x > pos2.x ? +1 : -1);
          //  yield return new WaitForSeconds(spawnSpeed);
            for (float y = (pos1.y> pos2.y ? pos2.y : pos1.y); y <= (pos1.y > pos2.y ? pos1.y : pos2.y); y+=3)
            {
                
                for (float z = (pos1.z > pos2.z ? pos2.z : pos1.z); z <= (pos1.z > pos2.z ? pos1.z : pos2.z); z++)
                {
                    //      yield return new WaitForSeconds(spawnSpeed);
                    scanerat(scanner, pos1, pos2, x, y, z);


            }
            yield return new WaitForSeconds(spawnSpeed);
        }
    }

}
    void scanerat(GameObject scanner, Vector3 from, Vector3 to, float x, float y, float z)
    {
        ///if (Vector3.Distance(scanner.transform.position + new Vector3(x, y, z),))
      //  {
            GameObject block = Instantiate(scanner, Vector3.zero, scanner.transform.rotation) as GameObject;
            block.transform.parent = transform;
            block.transform.localPosition = new Vector3(x, y, z);
            if (Vector3.Distance(from, new Vector3(x, y, z)) < 1f)

            {
                Scanner script = block.GetComponent(typeof(Scanner)) as Scanner;
                script.gscore = 0;
                script.fscore = Vector3.Distance(from, to);
            }

            nodes[new Vector3(x, y, z)] = block;
        }
   // }
}
