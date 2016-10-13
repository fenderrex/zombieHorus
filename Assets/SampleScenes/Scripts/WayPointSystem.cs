using UnityEngine;
using System.Collections;

public class WayPointSystem : MonoBehaviour {

	// Use this for initialization
	public AnimationCurve X;//this should be a array, no?
	public AnimationCurve Y;//this should be a array, no?
	public AnimationCurve Z;//this should be a array, no?
	public AnimationCurve scale;//this should be a array, no?
	public AnimationCurve smother;//this should be a array, no?

	private Keyframe[] x;//this should be a difrent array, no?
	private Keyframe[] y;//this should be a array, no?
	private Keyframe[] z;//this should be a array, no?
	private Keyframe[] sc;//this should be a array, no?
	private Keyframe[] sm;//this should be a array, no?
	public Cart ride;
    private int pay=0;
	public static Node f;
    //scale=n
    private float ttcashed = Time.time;
    public float CycalTime=60f;
    private float delta = 0f;
    public bool progress = true;
	public GameObject[] points;//=new GameObject[];
	private GameObject[] nodes;//=new GameObject[];
	private bool reloadNodes=true;
    private int old=0;
	void Awake() {
		MakePath();
	}
    void Pause()
    {
        ttcashed= Time.time;
        progress = false;
    }
    void Play()
    {
        delta = Time.time-ttcashed;
        progress = true;

    }
	Node w;
	void MakePath(){

		x = new Keyframe[points.Length];//this should be a array, no?
		y = new Keyframe[points.Length];
		z = new Keyframe[points.Length];
		sc = new Keyframe[points.Length];
		sm = new Keyframe[points.Length];
		
		int i = 0;
		float si = 0;
		//sc[i] = new Keyframe(i,1);
		while (i < points.Length) {
			if (reloadNodes){
				points[i].AddComponent<Node>();
				w =points[i].GetComponent<Node>();
				if (w != null){
					reloadNodes=false;
				}

			}
			//(Node)points[i].GetComponent(typeof(Node));
			w = points[i].GetComponent<Node>();
			if (!reloadNodes){
                w.SetTimeWarp((float)1/((float)CycalTime / (float)points.Length));
				x[i] = new Keyframe(i,points[i].transform.position.x);
				y[i] = new Keyframe(i,points[i].transform.position.y);
				z[i] = new Keyframe(i,points[i].transform.position.z);
				sc[i] = new Keyframe(i,points[i].GetComponent<Node>().GetTimeWarp());
				sm[i] = new Keyframe(i,points[i].GetComponent<Node>().GetSmothing());//in a keyframe so I can do some smothing stuff latter maybe...
				i++;
			}
		}
		i = points.Length;
		//sc[i] = new Keyframe (i, 1);
		X = new AnimationCurve(x);
		X.postWrapMode = WrapMode.Loop;
		Y = new AnimationCurve(y);
		Y.postWrapMode = WrapMode.Loop;
		Z = new AnimationCurve(z);
		Z.postWrapMode = WrapMode.Loop;
		int e = 0;
		while (e < points.Length) {
			X.SmoothTangents (e,sm[e].value);//just use the keys now play with Evaluate here when you get borad
			Y.SmoothTangents (e,sm[e].value);
			Z.SmoothTangents (e,sm[e].value);
			e+=1;
		}
		scale=new AnimationCurve(sc);

	}
	void Update() {
        //todo check if node changed and rebuild if so
        if (progress)
        {
            MakePath();
        //Cart objH = ride.GetComponent(typeof(Cart))as Cart;
    
            float s = scale.Evaluate(Time.time - delta);
            int test = (int)(((Time.time - delta) / X.length));
            //print(test);
            if (old != test){
                //todo fire node hooks
                if (test- (pay*X.length) >= X.length)
                {
                    pay += 1;
                }
                old = test;
                int index= test-(pay * X.length);
                points[index].GetComponent<Node>().Reached();


            }
            ride.UpdateTraling((Time.time- delta) *s,X,Y,Z);
        }
        // ride.transform.position = new Vector3( X.Evaluate(Time.time*s),Y.Evaluate(Time.time*s), Z.Evaluate(Time.time*s));
    }
}
