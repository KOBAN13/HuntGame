using System;
using UnityEngine;
using Weapon;

namespace ScriptsHumanoid
{
    public class PlayerCombat : MonoBehaviour
    { 
        private IWeapon _weapon;

        private void Shoot()
        {
            if(_weapon == null)
                return;
            
            if (_weapon.WeaponSwitch.IsEquipped)
            {
                if (Input.GetKey(KeyCode.R))
                    _weapon?.Reload();
                if (_weapon.WeaponSwitch.IsPistolEquipped && Input.GetButtonDown("Fire1"))
                    _weapon?.Shoot();
                if (Input.GetButton("Fire1") && _weapon.WeaponSwitch.IsAREquipped)
                    _weapon?.Shoot();
            }
        }

        public void Update()
        {
            Shoot();
        }

        public void EquipWeapon(IWeapon weapon)
        {
            _weapon = weapon;
        }
    }
}