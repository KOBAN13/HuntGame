using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] protected int ammoInMagazine;
    [SerializeField] protected int tempAmmo;
    [SerializeField] protected int totalAmmo;
    protected bool isAmmoInMagazine;
    protected GameObject prefabBullets;
    [SerializeField] protected float rateOfFire;
    protected Transform shootPoint;
    protected bool canShootWeapon;

    [SerializeField] protected float reloadTime;
    protected int TempAmmo
    {
        set { tempAmmo = ammoInMagazine; }
        get { return tempAmmo; }
    }
}

public class AR : Weapons, IWeapon
{
    private Bullets _bullets = new Bullets();
    public void Reload()
    {
        if (totalAmmo <= 0)
            return;
        int countAmmoAfterReloading = TempAmmo;
        countAmmoAfterReloading -= ammoInMagazine;

        totalAmmo -= countAmmoAfterReloading;
        ammoInMagazine = tempAmmo;
        //проигрование анимаций
        Wait(); // пока заглушка чтоб сразу не стерляли
    }

    public void Shoot()
    {
        throw new System.NotImplementedException();
    }

    private IEnumerable Wait()
    {
        yield return new WaitForSeconds(reloadTime);
    }
}
