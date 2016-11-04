using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class path3 : MonoBehaviour {
    public GameObject scanner;
    //public Dictionary<Vector3, GameObject> nodes = new Dictionary<Vector3, GameObject>();
    public GameObject start;
    public GameObject end;
    public bool renderTrail;
    public Dictionary<Vector3, GameObject> nodes = new Dictionary<Vector3, GameObject>();
    public Dictionary<Vector3, GameObject> openSet = new Dictionary<Vector3, GameObject>();
    public Dictionary<Vector3, GameObject> closeSet = new Dictionary<Vector3, GameObject>();
    public Dictionary<Vector3, float> gScore = new Dictionary<Vector3, float>();
    public Dictionary<Vector3, float> fScore = new Dictionary<Vector3, float>();
    public Dictionary<string, string> settings = new Dictionary<string, string>();
    public Dictionary<string, string> settingsOld = new Dictionary<string, string>();

    // Use this for initialization
    void Start()
    {
        // char c='-';
        StartCoroutine("CreateWorld");



        settings.Add("renderTrail", renderTrail ? "true" : "false");
        //GameObject block = Instantiate(scanner, scanner.transform.position, scanner.transform.rotation) as GameObject;
        //block.GetComponent(typeof(Scanner)) as Scanner;

        //Scanner script = block.GetComponent(typeof(Scanner)) as Scanner;
        //script.Look(start.transform.position, (x) => c = x);

        // block1.transform.position = start.transform.position;
        //openSet.Add(block.transform.position, block);





        //gScore.Add(start.transform.position, 0);
        //fScore.Add(start.transform.position, Vector3.Distance(start.transform.position, end.transform.position));
    }



    public float spawnSpeed = 0;



    IEnumerator CreateWorld()
    {
        Vector3 pos1 = start.transform.position;
        Vector3 pos2 = end.transform.position;
        for (float x = pos1.x; x < pos2.x + 1; x++)
        {
            yield return new WaitForSeconds(spawnSpeed);
            for (float y = pos1.y; y < pos2.y + 1; y++)
            {
                yield return new WaitForSeconds(spawnSpeed);

                for (float z = 0; z < pos2.z + 1; z++)
                {
                    yield return new WaitForSeconds(spawnSpeed);

                    GameObject block = Instantiate(scanner, Vector3.zero, scanner.transform.rotation) as GameObject;
                    block.transform.parent = transform;
                    block.transform.localPosition = new Vector3(x, y, z);
                    nodes[new Vector3(x, y, z)] = block;


                }
            }
        }
    }







    // Update is called once per frame
    void Update()
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

    void deadend()
    {
        if (openSet.Count > 0)
        {
            Vector3 minName = new Vector3(0f, 0f, 0f);
            float min = 999999;
            foreach (KeyValuePair<Vector3, float> entry in fScore)
            {
                if (entry.Value <= min)
                {

                    min = entry.Value;
                    minName = entry.Key;
                }
            }

            if (minName == end.transform.position)
            {
                print("done");

            }
            char[] separatingChars = { ',' };

            //string[] words = minName.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
            closeSet.Add(minName, nodes[minName]);
            GameObject testpoint = openSet[minName];
            openSet.Remove(minName);
            Scanner script = scanner.GetComponent(typeof(Scanner)) as Scanner;
            Vector3 neighbortestpoint = new Vector3(0f, 0f, 0f);
            for (int px = -1; px <= 1; px++)
            {

                for (int py = -1; py <= 1; py++)
                {


                    for (int pz = -1; pz <= 1; pz++)
                    {
                        if ((px == 0 & py == 0 & pz == 0) == false)//don't test the test block
                        {
                            int x = px * 1;
                            int y = py * 1;
                            int z = pz * 1;
                            GameObject neighbor = closeSet[minName];//("," + .x + x).ToString() + "," + (closeSet[minName].y + y).ToString() + "," + (closeSet[minName].z + z).ToString();
                                                                    // words = neighbor.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                            neighbortestpoint = new Vector3(closeSet[minName].transform.position.x + x, closeSet[minName].transform.position.y + y, closeSet[minName].transform.position.z + z);
                            char c = '-';
                            char result = script.Look(neighbortestpoint, (a) => c = a);
                            print(neighbortestpoint);
                            // result = script.Look(neighbortestpoint);
                            print(result);
                            if (closeSet.ContainsKey(neighbortestpoint) || result == 'X')
                            {

                                continue;
                            }



                            float tentative_gScore = gScore[testpoint.transform.position] + Vector3.Distance(closeSet[minName].transform.position, new Vector3(closeSet[minName].transform.position.x + x, closeSet[minName].transform.position.y + y, closeSet[minName].transform.position.z + z));
                            if (openSet.ContainsKey(testpoint.transform.position) == false)
                            {
                                openSet.Add(testpoint.transform.position, testpoint);
                            }
                            else if (tentative_gScore <= gScore[neighbor.transform.position])
                            {//not a better match
                                continue;
                            }

                            script.camefrom.Add(neighbor.transform.position);
                            //gScore[neighbor.transform.position] = tentative_gScore;
                            //fScore[neighbor.transform.position] = tentative_gScore + Vector3.Distance(script.camefrom[neighbor.transform.position], closeSet[minName].transform.position);
                        }

                    }

                }


            }
            foreach (Vector3 point in script.camefrom)
            {
                print(point);
            }



            {
                //Scanner script = scanner.GetComponent(typeof(Scanner)) as Scanner;
                // script.Look(new Vector3(0f, 0f, 0f));
                // print(script.value);

            }



        }

    }
}
