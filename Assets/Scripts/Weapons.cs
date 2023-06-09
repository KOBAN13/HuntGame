using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] protected int ammoInMagazine;
    [SerializeField] protected int tempAmmo;
    [SerializeField] protected int totalAmmo;
    [SerializeField] protected float rateOfFire;
    [SerializeField] protected Transform shootPoint;
    [SerializeField] protected Camera cam;
    [SerializeField] protected GameObject prefabBullets;
    protected Bullets bullets;
    protected bool isReloading;
    protected float fireTimer;

    [SerializeField] protected float reloadTime;
    protected int TempAmmo
    {
        set { tempAmmo = ammoInMagazine; }
        get { return tempAmmo; }
    }
}
