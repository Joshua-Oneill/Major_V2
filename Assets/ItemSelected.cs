using UnityEngine;
using UnityEngine.UI;

public class ItemSelected : MonoBehaviour
{

    public BlockDetection blockDetection;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SelectItem();
    }

    public void SelectItem()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) //Grass 
        {
            Vector2[] offset = calcUVs(4, 3);
            gameObject.GetComponent<Image>().sprite.te
            blockDetection.textureX = 3f;
            blockDetection.textureY = 16f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) //Stone
        {
            gameObject.GetComponent<Image>().color = Color.red;

            blockDetection.textureX = 1f;
            blockDetection.textureY = 15f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) //
        {
            gameObject.GetComponent<Image>().color = Color.red;

            blockDetection.textureX = 4f;
            blockDetection.textureY = 15f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            gameObject.GetComponent<Image>().color = Color.red;

            blockDetection.textureX = 4f;
            blockDetection.textureY = 15f;
        }
    }


    Vector2[] calcUVs(float x, float y)
    {
        float offsetX = 0.0625f * x;
        float offsetY = 0.0625f * y;

        return new Vector2[4]
        {
                new Vector2(offsetX - 0.0625f, offsetY - 0.0625f),  //left bottom corner           
                new Vector2(offsetX, offsetY - 0.0625f),            //right bottom corner
                new Vector2(offsetX, offsetY),                      //right top corner              
                new Vector2(offsetX - 0.0625f, offsetY)             //left top corner 

        };
    }
}
