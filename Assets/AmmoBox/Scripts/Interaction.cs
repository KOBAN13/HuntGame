using TMPro;
using UnityEngine;
using Weapon;

public class Interaction : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI indicator;
    [SerializeField] private Camera cameraPeson;
    [SerializeField] private AR weaponAR;
    
    
    public void OpenAndCloseAmmoBox()
    {
        var ray = cameraPeson.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out var hit))
        {
            if (hit.collider.TryGetComponent<Item>(out var box)) 
            {
                indicator.enabled = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    box.Interaction();
                }
            }
        }
        else
        {
            indicator.enabled = false;
        }
    }
}



