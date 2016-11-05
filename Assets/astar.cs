using UnityEngine;
using System;
using System.Collections;

using System.Collections.Generic;
using System.Linq;
public class astar : MonoBehaviour {
    int[] e = new int[] { };
    public bool is3D = true;
    public GameObject block1;
    public GameObject scanner;
    public Transform to;

    //private int transform.position.x = 1, transform.position.y = 3, transform.position.z = 1, to.transform.position.x = 3, to.transform.position.y = 3, to.transform.position.z = 3;
    

    //public int transform.position.x = 100, transform.position.y = 100, transform.position.z = 100, to.transform.position.x = 100, to.transform.position.y = 100, to.transform.position.z = 100;
    public List<GameObject> astarpath;// = new List<GameObject>;
    public bool clean = true;
    // Use this for initialization
    void Start () {


        //unitTest_AStar();
    }

    // Update is called once per frame
    void Update() {
        
        //dataIn a=datain.matrix;
        //if (transform.position.x != transform.position.x || transform.position.y != transform.position.y || transform.position.z != transform.position.z || to.transform.position.x != to.transform.position.x || to.transform.position.y != to.transform.position.y || to.transform.position.z != to.transform.position.z) {
            
            for (int i=0;i< astarpath.Count;i++)
            {
                Destroy(astarpath[i]);
                Destroy(astarpath[i]);
                astarpath[i] = null;
                astarpath.Remove(astarpath[i]);
            }

           // transform.position.z = transform.position.z;
           // transform.position.y = transform.position.y;
           // transform.position.x = transform.position.x;
           // to.transform.position.x = to.transform.position.x;
           // to.transform.position.y = to.transform.position.y;
           // to.transform.position.z = to.transform.position.z;
            //buildpath();
            clean = true;
       // }


    }
    void LateUpdate()
    {

        buildpath();
    }
    void buildpath() {
    //dataIn datain = new dataIn();
    //pathTools box =new pathTools();
    matrixNode endNode =AStar(true,new Vector3(0f,0f,0f),new Vector3(0f,0f,0f));
    Stack<matrixNode> path = new Stack<matrixNode>();
        if (endNode != null)
        {
            while (endNode.x != transform.position.x || endNode.y != transform.position.y || endNode.z != transform.position.z)
            {
                path.Push(endNode);
                endNode = endNode.parent;
            }
            path.Push(endNode);
        }
        

        print("The shortest path from  " +
                          "(" + transform.position.x + "," + transform.position.y + "," + transform.position.z + ")  to " +
                          "(" + to.transform.position.x + "," + to.transform.position.y + "," + to.transform.position.z + ")  is:  \n");

        while (path.Count > 0)
        {
            matrixNode node = path.Pop();

            print("(" + node.x + "," + node.y + "," + node.z + ")");
            GameObject block = Instantiate(block1, Vector3.zero, block1.transform.rotation) as GameObject;
            block.transform.parent = transform;
            block.transform.localPosition = new Vector3(node.x, node.y, node.z);
            block.transform.name = "trail";
            //block.transform.tag = "trail";
            astarpath.Add(block);
        }
    }
    public void unitTest_AStar()
{


        //looking for shortest path from 'S' at (0,1) to 'E' at (3,3)
        //obstacles marked by 'X'
        //dataIn datain = new dataIn();
        //pathTools part = new pathTools();
        //int transform.position.x = 1, transform.position.y = 1,transform.position.z=1, to.transform.position.x = 1, to.transform.position.y = 1, to.transform.position.z=3;
    matrixNode endNode = AStar(true, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f));

    //looping through the Parent nodes until we get to the start node
    Stack<matrixNode> path = new Stack<matrixNode>();
    
    while (endNode.x != transform.position.x || endNode.y != transform.position.y || endNode.z != transform.position.z)
    {
        path.Push(endNode);
        endNode = endNode.parent;
    }
    path.Push(endNode);

    print("The shortest path from  " +
                      "(" + transform.position.x + "," + transform.position.y +","+ transform.position.z+")  to " +
                      "(" + to.transform.position.x + "," + to.transform.position.y + "," + to.transform.position.z + ")  is:  \n");

    while (path.Count > 0)
    {
        matrixNode node = path.Pop();
            print("(" + node.x + "," + node.y + ","+node.z+")");
    }
}

