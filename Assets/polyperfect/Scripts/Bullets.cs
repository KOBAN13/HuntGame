using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullets : MonoBehaviour 
{
    [SerializeField] public int _ammoDamage;
    [SerializeField] public string[] _tagList;
    [SerializeField] public int _ammoSpeed;
    public HPObject hPObject;

    public void OnCollisionEnter(Collision collision)
    {
        if(CheckTag(collision.gameObject))
        {
            Debug.Log("hit persona");
            hPObject = collision.transform.GetComponent<HPObject>();
            hPObject.CheckDestroy(_ammoDamage);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }

    public bool CheckTag(GameObject obj)
    {
        foreach (var tag in _tagList)
        {
            if (obj.tag == tag)
                return true;
        }
        return false;
    }
}
