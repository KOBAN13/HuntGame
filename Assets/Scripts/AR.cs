using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        //проигрование анимаций
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
        GameObject newBullet = Instantiate(prefabBullets, shootPoint.position, Quaternion.identity); //эта строчка созхдает новый обьект пули из префаба, создается в точке выстрела
                                                                                                     //и последний аргумент использеться для получения ориентации от родительского обьекта

        bullets = new ARBullet();
        newBullet.transform.forward = newBulletDistanse.normalized; //эта строка устанавливает направелние обьекта новой пули в направлении newBulletDistance
        newBullet.GetComponent<Rigidbody>().AddForce(bulletDistanse.normalized * bullets._ammoSpeed, ForceMode.Impulse); //получение компонента риджитбади из пули и добавление к нему метода
                                                                                                                         //которые добавляет различные свойства
        ammoInMagazine--; //вычитане пули из магазина
        fireTimer = 0.0f;
    }

    public void Start()
    {
        cam = GetComponent<Camera>();
        //получение анимации
        ammoInMagazine = tempAmmo;
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            if (totalAmmo > 0 && !isReloading)
                StartCoroutine("Wait");
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (ammoInMagazine > 0)
                Shoot();
            if (totalAmmo > 0 && !isReloading)
                StartCoroutine("Wait");
        }
        if (fireTimer < rateOfFire)
            fireTimer += Time.deltaTime;
    }

    private IEnumerable Wait()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        isReloading = false;
        Reload();
    }
}
