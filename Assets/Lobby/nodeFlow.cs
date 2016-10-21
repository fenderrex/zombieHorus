using UnityEngine;
using System.Collections;

public class nodeFlow : MonoBehaviour {
	protected AnimationCurve X;
	protected AnimationCurve Y;
	protected AnimationCurve Z;
	public AnimationCurve TimeShift;
	public GameObject[] Set;
	public GameObject prop;
	// Use this for initialization
	void Start() {
		//for(GameObject i in Set){
        for (int i = 0; i < Set.Length; i++) { 
            X.AddKey (i, Set[i].transform.position.x);
			Y.AddKey (i, Set[i].transform.position.y);
			Z.AddKey (i, Set[i].transform.position.z);
		}
		print ("nodes accounted for!");
	}

	// Update is called once per frame
	void Update() {

	}
	void FixedUpdate() {
		float t=Time.time*TimeShift.Evaluate(Time.time);
		prop.transform.position=(Vector3)GetPoint(t);
	}
	Vector3 GetPoint(float p){
		return new Vector3(X.Evaluate(p),Y.Evaluate(p),Z.Evaluate(p));
	}

}
