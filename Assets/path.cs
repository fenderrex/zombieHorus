using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class path : MonoBehaviour {
    public Dictionary<Vector3, GameObject> nodes = new Dictionary<Vector3, GameObject>();
    public Dictionary<Vector3, GameObject> openSet = new Dictionary<Vector3, GameObject>();
    public Dictionary<Vector3, GameObject> closeSet = new Dictionary<Vector3, GameObject>();
    public GameObject scanner;
    public Dictionary<string, string> settings = new Dictionary<string, string>();
    public GameObject start;
    public GameObject end;
    public bool renderTrail;
    public float spawnSpeed=0;
    // Use this for initialization
    void Start()
    {
        
        settings.Add("renderTrail", renderTrail ? "true" : "false");//set settings
        StartCoroutine("CreateWorld");//start rendering scaner
    }

    // Update is called once per frame
    void Update()
    {
        Settings();
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
        for (float x = pos1.x; x < pos2.x + 1; x++)
        {
            yield return new WaitForSeconds(spawnSpeed);
            for (float y = pos1.y; y < pos2.y + 1; y++)
            {
                

                for (float z = 0; z < pos2.z + 1; z++)
                {
                   
                    GameObject block = Instantiate(scanner, Vector3.zero, scanner.transform.rotation) as GameObject;
                    block.transform.parent = transform;
                    block.transform.localPosition = new Vector3(x, y, z);
                    
                    if (first == true)
                    {
                        first = false;
                        //start.transform.position
                        Scanner script = block.GetComponent(typeof(Scanner)) as Scanner;
                        script.gscore = 0;
                        script.fscore = Vector3.Distance(pos1, pos2);
                    }

                    nodes[new Vector3(x, y, z)] = block;

                }
                yield return new WaitForSeconds(spawnSpeed);
            }
        }
    }

}
