using UnityEngine;
using System;
using System.Collections;

using System.Collections.Generic;
using System.Linq;
public class astar : MonoBehaviour {
    int[] e = new int[] { };
	// Use this for initialization
	void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
	
	}

public void unitTest_AStar()
{
    char[][] matrix = new char[][] { new char[] {'-', 'S', '-', '-', 'X'},
                                         new char[] {'-', 'X', 'X', '-', '-'},
                                         new char[] {'-', '-', '-', 'X', '-'},
                                         new char[] {'X', '-', 'X', 'E', '-'},
                                         new char[] {'-', '-', '-', '-', 'X'}};

    //looking for shortest path from 'S' at (0,1) to 'E' at (3,3)
    //obstacles marked by 'X'
    int fromX = 0, fromY = 1,fromZ=0, toX = 3, toY = 3, toZ=3;
    matrixNode endNode = AStar(matrix, fromX, fromY, fromZ, toX, toY, toZ);

        //looping through the Parent nodes until we get to the start node
        Stack<matrixNode> path = new Stack<matrixNode>();

    while (endNode.x != fromX || endNode.y != fromY)
    {
        path.Push(endNode);
        endNode = endNode.parent;
    }
    path.Push(endNode);

    Console.WriteLine("The shortest path from  " +
                      "(" + fromX + "," + fromY + ")  to " +
                      "(" + toX + "," + toY + ")  is:  \n");

    while (path.Count > 0)
    {
        matrixNode node = path.Pop();
        Console.WriteLine("(" + node.x + "," + node.y + ")");
    }
}

public class matrixNode
{
    public int fr = 0, to = 0, sum = 0;
    public int x, y, z;
    public matrixNode parent;
}

public static matrixNode AStar(char[][] matrix, int fromX, int fromY, int fromZ, int toX, int toY, int toZ)
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // in this version an element in a matrix can move left/up/right/down in one step, two steps for a diagonal move.
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //the keys for greens and reds are x.ToString() + y.ToString() of the matrixNode 
    Dictionary<string, matrixNode> greens = new Dictionary<string, matrixNode>(); //open 
    Dictionary<string, matrixNode> reds = new Dictionary<string, matrixNode>(); //closed 
    public bool is3D = true;
    matrixNode startNode = new matrixNode { x = fromX, y = fromY , z = fromZ};
    string key = startNode.x.ToString() + startNode.x.ToString();
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
        List<List<int>> sixNeighbors =
        new List<List<int>>{
            new  List <int> { 0,0,1},
            new  List <int> { 0,0,-1},
            new  List <int> { 0,1,0},
            new  List <int> { 0,-1,0},
            new  List <int> { 1,0,0},
            new  List <int> { -1,0,0}


        };
    int maxX = matrix.GetLength(0);
    if (maxX == 0)
        return null;
    int maxY = matrix[0].Length;

    while (true)
    {
        if (greens.Count == 0)
            return null;

        KeyValuePair<string, matrixNode> current = smallestGreen();
        if (current.Value.x == toX && current.Value.y == toY && current.Value.z == toZ)
            return current.Value;

        greens.Remove(current.Key);
        reds.Add(current.Key, current.Value);

        foreach (KeyValuePair<int, int> plusXY in fourNeighbors)
        {
            int nbrX = current.Value.x + plusXY.Key;
            int nbrY = current.Value.y + plusXY.Value;
            int nbrZ = 0;
                if (is3D == true)
                {

                }
            string nbrKey = nbrX.ToString() + nbrY.ToString();
            if (nbrX < 0 || nbrY < 0 || nbrX >= maxX || nbrY >= maxY
                || matrix[nbrX][nbrY] == 'X' //obstacles marked by 'X'
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