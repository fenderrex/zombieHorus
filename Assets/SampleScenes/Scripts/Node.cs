using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {
	private bool error=false;
   
	private Vector3[]transformPos;
	public float smother=0.2f;
	public float warp=0.4f;
	private float smotherTemp;
	private float warpTemp;
	public Node(){

	}
    
	// Use this for initialization
	void Start ()
	{
		Vector3 tete;
		tete= transform.position;
		transformPos=new Vector3[]{tete,tete};
		//smother = 2f;
		smotherTemp = smother;
		//warp=.4f;
		warpTemp=warp;

	}
    public void Reached()
    {
        print("reached");


    }
    // Update is called once per frame
    void Update ()
    {
        
        //MakePath();
        if (!error){//the error has not been reset(the path still needs to be updated) dont waste time runing this...
			if (warp!=warpTemp){
				warpTemp=warp;
				error=true;
			}
			if (smother!=smotherTemp){
				smotherTemp=smother;
				error=true;
			}
			if (transformPos[0]==transformPos[1]){
				transformPos[1]=transformPos[0];
				error=true;
			}
			//float s = scale.Evaluate (Time.time);
			//ride.transform.position = new Vector3( X.Evaluate(Time.time*s),Y.Evaluate(Time.time*s), Z.Evaluate(Time.time*s));
		}
		
	}
	public void SetSmothing(float e){
		smother = e;
	}
	public float GetSmothing(){
		return smother;
	}
	public void SetTimeWarp(float e){
		warp = e;
	}
	public float GetTimeWarp(){
		return warp;
	}
	public Vector3 TransformPos
	{
		get
		{
			return this.transformPos[0];
		}
		set
		{
			this.transformPos[0] = value;
		}
	}
	public bool Error
	{
		get
		{
			return this.error;
		}
		set
		{
			this.error = value;
		}
	}
}