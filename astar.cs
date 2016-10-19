using UnityEngine;
using System.Collections;

public class astar : MonoBehaviour {
    int[] e = new int[] { };
	// Use this for initialization
	void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

void AStar(start, goal) {
// The set of nodes already evaluated.
GameObject[] closedSet= new GameObject[] { };
    // The set of currently discovered nodes still to be evaluated.
    // Initially, only the start node is known.
GameObject[] openSet = new GameObject[] { start};
// For each node, which node it can most efficiently be reached from.
// If a node can be reached from many nodes, cameFrom will eventually contain the
// most efficient previous step.
//cameFrom:= the empty map
GameObject[] cameFrom = new GameObject[] { };
    // For each node, the cost of getting from the start node to that node.
    list< GameObject>=new list<GameObject>
    for (GameObject item in openSet)
    {
        item.GetComponent(typeof(astarnode)).fScore();
    }

    for (GameObject item in openSet) { 
        current =//he node in openSet having the lowest fScore[] value
        if current = goal
            return reconstruct_path(cameFrom, current)

        openSet.Remove(current)
        closedSet.Add(current)
        for each neighbor of current
            if neighbor in closedSet
                continue		// Ignore the neighbor which is already evaluated.
            // The distance from start to a neighbor
            tentative_gScore:= gScore[current] + dist_between(current, neighbor)
            if neighbor not in openSet	// Discover a new node
                openSet.Add(neighbor)
            else if tentative_gScore >= gScore[neighbor]
                continue		// This is not a better path.
   
    // This path is the best until now. Record it!
    cameFrom[neighbor] := current
            gScore[neighbor] := tentative_gScore
            fScore[neighbor] := gScore[neighbor] + heuristic_cost_estimate(neighbor, goal)

    return failure;
    }
}
function reconstruct_path(cameFrom, current)
    total_path := [current]
    while current in cameFrom.Keys:
        current := cameFrom[current]
        total_path.append(current)
    return total_path