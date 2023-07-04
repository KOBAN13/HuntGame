using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Weapon
{
    public class Bullets : MonoBehaviour
    {
        [SerializeField] private BulletsInfo bullet;
        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<HPObject>(out var hpObject))
            {
                hpObject.CheckDestroy(bullet.AmmoDamage);
                Destroy(gameObject);
            }
        }

        public void Start()
        {
            Destroy(gameObject, 10f);
        }
    }
}
