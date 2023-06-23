using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapon;

public class Interaction : MonoBehaviour
{
    public TextMeshProUGUI indicator;
    public Camera cameraPeson;
    [SerializeField] private AR weaponAR;
    private void Start()
    {
        cameraPeson.GetComponent<Camera>();
        weaponAR = GetComponent<AR>();
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
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        item.Interaction();
                        weaponAR.totalAmmo += 180;

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



