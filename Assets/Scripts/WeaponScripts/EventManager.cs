using System;
using UnityEngine;

namespace Weapon
{
    public static class EventManager
    {
        public static event Action<string> WeaponAnimation;
        public static event Action<Vector3, (float, float)> SpawnBullet;
        public static event Action<int, int> SpawnSleeve; 


        public static void OnActivationAnimate(string animation)
        {
            WeaponAnimation?.Invoke(animation);
            Debug.Log("Анимация проиграна");
        }

        public static void OnSpawnBullet(Vector3 newBulletDistanse, (float, float) spreadAngle)
        {
            SpawnBullet?.Invoke(newBulletDistanse, spreadAngle);
            Debug.Log("пуля созданна");
        }

        public static void OnSpawnSleeve(int launchForce, int torsionForce)
        {
            SpawnSleeve?.Invoke(launchForce, torsionForce);
            Debug.Log("гильза создана");
        }
    }
}