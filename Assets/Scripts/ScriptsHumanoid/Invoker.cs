using UnityEngine;

namespace ScriptsHumanoid
{
    public class Invoker : MonoBehaviour
    {
        private ISwap _swapWeapon;

        public void SetCommand(ISwap swapWeapon)
        {
            _swapWeapon = swapWeapon;
        }
        public void SwapWeapon(Transform _weaponMove, Transform _weapon)
        {
            _swapWeapon.SwapWeapon(_weaponMove, _weapon);
        }
        public void AwayWeapon(Transform _weaponHolder, Transform _weapon)
        {
            _swapWeapon.AwayWeapon(_weaponHolder, _weapon);
        }
    }

}
