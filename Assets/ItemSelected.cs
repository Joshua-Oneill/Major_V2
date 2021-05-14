using UnityEngine;
using UnityEngine.UI;

public class ItemSelected : MonoBehaviour
{
    //simple script that changes hat textured block you place depending on a key press
    //it will highlight an image so you can tell which one is being used, sends the texture info to block detction script to tell the new cibe what texture offset to have

    public BlockDetection blockDetection;

    Image im;
    Image allChildrenIm;
    Color constant;
    Color c;
    
    
    void Update()
    {
        SelectItem();
    }

    public void SelectItem()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) //Grass 
        {
            TransparencySet();


            im = gameObject.transform.Find("Dirt").GetComponent<Image>();
            c = im.color;
            c.a = 1;
            im.color = c;


            blockDetection.textureX = 3f;
            blockDetection.textureY = 16f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) //m Stone
        {

            TransparencySet();


            im = gameObject.transform.Find("Stone").GetComponent<Image>();
            c = im.color;
            c.a = 1;
            im.color = c;


            blockDetection.textureX = 5f;
            blockDetection.textureY = 14f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) //snad 
        {
            TransparencySet();


            im = gameObject.transform.Find("Sand").GetComponent<Image>();
            c = im.color;
            c.a = 1;
            im.color = c;

            

            blockDetection.textureX = 1f;
            blockDetection.textureY = 3f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TransparencySet();

            im = gameObject.transform.Find("Bedrock").GetComponent<Image>();
            c = im.color;
            c.a = 1;
            im.color = c;

            blockDetection.textureX = 2f;
            blockDetection.textureY = 15f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            TransparencySet();

            im = gameObject.transform.Find("Wood").GetComponent<Image>();
            c = im.color;
            c.a = 1;
            im.color = c;

            blockDetection.textureX = 5f;
            blockDetection.textureY = 16f;
        }
    }


    public void TransparencySet()
    {
        foreach (Transform item in gameObject.transform)
        {
            item.gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
        }
    }
}
