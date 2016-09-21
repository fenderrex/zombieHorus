using UnityEngine;
using System.Collections;

public class Plugin : MonoBehaviour {
    //player prediction plugin
    int index = 0;
    string[] data = new string[20];//todo use FIFO lib....
	// Use this for initialization
	public void Start () {
        //data[0] = "hello";
    }

    // Update is called once per frame
    public bool Update()
    {
        //data[1] = " world";
        return true;
    }
    public string[] Result()
    {
        string[] temp = new string[20];
        temp = data;
        data = new string[20];//reset buffer
        return temp;
    }
}
