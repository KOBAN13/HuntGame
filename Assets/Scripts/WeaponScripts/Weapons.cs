using System;
using System.Collections;
using ScriptsHumanoid;
using UnityEngine;
using UnityEngine.Serialization;

namespace Weapon
{
    public class Weapons : MonoBehaviour
    {
        private Vector3 _newBulletDistanse;
        private float DelayTimeShoot;
        private bool _isReloading;
        protected bool IsShoot = true;
        protected WeaponSwitch WeaponSwap; //������� � ��������� ��������
        [SerializeField] protected WeaponsInfo weaponsInfo;
        [SerializeField] protected Recoil recoilWeapon;
        [SerializeField] protected Camera camWeapon;
        [SerializeField] protected ControlerVisual controlerVisual;
        [SerializeField] protected PlayerCombat playerCombat;

        public WeaponsInfo WeaponsInfo => weaponsInfo;
        public ControlerVisual ControlerVisual => controlerVisual;

        public WeaponSwitch WeaponSwitch
        {
            get => WeaponSwap;
            set => WeaponSwap = value;
        }

        public virtual void ReloadWeapon()
        {
            switch (weaponsInfo.TotalAmmo)
            {
                case <= 0:
                    return;
                case >= 0 when !_isReloading:
                    EventManager.WeaponAnimation += controlerVisual.TriggerAnimation;
                    TriggerAnimationsReload("Reloading");
                    int countAmmoAfterReloading = weaponsInfo.TempAmmo;
                    countAmmoAfterReloading -= weaponsInfo.AmmoInMagazine;

                    weaponsInfo.TotalAmmo -= countAmmoAfterReloading;
                    EventManager.WeaponAnimation -= controlerVisual.TriggerAnimation;
                    break;
            }
            weaponsInfo.AmmoInMagazine = weaponsInfo.TempAmmo;
        }
        
        private void AddForseMagazineAndSleepy(int launchForce, int torsionForce, (float, float) spreadAngle)
        {
            EventManager.OnSpawnBullet(_newBulletDistanse, spreadAngle);
            EventManager.OnSpawnSleeve(launchForce, torsionForce);
        }

        public virtual void ShootWeapon()
        {
            controlerVisual.OnActivateEvent();
            if (weaponsInfo.AmmoInMagazine <= 0)
                return;
            Ray ray = camWeapon.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // �������� ���� �� ������ ������
            RaycastHit hit; // ��� ��������� ������� ������ ���������� � �������� ������� ������ ��� ����������� ����
            Vector3 targetHit; // ������ ��� ����

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("im hit a " + hit.transform.name);
                targetHit = hit.point;                                  //� ����� ��� ����������� ������ ��� ��� �������� ����� �� ���� ���� ����� ������� ���������� ��� �� ��������� �������
                //���������� ����� ����������� � ��������
            }
            else
            {
                Debug.Log("im watch nofing");
                targetHit = ray.GetPoint(50);
            }
            _newBulletDistanse = targetHit - controlerVisual.ShootPoint.position; //��������� �� ����� ������ ���� �� ����
            
            AddForseMagazineAndSleepy(weaponsInfo.LaunchForceSleepy, weaponsInfo.TorsionForceSleepy, (1,-1));
            recoilWeapon.ShootRecoil(); //������
            weaponsInfo.AmmoInMagazine--; //�������� ���� �� ��������
        }
        
        protected void CheckBeforeShooting(float delay)
        {
            DelayTimeShoot += Time.deltaTime;
            if (weaponsInfo.AmmoInMagazine > 0 && IsShoot)
            {
                if (DelayTimeShoot > delay)
                {
                    ShootWeapon();
                    TriggerAnimationShoot("Shoot");
                    DelayTimeShoot = 0;
                    controlerVisual.OnDisableEvent();
                }
            }
            else
            {
                ReloadWeapon();
            }
        }
        
        private IEnumerator WaitReload(float reloadTime)
        {
            _isReloading = true;
            IsShoot = false;
            yield return new WaitForSeconds(reloadTime);

            _isReloading = false;
            IsShoot = true;
        }
        
        private void TriggerAnimationsReload(string nameTrigger)
        {
            EventManager.OnActivationAnimate(nameTrigger);
            StartCoroutine(WaitReload(weaponsInfo.ReloadTime));
        }

        private void TriggerAnimationShoot(string nameTrigger)
        {
            EventManager.OnActivationAnimate(nameTrigger);
        }
    }
}