public class matrixNode
{
    public int fr = 0, to = 0, sum = 0;
    public int x, y, z;
    public matrixNode parent;
}
public class repNode
{
    public char value = '-';
    public int length = 100, height = 100, width = 100;
    }

        public class dataIn
        {
            public class repNode
            {
                public char value = '-';
                public int length = 100, height = 100, width = 100;
            }
           // public delegate repNode matrixDelegate(int x, int y, int z);
            public repNode matrix(int x, int y, int z)
            {
                char[][][] matrix = new char[][][] {
new char[][]  {
    new char[] {'X', 'X', 'X', 'X', 'X'},
    new char[] {'X', 'X', 'X', 'X', 'X'},
    new char[] {'X', 'X', 'X', 'X', 'X'},
    new char[] {'X', 'X', 'X', 'X', 'X'},
    new char[] {'X', 'X', 'X', 'X', 'X'} },
new char[][]  {
    new char[] {'X', 'X', 'X', 'X', 'X'},
    new char[] {'X', '-', 'X', '-', 'X'},
    new char[] {'X', '-', 'X', '-', 'X'},
    new char[] {'X', '-', 'X', '-', 'X'},
    new char[] {'X', 'X', 'X', 'X', 'X'} },
new char[][]  {
    new char[] {'X', 'X', 'X', 'X', 'X'},
    new char[] {'X', '-', 'X', '-', 'X'},
    new char[] {'X', '-', 'X', '-', 'X'},
    new char[] {'X', '-', 'X', '-', 'X'},
    new char[] {'X', 'X', 'X', 'X', 'X'} },
new char[][]  {
    new char[] {'X', 'X', 'X', 'X', 'X'},
    new char[] {'X', '-', 'X', '-', 'X'},
    new char[] {'X', '-', 'X', '-', 'X'},
    new char[] {'X', '-', '-', '-', 'X'},
    new char[] {'X', 'X', 'X', 'X', 'X'} },
new char[][]  {
    new char[] {'X', 'X', 'X', 'X', 'X'},
    new char[] {'X', 'X', 'X', '-', 'X'},
    new char[] {'X', 'X', 'X', '-', 'X'},
    new char[] {'X', 'X', 'X', '-', 'X'},
    new char[] {'X', 'X', 'X', 'X', 'X'} },
new char[][]  {
    new char[] {'X', 'X', 'X', 'X', 'X'},
    new char[] {'X', '-', '-', '-', 'X'},
    new char[] {'X', '-', 'X', '-', 'X'},
    new char[] {'X', '-', 'X', '-', 'X'},
    new char[] {'X', 'X', 'X', 'X', 'X'} },
new char[][]  {
    new char[] {'X', 'X', 'X', 'X', 'X'},
    new char[] {'X', '-', 'X', '-', 'X'},
    new char[] {'X', '-', 'X', '-', 'X'},
    new char[] {'X', '-', 'X', '-', 'X'},
    new char[] {'X', 'X', 'X', 'X', 'X'} },
new char[][]  {
    new char[] {'X', 'X', 'X', 'X', 'X'},
    new char[] {'X', '-', 'X', '-', 'X'},
    new char[] {'X', '-', 'X', '-', 'X'},
    new char[] {'X', '-', '-', '-', 'X'},
    new char[] {'X', 'X', 'X', 'X', 'X'} },
new char[][] {
    new char[] {'X', 'X', 'X', 'X', 'X'},
    new char[] {'X', 'X', 'X', '-', 'X'},
    new char[] {'X', 'X', 'X', '-', 'X'},
    new char[] {'X', 'X', 'X', 'X', 'X'},
    new char[] {'X', 'X', 'X', 'X', 'X'} } };
                // GameObject block = Instantiate(block1, Vector3.zero, block1.transform.rotation) as GameObject;
                // block.transform.parent = transform;
                // block.transform.localPosition = new Vector3(x,y, z);

                return new repNode { value = matrix[0][0][0] };
            }
        }

    public matrixNode AStar(bool is3D, Vector3 pos1, Vector3 pos2)
{
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // in this version an element in a matrix can move left/up/right/down in one step, two steps for a diagonal move.
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            char testingblock = '-';
            //Scanner script=scanner.GetComponent(typeof(Scanner)) as Scanner;
            //StartCoroutine(script.look(0, 0, 0, (x) => testingblock = x));
            //StartCoroutine(script.look(0, 0, 0, (x) => testingblock = x));
            //repNode a = new repNode {value= testingblock };
            //GameObject block = Instantiate(block1, Vector3.zero, block1.transform.rotation) as GameObject;
            //the keys for greens and reds are x.ToString() + y.ToString() of the matrixNode 
            Dictionary<string, matrixNode> greens = new Dictionary<string, matrixNode>(); //open 
    Dictionary<string, matrixNode> reds = new Dictionary<string, matrixNode>(); //closed 
    
    matrixNode startNode = new matrixNode { x = (int)transform.position.x, y = (int)transform.position.y , z = (int)transform.position.z};
    string key = startNode.x.ToString() + startNode.y.ToString() +startNode.z.ToString();
        greens.Add(key, startNode);

        Func<KeyValuePair<string, matrixNode>> smallestGreen = () =>
        {
           
      
            KeyValuePair<string, matrixNode> smallest = new KeyValuePair<string, matrixNode>( greens.Keys.ToArray()[0],greens[greens.Keys.ToArray()[0]] );//because dot net does some fun things we dont



            foreach (KeyValuePair<string, matrixNode> item in greens)
            {
                reds.Remove(item.Key);
                if (item.Value.sum < smallest.Value.sum) { 
                    smallest = item; }
                else if (item.Value.sum == smallest.Value.sum && item.Value.to < smallest.Value.to)
                {
                    smallest = item;
                }
            }
            print("testnode");
            print(smallest.Value.x);
            print(smallest.Value.y);
            print(smallest.Value.z);
            return smallest;
        };
        //2D block
        //add these values to current node's x and y values to get the left/up/right/bottom neighbors
        List<KeyValuePair<int, int>> fourNeighbors = new List<KeyValuePair<int, int>>()
                                            { new KeyValuePair<int, int>(-1,0),
                                              new KeyValuePair<int, int>(0,1),
                                              new KeyValuePair<int, int>(1, 0),
                                              new KeyValuePair<int, int>(0,-1) };
        //3d block
        //List<List<int>> sixNeighbors =
        int[][] sixNeighbors = new int[][] {//use one collider and expand it for the ediges...
            new  int[] { 0,0,1},
            new  int[] { 0,0,-1},
            new  int[] { 0,1,0},
            new  int[] { 0,-1,0},
            new  int[] { 1,0,0},
            new  int[] { -1,0,0}


        };
    //repNode a = new amatrix(0, 0, 0);

    while (true)
    {
            if (greens.Count == 0)
            {
                print("nowhere togo");
                return null;
            }
            //print("{{");
            //print(greens.Count);
        KeyValuePair<string, matrixNode> current = smallestGreen();
            //print("header");
            //print(current.Value.x);
            //print(current.Value.z);
            //print(current.Value.x);
            if (current.Value.x == to.transform.position.x && current.Value.y == to.transform.position.y && current.Value.z == to.transform.position.z)
            {
                return current.Value;
            }

        greens.Remove(current.Key);
        reds.Add(current.Key, current.Value);
       
                foreach (int[] mainVector in sixNeighbors)
                {
                    int nbrX = current.Value.x + mainVector[0];
                    //if (nbrX < 0)
                    //    nbrX = 0;
                    //if (nbrX > maxX)
                    //    nbrX= maxX;
                    int nbrY = current.Value.y + mainVector[1];
                    //if (nbrY < 0)
                    //    nbrY = 0;
                    //if (nbrY > maxY)
                    //   nbrY = maxY;
                    int nbrZ = current.Value.z + mainVector[2];
                    //if (nbrZ < 0)
                    //    nbrZ = 0;
                    //if (nbrZ > maxZ)
                    //    nbrZ = maxZ;
                    if (is3D == true)
                    {

                    }
                    string nbrKey = nbrX.ToString() + nbrY.ToString() + nbrZ.ToString();
                    //print(nbrKey);
                    testingblock = '-';
                    Scanner script = scanner.GetComponent(typeof(Scanner)) as Scanner;//this is how its set
                    StartCoroutine(script.look(nbrX, nbrY, nbrZ, (x) => testingblock = x));
                    StartCoroutine(script.look(nbrX, nbrY, nbrZ, (x) => testingblock = x));
                    //StartCoroutine(look(0, 0, 0, (x) => testingblock = x));
                    repNode b = new repNode { value = testingblock };

                    //  repNode b = new repNode { value = testingblock };

                    //if (nbrX < 0 || nbrY < 0 || nbrZ < 0 || nbrX >= maxX || nbrY >= maxY || nbrZ >= maxZ
                    if (b.value == 'X' || reds.ContainsKey(nbrKey))
                    //greens.Remove(nbrKey);
                        continue;

                    if (greens.ContainsKey(nbrKey))
                    {
                        matrixNode curNbr = greens[nbrKey];
                        int from = (int)Math.Abs(nbrX - transform.position.x) + (int)Math.Abs(nbrY - transform.position.y) + (int)Math.Abs(nbrZ - transform.position.z);
                        if (from < curNbr.fr)
                        {
                            curNbr.fr = from;
                            curNbr.sum = curNbr.fr + curNbr.to;
                            curNbr.parent = current.Value;
                        }
                    }
                    else
                    {
                        matrixNode curNbr = new matrixNode { x = nbrX, y = nbrY, z = nbrZ };
                        curNbr.fr = (int)Math.Abs(nbrX - transform.position.x) + (int)Math.Abs(nbrY - transform.position.y) + (int)Math.Abs(nbrZ - transform.position.z);
                        curNbr.to = (int)Math.Abs(nbrX - to.transform.position.x) + (int)Math.Abs(nbrY - to.transform.position.y) + (int)Math.Abs(nbrZ - to.transform.position.z);
                        curNbr.sum = curNbr.fr + curNbr.to;
                        curNbr.parent = current.Value;
                        greens.Add(nbrKey, curNbr);
                    }
                }
            }
    }
}

