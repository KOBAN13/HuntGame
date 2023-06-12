using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] protected int ammoInMagazine;
    [SerializeField] protected int tempAmmo;
    [SerializeField] protected int totalAmmo;
    [SerializeField] protected Transform shootPoint;
    [SerializeField] protected Camera cam;
    [SerializeField] protected GameObject prefabBullets;
    [SerializeField] protected float reloadTime;
    [SerializeField] protected Bullets bullets;
    [SerializeField] protected float rateOfFire;
    protected bool shootingMode;
    protected float time;
    protected bool isReloading;
    protected Animator animWeapon;
    protected ParticleSystem particleShoot;
    protected int TempAmmo
    {
        set { tempAmmo = ammoInMagazine; }
        get { return tempAmmo; }
    }
}
