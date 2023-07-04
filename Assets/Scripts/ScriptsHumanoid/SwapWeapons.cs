using UnityEngine;

namespace ScriptsHumanoid
{
    public class SwapWeapons : MonoBehaviour, ISwap
    {
        public void AwayWeapon(Transform weaponHolder, Transform weapon)
        {
            TransformPostiton(weaponHolder, weapon);
        }

        public void SwapWeapon(Transform moveWeapon, Transform weapon)
        {
            TransformPostiton(moveWeapon, weapon);
        }

        private void TransformPostiton(Transform weaponMove, Transform weapon)
        {
            weapon.transform.SetParent(weaponMove);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
