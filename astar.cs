using UnityEngine;
using System;
using System.Collections;

using System.Collections.Generic;
using System.Linq;
public class astar : MonoBehaviour {
    int[] e = new int[] { };
    public bool is3D = true;
    // Use this for initialization
    void Start () {

        unitTest_AStar();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

public void unitTest_AStar()
{
    char[][][] matrix = new char[][][] {new char[][]  {
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
                                         new char[] {'X', '-', 'X', '-', 'X'},
                                         new char[] {'X', 'X', 'X', 'X', 'X'} },
                                        new char[][] {
                                         new char[] {'-', '-', '-', '-', 'X'},
                                         new char[] {'-', 'X', 'X', '-', '-'},
                                         new char[] {'-', '-', 'X', '-', '-'},
                                         new char[] {'X', '-', 'X', '-', '-'},
                                         new char[] {'-', '-', '-', '-', 'X'}} };

    //looking for shortest path from 'S' at (0,1) to 'E' at (3,3)
    //obstacles marked by 'X'
    int fromX = 1, fromY = 1,fromZ=1, toX = 3, toY = 3, toZ=3;
    matrixNode endNode = AStar(true, matrix, fromX, fromY, fromZ, toX, toY, toZ);

    //looping through the Parent nodes until we get to the start node
    Stack<matrixNode> path = new Stack<matrixNode>();
    
    while (endNode.x != fromX || endNode.y != fromY || endNode.z != fromZ)
    {
        path.Push(endNode);
        endNode = endNode.parent;
    }
    path.Push(endNode);

    print("The shortest path from  " +
                      "(" + fromX + "," + fromY +","+ fromZ+")  to " +
                      "(" + toX + "," + toY + "," + toZ + ")  is:  \n");

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

public static matrixNode AStar(bool is3D,char[][][] matrix, int fromX, int fromY, int fromZ, int toX, int toY, int toZ)
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // in this version an element in a matrix can move left/up/right/down in one step, two steps for a diagonal move.
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //the keys for greens and reds are x.ToString() + y.ToString() of the matrixNode 
    Dictionary<string, matrixNode> greens = new Dictionary<string, matrixNode>(); //open 
    Dictionary<string, matrixNode> reds = new Dictionary<string, matrixNode>(); //closed 
    
    matrixNode startNode = new matrixNode { x = fromX, y = fromY , z = fromZ};
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
            print("smallest");
            
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
        int[][] sixNeighbors = new int[][] {
            new  int[] { 0,0,1},
            new  int[] { 0,0,-1},
            new  int[] { 0,1,0},
            new  int[] { 0,-1,0},
            new  int[] { 1,0,0},
            new  int[] { -1,0,0}


        };
       
        int maxX = matrix.Length;
    if (maxX == 0)
        return null;
    int maxY = matrix[0].Length;
        if (maxY == 0)
        return null;
    int maxZ = matrix[0][0].Length;
    
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
            print("header");
            print(current.Value.x);
            print(current.Value.z);
            print(current.Value.x);
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
            print(nbrKey);
            if (nbrX < 0 || nbrY < 0 || nbrZ < 0 || nbrX >= maxX || nbrY >= maxY || nbrZ >= maxZ
                || matrix[nbrX][nbrY][nbrZ] == 'X' //obstacles marked by 'X'
                || reds.ContainsKey(nbrKey))
                continue;

            if (greens.ContainsKey(nbrKey))
            {
                matrixNode curNbr = greens[nbrKey];
                int from = Math.Abs(nbrX - fromX) + Math.Abs(nbrY - fromY);
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
                curNbr.fr = Math.Abs(nbrX - fromX) + Math.Abs(nbrY - fromY) + Math.Abs(nbrZ - fromZ);
                curNbr.to = Math.Abs(nbrX - toX) + Math.Abs(nbrY - toY) + Math.Abs(nbrZ - toZ);
                curNbr.sum = curNbr.fr + curNbr.to;
                curNbr.parent = current.Value;
                greens.Add(nbrKey, curNbr);
            }
        }
    }
}
}
