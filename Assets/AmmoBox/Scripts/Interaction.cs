using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public Text indicator;
    public Camera cameraPeson;
     private void Start()
    {
        cameraPeson.GetComponent<Camera>();

    }
    void Update()
    {
        RaycastHit hit;
        Ray ray = cameraPeson.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit))
        {
       
            if (hit.transform.name == "Object005") 
            {
                Item item = hit.collider.GetComponent<Item>();
             

                if (item != null)
                {
                    indicator.enabled = true;
                    Debug.Log("huy2");
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        item.Interaction();
                    }
                }
            }
            else
            {
                indicator.enabled = false;
            } 
        }
        else
        {
            indicator.enabled = false;
        }
    }
}



