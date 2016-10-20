using UnityEngine;
using System.Collections;

public class grid : MonoBehaviour {




    public GameObject block1;

    public int worldWidth = 10;
    public int worldHeight = 10;
    public int worldLenght = 10;
    
    public float spawnSpeed = 0;

    void Start()
    {
        Quaternion a= transform.rotation;
        StartCoroutine(CreateWorld());
        transform.rotation=a;
    }

    IEnumerator CreateWorld()
    {
        for (int y = 0; y < worldHeight; y++)
        {
            yield return new WaitForSeconds(spawnSpeed);
            for (int x = 0; x < worldWidth; x++)
            {
                if (x % 30 == 0)//TODO: or if low fpf and not game load
                {
                    yield return new WaitForSeconds(spawnSpeed);
                }
                for (int z = 0; z < worldLenght; z++)
                {
                    

                    GameObject block = Instantiate(block1, transform.position, transform.rotation) as GameObject;
                    block.transform.parent = transform;
                    block.transform.localPosition = new Vector3(x*transform.localScale.x, y* transform.localScale.y, z* transform.localScale.z);
                    //yield return new WaitForSeconds(spawnSpeed);
                }
            }
        }
       
    }
}