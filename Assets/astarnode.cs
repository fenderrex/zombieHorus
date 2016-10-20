using UnityEngine;
using System.Collections;

public class astarnode : MonoBehaviour {
    public float gs = Mathf.Infinity;
    public float fs = Mathf.Infinity;
    // Use this for initialization
    void Start () {
	
	}


    // For each node, the total cost of getting from the start node to the goal
    // by passing by that node. That value is partly known, partly heuristic.
    public float fScore(Vector3 pos2 )
    {
        // For the first node, that value is completely heuristic.
        return Vector3.Distance(pos2, transform.position);
    }
    public float gScore
    {
        get
        {
            return gs;
        }
        set
        {
            gs = value;

        }

    }
    // Update is called once per frame
    void Update () {
	
	}
}
