using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Weapon
{
    public class Bullets : MonoBehaviour
    {
        [SerializeField] public int _ammoDamage;
        [SerializeField] public string[] _tagList;
        [SerializeField] public int _ammoSpeed;
        public HPObject hPObject;

        public void OnCollisionEnter(Collision collision)
        {
            GetDamage(collision.gameObject);
        }

        protected void GetDamage(GameObject gameObj)
        {
            if (CheckTag(gameObj))
            {
                Debug.Log("hit persona AMOGUS");
                hPObject = gameObj.transform.GetComponent<HPObject>();
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
}
