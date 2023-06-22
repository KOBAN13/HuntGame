using System.Collections;
using UnityEngine;

namespace Weapon
{
    public class AR : Weapons, IWeapon
    {
        public void Reload()
        {
            if (totalAmmo <= 0)
                return;
            int countAmmoAfterReloading = TempAmmo;
            countAmmoAfterReloading -= ammoInMagazine;

            totalAmmo -= countAmmoAfterReloading;
            ammoInMagazine = tempAmmo;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void Shoot()
        {
            if (ammoInMagazine <= 0)
                return;
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // создание луча от центра камеры
            RaycastHit hit; // ЭТО структура которая хранит информацию о обьектах которые попали под пересечение луча
            Vector3 targetHit; // вектор для луча

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("im hit a " + hit.transform.name);
                targetHit = hit.point;                                  //в целом эта конструкция делает так что получает точку до цели если метод рейкаст возвращает тру то структура рейкаст
                                                                        //возвращает точку пересечения с обьектом
            }
            else
            {
                Debug.Log("im watch nofing");
                targetHit = ray.GetPoint(50);
            }
            Vector3 bulletDistanse = targetHit - shootPoint.position; //дистанция от точки вылета пули до цели
            Vector3 newBulletDistanse = bulletDistanse + new Vector3(0f, 0f, 0f);// новое направление пули
            GameObject newBullet = Instantiate(prefabBullets, shootPoint.position, Quaternion.identity); //эта строчка созхдает новый обьект пули из префаба, создается в точке выстрела
                                                                                                         //и последний аргумент использеться для получения ориентации от родительского обьекта


            Vector3 direction = Quaternion.Euler(Random.Range(-1f, 1f), Random.Range(1f, -1f), Random.Range(1f, -1f)) * newBulletDistanse.normalized;
            newBullet.transform.forward = direction.normalized;     //эта строка устанавливает направелние обьекта новой пули в направлении newBulletDistance
            newBullet.GetComponent<Rigidbody>().AddForce(direction * bullets._ammoSpeed, ForceMode.Impulse); //получение компонента риджитбади из пули и добавление к нему метода
                                                                                                             //которые добавляет различные свойства


            GameObject newSleeve = Instantiate(prefabSleeve, spawnSleeve.position , spawnSleeve.rotation);
            Vector3 forseSleepy = spawnSleeve.right * 1f + spawnSleeve.up * Random.Range(0,1f);
            newSleeve.GetComponent<Rigidbody>().AddForce(forseSleepy * 3f, ForceMode.Impulse);
            newSleeve.GetComponent<Rigidbody>().AddTorque(Random.insideUnitSphere * 3f);

            recoil.ShootRecoil(); //отдача
            ammoInMagazine--; //вычитане пули из магазина
        }

        public void Start()
        {
            animWeapon = GetComponent<Animator>();
            ammoInMagazine = tempAmmo;
            weaponSwap = GetComponent<WeaponSwitch>();
            bullets = GameObject.FindWithTag("bullets").GetComponent<ARBullet>();
        }

        public void Update()
        {
            if (weaponSwap.isEquipped && weaponSwap != null)
            {
                ShootindMode();
                if (Input.GetKey(KeyCode.R))
                {
                    CheckReloadAndTrigger();
                }
                if (Input.GetButton("Fire1") && !shootingMode)
                {
                    var animation = "Shoot";
                    var delayShoot = 1 / (rateOfFire / 60);
                    ShootAnyModes(delayShoot, animation);
                    recoil.ShakeCamera();
                }
                if (shootingMode && Input.GetButtonDown("Fire1"))
                {
                    string animation = "Shoot1";
                    float delayShoot = 0.0055f;
                    ShootAnyModes(delayShoot, animation);
                }
            }
        }

        private void ShootAnyModes(float delayShoot, string animation)
        {
            time += Time.deltaTime;
            if (ammoInMagazine > 0 && isShoot)
            {
                if (time > delayShoot)
                {
                    ShootAndAnimations(animation);
                    time = 0;
                }
            }
            else
            {
                CheckReloadAndTrigger();
            }
        }

        private void ShootAndAnimations(string animation)
        {
            Shoot();
            animWeapon.SetTrigger(animation);
            //particleShoot.Play();
        }

        private void CheckReloadAndTrigger()
        {
            if (totalAmmo > 0 && !isReloading)
            {
                TriggerAnimationsReload();
            }
        }

        private IEnumerator Wait()
        {
            isReloading = true;
            isShoot = false;
            yield return new WaitForSeconds(reloadTime);

            isReloading = false;
            Reload();
            isShoot = true;
        }

        private void DropMagazine() //ивент по выпаданию магазина из оружия
        {
            GameObject magazine = Instantiate(prefabMagazine, spawnMagazine.position, spawnMagazine.rotation);
            Rigidbody prefabMagazin = magazine.GetComponent<Rigidbody>();
            prefabMagazin.AddForce(spawnMagazine.forward * 1f, ForceMode.Impulse);
        }

        private void TriggerAnimationsReload()
        {
            animWeapon.SetTrigger("Reloading");
            StartCoroutine(Wait());
        }

        private bool ShootindMode()
        {
            if (Input.GetKey(KeyCode.B))
            {
                shootingMode = false;
            }
            if (Input.GetKey(KeyCode.V))
            {
                shootingMode = true;
            }
            return shootingMode;
        }
    }

}
