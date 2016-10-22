
 using UnityEngine;
 using System.Collections;
 
 public class matrix3D : MonoBehaviour
{

    public GameObject block1;

    public int worldWidth = 10;
    public int worldLenght = 10;
    public int worldHeight = 10;

    public float spawnSpeed = 0;

    void Start()
    {
        StartCoroutine(CreateWorld());
    }

    IEnumerator CreateWorld()
    {
        for (int x = 0; x < worldWidth; x++)
        {
            yield return new WaitForSeconds(spawnSpeed);

            for (int y = 0; y < worldHeight; y++)
            {
                yield return new WaitForSeconds(spawnSpeed);
                for (int z = 0; z < worldHeight; z++)
                {
                    //yield return new WaitForSeconds(spawnSpeed);

                    GameObject block = Instantiate(block1, Vector3.zero, block1.transform.rotation) as GameObject;
                    block.transform.parent = transform;
                    block.transform.localPosition = new Vector3(x, y, z);
                }
            }
        }
    }
}