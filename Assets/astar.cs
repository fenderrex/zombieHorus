using UnityEngine;
using System;
using System.Collections;

using System.Collections.Generic;
using System.Linq;
public class astar : MonoBehaviour {
    int[] e = new int[] { };
    public bool is3D = true;
    public GameObject block1;
    public int fromX = 1, fromY = 3, fromZ = 1, toX = 3, toY = 3, toZ = 3;
    public int fromX1 = 100, fromY1 = 100, fromZ1 = 100, toX1 = 100, toY1 = 100, toZ1 = 100;
    public List<GameObject> astarpath;// = new List<GameObject>;
    public bool clean = true;
    // Use this for initialization
    void Start () {


        //unitTest_AStar();
    }

    // Update is called once per frame
    void Update() {
        
        //dataIn a=datain.matrix;
        //if (fromX != fromX1 || fromY != fromY1 || fromZ != fromZ1 || toX != toX1 || toY != toY1 || toZ != toZ1) {
            
            for (int i=0;i< astarpath.Count;i++)
            {
                Destroy(astarpath[i]);
                Destroy(astarpath[i]);
                astarpath[i] = null;
                astarpath.Remove(astarpath[i]);
            }

            fromZ1 = fromZ;
            fromY1 = fromY;
            fromX1 = fromX;
            toX1 = toX;
            toY1 = toY;
            toZ1 = toZ;
            //buildpath();
            clean = true;
       // }


    }
    void LateUpdate()
    {

        buildpath();
    }
    void buildpath() {
    dataIn datain = new dataIn();
    pathTools box =new pathTools();
    matrixNode endNode =box.AStar(true, datain.matrix, fromX1, fromY1, fromZ1, toX1, toY1, toZ1);
    Stack<matrixNode> path = new Stack<matrixNode>();
        if (endNode != null)
        {
            while (endNode.x != fromX1 || endNode.y != fromY1 || endNode.z != fromZ1)
            {
                path.Push(endNode);
                endNode = endNode.parent;
            }
            path.Push(endNode);
        }
        

        print("The shortest path from  " +
                          "(" + fromX1 + "," + fromY1 + "," + fromZ1 + ")  to " +
                          "(" + toX + "," + toY + "," + toZ + ")  is:  \n");

        while (path.Count > 0)
        {
            matrixNode node = path.Pop();

            print("(" + node.x + "," + node.y + "," + node.z + ")");
            GameObject block = Instantiate(block1, Vector3.zero, block1.transform.rotation) as GameObject;
            block.transform.parent = transform;
            block.transform.localPosition = new Vector3(node.x, node.y, node.z);
            astarpath.Add(block);
        }
    }
    public void unitTest_AStar()
{


        //looking for shortest path from 'S' at (0,1) to 'E' at (3,3)
        //obstacles marked by 'X'
        dataIn datain = new dataIn();
        pathTools part = new pathTools();
        int fromX = 1, fromY = 1,fromZ=1, toX = 1, toY = 1, toZ=3;
    matrixNode endNode = part.AStar(true, datain.matrix, fromX1, fromY1, fromZ1, toX1, toY1, toZ1);

    //looping through the Parent nodes until we get to the start node
    Stack<matrixNode> path = new Stack<matrixNode>();
    
    while (endNode.x != fromX1 || endNode.y != fromY1 || endNode.z != fromZ1)
    {
        path.Push(endNode);
        endNode = endNode.parent;
    }
    path.Push(endNode);

    print("The shortest path from  " +
                      "(" + fromX1 + "," + fromY1 +","+ fromZ1+")  to " +
                      "(" + toX1 + "," + toY1 + "," + toZ1 + ")  is:  \n");

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
    public int length = 0, height = 0, width=0;
}
public class dataIn
    {
        public class repNode
        {
            public char value = '-';
            public int length = 0, height = 0, width = 0;
        }
        public delegate repNode matrixDelegate(int x, int y, int z);
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
            
    return new repNode { length = 4, height = 5, width = 4, value = matrix[x][y][z] };
    }
}
public class pathTools { 
public matrixNode AStar(bool is3D, dataIn.matrixDelegate amatrix, int fromX1, int fromY1, int fromZ1, int toX, int toY, int toZ)
{
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // in this version an element in a matrix can move left/up/right/down in one step, two steps for a diagonal move.
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        dataIn.repNode a = amatrix(0, 0, 0);
        //GameObject block = Instantiate(block1, Vector3.zero, block1.transform.rotation) as GameObject;
                //the keys for greens and reds are x.ToString() + y.ToString() of the matrixNode 
        Dictionary<string, matrixNode> greens = new Dictionary<string, matrixNode>(); //open 
    Dictionary<string, matrixNode> reds = new Dictionary<string, matrixNode>(); //closed 
    
    matrixNode startNode = new matrixNode { x = fromX1, y = fromY1 , z = fromZ1};
    string key = startNode.x.ToString() + startNode.y.ToString() +startNode.z.ToString();
        greens.Add(key, startNode);

        Func<KeyValuePair<string, matrixNode>> smallestGreen = () =>
        {
            bool cat = false;
      
            KeyValuePair<string, matrixNode> smallest = new KeyValuePair<string, matrixNode>( greens.Keys.ToArray()[0],greens[greens.Keys.ToArray()[0]] );//because dot net does some fun things we dont



            foreach (KeyValuePair<string, matrixNode> item in greens)
            {
               
                    if (item.Value.sum < smallest.Value.sum)
                    smallest = item;
                else if (item.Value.sum == smallest.Value.sum
                        && item.Value.to < smallest.Value.to)
                    smallest = item;
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
    int maxX = a.length;
    if (maxX == 0)
        return null;
    int maxY = a.height;
        if (maxY == 0)
        return null;
    int maxZ = a.width;
    
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
            if (current.Value.x == toX && current.Value.y == toY && current.Value.z == toZ)
            return current.Value;

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
            string nbrKey = nbrX.ToString() + nbrY.ToString()+ nbrZ.ToString();
            //print(nbrKey);
            dataIn.repNode b =amatrix(nbrX, nbrY, nbrZ);
            if (nbrX < 0 || nbrY < 0 || nbrZ < 0 || nbrX >= maxX || nbrY >= maxY || nbrZ >= maxZ
                || b.value == 'X' //obstacles marked by 'X'
                || reds.ContainsKey(nbrKey))
                continue;

            if (greens.ContainsKey(nbrKey))
            {
                matrixNode curNbr = greens[nbrKey];
                int from = Math.Abs(nbrX - fromX1) + Math.Abs(nbrY - fromY1);
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
                curNbr.fr = Math.Abs(nbrX - fromX1) + Math.Abs(nbrY - fromY1) + Math.Abs(nbrZ - fromZ1);
                curNbr.to = Math.Abs(nbrX - toX) + Math.Abs(nbrY - toY) + Math.Abs(nbrZ - toZ);
                curNbr.sum = curNbr.fr + curNbr.to;
                curNbr.parent = current.Value;
                greens.Add(nbrKey, curNbr);
            }
        }
    }
}
}
}
