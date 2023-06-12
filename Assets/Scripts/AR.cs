using Assets.Scripts;
using System.Collections;
using UnityEngine;

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

    public void Shoot()
    {
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
            targetHit = ray.GetPoint(75);
        }
        Vector3 bulletDistanse = targetHit - shootPoint.position; //дистанция от точки вылета пули до цели
        Vector3 newBulletDistanse = bulletDistanse + new Vector3(0f, 0f, 0f);// новое направление пули
        GameObject newBullet = Instantiate(prefabBullets, shootPoint.position, Quaternion.Euler(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0)); //эта строчка созхдает новый обьект пули из префаба, создается в точке выстрела
                                                                                                                                                       //и последний аргумент использеться для получения ориентации от родительского обьекта
        newBullet.transform.forward = newBulletDistanse.normalized ;     //эта строка устанавливает направелние обьекта новой пули в направлении newBulletDistance
        newBullet.GetComponent<Rigidbody>().AddForce(bulletDistanse.normalized * bullets._ammoSpeed, ForceMode.Impulse); //получение компонента риджитбади из пули и добавление к нему метода
                                                                                                                         //которые добавляет различные свойства
        ammoInMagazine--; //вычитане пули из магазина
    }

    public void Start()
    {
        animWeapon = GetComponent<Animator>();
        ammoInMagazine = tempAmmo;
        cam = GameObject.FindWithTag("camWeapon").GetComponent<Camera>();
        bullets = GameObject.FindWithTag("bullets").GetComponent<ARBullet>();
        particleShoot = GameObject.FindWithTag("particalShoot").GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        ShootindMode();
        if (Input.GetKey(KeyCode.R))
        {
            if (totalAmmo > 0 && !isReloading)
            {
                TriggerAnimationsReload();
                StartCoroutine(Wait());
            }
        }
        if(Input.GetButton("Fire1") && !shootingMode)
        {
             time += Time.deltaTime;
             if (ammoInMagazine > 0)
             {
                if(time > 1 / (rateOfFire / 60))
                {
                    Shoot();
                    animWeapon.SetTrigger("Shoot");
                    particleShoot.Play();
                    time = 0;
                }
             }
            else if (totalAmmo > 0 && !isReloading)
            {
                TriggerAnimationsReload();
                StartCoroutine(Wait());
            }
        }
        if(shootingMode && Input.GetButtonDown("Fire1"))
        {
            if (ammoInMagazine > 0)
            {
                Shoot();
                animWeapon.SetTrigger("Shoot1");
                particleShoot.Play();
            }
            else if (totalAmmo > 0 && !isReloading)
            {
                TriggerAnimationsReload();
                StartCoroutine(Wait());
            }
        }
    }

    private IEnumerator Wait()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        isReloading = false;
        Reload();
    }

    private void TriggerAnimationsReload()
    {
        animWeapon.SetTrigger("Reloading");
        animWeapon.SetTrigger("Reloading1");
    }

    private bool ShootindMode()
    {
        if(Input.GetKey(KeyCode.B))
        {
            shootingMode = false;
        }
        if(Input.GetKey(KeyCode.V))
        {
            shootingMode = true;
        }
        return shootingMode;
    }
}
