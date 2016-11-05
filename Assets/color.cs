using UnityEngine;
using System.Collections;

public class color : MonoBehaviour
{



    public Color ObjectColor;

    private Color currentColor;
    private Material materialColored;
    void Update()
    {
        //currentColor = ObjectColor;
    
        if (ObjectColor != currentColor)
        {
            //print("color update");
            //helps stop memory leaks
            if (materialColored != null)
                UnityEditor.AssetDatabase.DeleteAsset(UnityEditor.AssetDatabase.GetAssetPath(materialColored));

            //create a new material
            materialColored = new Material(Shader.Find("Diffuse"));
            materialColored.color = currentColor = ObjectColor;
            this.GetComponent<Renderer>().material = materialColored;
        }
    }
}