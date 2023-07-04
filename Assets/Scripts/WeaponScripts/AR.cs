using ScriptsHumanoid;

namespace Weapon
{
    public class AR : Weapons, IWeapon
    {
        public void Start()
        {
            weaponsInfo.AmmoInMagazine = weaponsInfo.TempAmmo;
            WeaponSwap = GetComponent<WeaponSwitch>();
        }

        public void Reload()
        {
            ReloadWeapon();
        }

        public void Shoot()
        {
            float delay = 1 / (weaponsInfo.RateOfFire / 60);
            CheckBeforeShooting(delay);
        }

        public void CHE(AR weapons)
        {
            playerCombat.EquipWeapon(weapons);
        }
    }

}
