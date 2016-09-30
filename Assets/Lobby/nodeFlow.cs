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
		foreach(int i in Set){
			
			X.AddKey (i, Set[i].transform.posstion.x);
			Y.AddKey (i, Set[i].transform.posstion.y);
			Z.AddKey (i, Set[i].transform.posstion.z);
		}
		print ("nodes accounted for!");
	}

	// Update is called once per frame
	void Update() {

	}
	void FixedUpdate() {
		float t=Time.time*TimeShift.Evaluate(Time.time);
		prop.transform.posstion=GetPoint(t);
	}
	Vector3 virtural GetPoint(float p){
		new Vector(X.Evaluate(p),Y.Evaluate(p),Z.Evaluate(p));
	}

}
