using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class NewBehaviourScript : MonoBehaviour {
    public List<Rigidbody> boxs =new List<Rigidbody>();
	// Use this for initialization
	void Start () {
        string[] array = new string[] { "A", "H" };
        Main(array);

    }

	
	public static void Main(string[] args)
    {
        Graph g = new Graph();
        g.add_vertex('A', new Dictionary<char, int>() { { 'B', 7 }, { 'C', 8 } });
        g.add_vertex('B', new Dictionary<char, int>() { { 'A', 7 }, { 'F', 2 } });
        g.add_vertex('C', new Dictionary<char, int>() { { 'A', 8 }, { 'F', 6 }, { 'G', 4 } });
        g.add_vertex('D', new Dictionary<char, int>() { { 'F', 8 } });
        g.add_vertex('E', new Dictionary<char, int>() { { 'H', 1 } });
        g.add_vertex('F', new Dictionary<char, int>() { { 'B', 2 }, { 'C', 6 }, { 'D', 8 }, { 'G', 9 }, { 'H', 3 } });
        g.add_vertex('G', new Dictionary<char, int>() { { 'C', 4 }, { 'F', 9 } });
        g.add_vertex('H', new Dictionary<char, int>() { { 'E', 1 }, { 'F', 3 } });

        g.shortest_path('A', 'H').ForEach(x => print(x));
    }
    public GameObject brick;
    // Update is called once per frame
    void Update () {
        //Rigidbody clone;
        if (Input.GetKey("up"))
            boxs.Add((Rigidbody)Instantiate(brick, transform.position+ new Vector3(UnityEngine.Random.Range(-10.0f, 10.0f), UnityEngine.Random.Range(-10.0f, 10.0f), UnityEngine.Random.Range(-10.0f, 10.0f)), transform.rotation));
        //new Vector3(UnityEngine.Random.Range(-10.0f, 10.0f), UnityEngine.Random.Range(-10.0f, 10.0f), UnityEngine.Random.Range(-10.0f, 10.0f));
        if (Input.GetKey("down"))
        {

            foreach (Rigidbody box1 in boxs)
            {
                

            
                foreach (Rigidbody box2 in boxs)
                {
                 //   box1.GetComponent<Cart>();
                 //   box2.GetComponent<Cart>();
                    Math.Round(diff(box1.transform.position, box2.transform.position));

                }
            }



        }


        // Instantiate(brick, transform.position, transform.rotation);


    }



    float diff(Vector3 pos1, Vector3 pos2)
    {
        float deltaX = pos1.x - pos2.x;
        float deltaY = pos1.y - pos2.y;
        float deltaZ = pos1.z - pos2.z;

        return  (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);

    }


    class Graph
    {
        Dictionary<char, Dictionary<char, int>> vertices = new Dictionary<char, Dictionary<char, int>>();

        public void add_vertex(char name, Dictionary<char, int> edges)
        {
            vertices[name] = edges;
        }

        public List<char> shortest_path(char start, char finish)
        {
            var previous = new Dictionary<char, char>();
            var distances = new Dictionary<char, int>();
            var nodes = new List<char>();

            List<char> path = null;

            foreach (var vertex in vertices)
            {
                if (vertex.Key == start)
                {
                    distances[vertex.Key] = 0;
                }
                else
                {
                    distances[vertex.Key] = int.MaxValue;
                }

                nodes.Add(vertex.Key);
            }

            while (nodes.Count != 0)
            {
                nodes.Sort((x, y) => distances[x] - distances[y]);

                var smallest = nodes[0];
                nodes.Remove(smallest);

                if (smallest == finish)
                {
                    path = new List<char>();
                    while (previous.ContainsKey(smallest))
                    {
                        path.Add(smallest);
                        smallest = previous[smallest];
                    }

                    break;
                }

                if (distances[smallest] == int.MaxValue)
                {
                    break;
                }

                foreach (var neighbor in vertices[smallest])
                {
                    var alt = distances[smallest] + neighbor.Value;
                    if (alt < distances[neighbor.Key])
                    {
                        distances[neighbor.Key] = alt;
                        previous[neighbor.Key] = smallest;
                    }
                }
            }

            return path;
        }
    }










}



