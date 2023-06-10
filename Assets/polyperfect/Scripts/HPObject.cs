using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPObject : MonoBehaviour
{
    [SerializeField] private float _hpObject;
    [SerializeField] private float arrmor;

    public float currentHp
    {
        get { return _hpObject; }
    }

    public void CheckDestroy(float damage)
    {
        _hpObject -= damage;
        if(_hpObject <=0)
        {
            Destroy(gameObject);
        }
    }
}
