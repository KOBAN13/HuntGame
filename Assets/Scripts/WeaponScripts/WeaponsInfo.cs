using System;
using ScriptsHumanoid;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Weapon
{
    [CreateAssetMenu(fileName = "WeaponsInfo", menuName = "Create New Weapon Info")]
    public class WeaponsInfo : ScriptableObject
    {
        [SerializeField] private int ammoInMagazine;
        [SerializeField] private int tempAmmo;
        [SerializeField] private int totalAmmo; //переделать
        [SerializeField] private GameObject prefabBullet;
        [SerializeField] private float reloadTime;
        [SerializeField] private float rateOfFire;
        [SerializeField] private GameObject prefabMagazine; //вынести отдельно в ивенты
        [SerializeField] private GameObject prefabSleeve; //вынести отдельно в ивенты//
        [SerializeField] private BulletsInfo bullets;
        [SerializeField] private int launchForceSleepy;
        [SerializeField] private int torsionForceSleepy;

        public int AmmoInMagazine
        {
            get => ammoInMagazine;
            set
            {
                if(value < 0)
                    throw new ArgumentException("не правильное значение патронов в магазине");
                ammoInMagazine = value;
            }
        }
        public int TempAmmo => tempAmmo;

        public int TotalAmmo
        {
            get => totalAmmo;
            set
            {
                if (value < 0)
                    throw new ArgumentException("не правильная установка остатка патронов");
                totalAmmo = value;
            }
        }
        public GameObject PrefabBullet => prefabBullet;
        public float ReloadTime => reloadTime;
        public float RateOfFire => rateOfFire;
        public GameObject PrefabMagazine => prefabMagazine;
        public GameObject PrefabSleeve => prefabSleeve;
        public BulletsInfo BulletsInfo => bullets;
        public int LaunchForceSleepy => launchForceSleepy;
        public int TorsionForceSleepy => torsionForceSleepy;


    }

}
