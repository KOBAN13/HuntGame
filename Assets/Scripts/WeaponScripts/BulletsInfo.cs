using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu (fileName = "BulletsInfo", menuName = "GamePlay/New Bullets")]
    public class BulletsInfo : ScriptableObject
    {
        [SerializeField] private int _ammoDamage;
        [SerializeField] private int _ammoSpeed;
        public int AmmoDamage => _ammoDamage;
        public int AmmoSpeed => _ammoSpeed;
    }
}
