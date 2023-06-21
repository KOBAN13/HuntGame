using UnityEngine;

namespace Weapon
{
    public class Weapons : MonoBehaviour
    {
        [SerializeField] protected int ammoInMagazine;
        [SerializeField] protected int tempAmmo;
        [SerializeField] public int totalAmmo; //переделать
        [SerializeField] protected Transform shootPoint;
        [SerializeField] protected Camera cam;
        [SerializeField] protected GameObject prefabBullets;
        [SerializeField] protected float reloadTime;
        [SerializeField] protected float rateOfFire;
        [SerializeField] protected Transform spawnMagazine;
        [SerializeField] protected GameObject prefabMagazine;
        protected bool shootingMode;
        protected float time;
        protected bool isReloading;
        protected Animator animWeapon;
        protected ParticleSystem particleShoot;
        protected bool isShoot = true;
        protected ARBullet bullets;  //хз че то надо с этим сделать
        [SerializeField] protected Recoil recoil;
        protected WeaponSwitch weaponSwap;
        protected int TempAmmo
        {
            set { tempAmmo = ammoInMagazine; }
            get { return tempAmmo; }
        }
    }
}