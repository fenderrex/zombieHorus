using UnityEngine;
using System.Collections;


public class Cart : MonoBehaviour {
    public GameObject focus;
    //this class is assined to the rider of a WayPointSystem given Nodes
    // Use this for initialization
    //protected Rigidbody tail = new Rigidbody();
    public int setback = 3;
    //public SpringJoint link;// = new SpringJoint();
    void Start () {
       // SetSpringJoint(link);
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.LookAt(focus.transform);
    }
    

    public void UpdateTraling(float T, AnimationCurve X, AnimationCurve Y, AnimationCurve Z)
    {
        T=T + setback;
        float A = T;
        float B = T- setback;
        this.transform.position = new Vector3(X.Evaluate(A), Y.Evaluate(A), Z.Evaluate(A));
        Cart tail=this.GetComponent("Cart") as Cart;
        tail.gameObject.transform.position = new Vector3(X.Evaluate(B), Y.Evaluate(B), Z.Evaluate(B));
    }
}
