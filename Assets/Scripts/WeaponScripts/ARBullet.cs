using UnityEngine;
using UnityEngine.UIElements;

namespace Weapon
{
    public class ARBullet : Bullets
    {
        private Vector3 previousPosition;
        private LayerMask hitLayerMask;
        GameObject hitGameObjectNext;
        public void Start()
        {
            Destroy(gameObject, 10f);
            previousPosition = transform.position;
            hitLayerMask = ~(1 << LayerMask.NameToLayer("bullets"));
        }

        public void Update()
        {
            if(Input.GetButton("Fire1"))
            {
                CheckRayObject();
            }
        }

        protected void CheckRayObject()
        {
            Vector3 currentPosition = transform.position;
            Ray currentRayPosition = new(currentPosition, transform.forward);
            Ray previousBulletRay = new(previousPosition, transform.forward);
            RaycastHit hit;
            string hitGameObjectCurrent = null;
            string hitGameObjectPrevious = null;

            if (Physics.Raycast(currentRayPosition, out hit, Mathf.Infinity, hitLayerMask))
            {
                hitGameObjectCurrent = hit.collider.gameObject.name;
            }
            if(Physics.Raycast(previousBulletRay, out hit, Mathf.Infinity, hitLayerMask))
            {
                hitGameObjectPrevious = hit.collider.gameObject.name;
                hitGameObjectNext = hit.collider.gameObject;
            }
            if(hitGameObjectPrevious != hitGameObjectCurrent)
            {
                GetDamage(hitGameObjectNext);
            }
            previousPosition = currentPosition;
        }
    }
}
