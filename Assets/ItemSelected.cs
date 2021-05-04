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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gameObject.GetComponent<Image>().color = Color.red;

            blockDetection.textureX = 4f;
            blockDetection.textureY = 15f;
        }
    }
}
