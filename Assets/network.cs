using UnityEngine;
using System.Collections;

public class network : MonoBehaviour {
    int time = 0;
    public string baceURL = "127.0.0.1:7905/";
    // Use this for initialization
    void Start () {
	

    }
	
	// Update is called once per frame
	void Update () {
        time += 1;
        if (time >= 3)
        {
            PostURL e = new PostURL();
            e.Start(baceURL);
            time = 0;
        }
        else { print("no requesting"); }
	}
}

public class PostURL : MonoBehaviour
{
    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.data);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
    public void Start(string url)

    {
        WWWForm form = new WWWForm();
        form.AddField("userName", "value1");
        form.AddField("Password", "value2");
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www));
    }

   
}
public class GetURL : MonoBehaviour
{

    void Start(string url)
    {
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www));
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.data);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}