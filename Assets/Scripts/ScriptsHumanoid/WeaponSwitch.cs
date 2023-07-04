using UnityEngine;
using Weapon;

namespace ScriptsHumanoid
{
    public class WeaponSwitch : MonoBehaviour
    {
        private Invoker _invoker;
        private SwapWeapons _swapWeapon;
        [SerializeField] private Transform weapon;
        [SerializeField] private AR arWeapon;
        [SerializeField] private Pistols pistols;
        [SerializeField] private Transform weaponHolder;
        [SerializeField] private Transform weaponMove;
        [SerializeField] private WeaponSwitch[] switchesWeapon;
        private bool _isPistolEquipped;
        private bool _isAREquipped;
        
        public bool IsPistolEquipped => _isPistolEquipped;
        public bool IsAREquipped => _isAREquipped;

        public bool IsEquipped { get; private set; }
        private void Start()
        {
            _invoker = GetComponent<Invoker>();
            _swapWeapon = GetComponent<SwapWeapons>();
        }
        private void Update()
        {
            switch (Input.inputString)
            {
                case "1" :
                    ChangeWeapons(ref _isPistolEquipped, ref _isAREquipped, 0);
                    ChooseWeapon(0, ref _isAREquipped);
                    arWeapon.CHE(arWeapon);
                    break;
                case "2" :
                    ChangeWeapons(ref _isAREquipped, ref _isPistolEquipped, 1);
                    ChooseWeapon(1, ref _isPistolEquipped);
                    pistols.CHE(pistols);
                    break;
                case "x": AwayWeapon();
                    break;
                case "ч": AwayWeapon();
                    break;
            }
        }
        private void SwapWeapon()
        {
            _invoker.SetCommand(_swapWeapon);
            _invoker.SwapWeapon(weaponMove, weapon);
            
        }

        private void AwayWeapon()
        {
            _invoker.SetCommand(_swapWeapon);
            _invoker.AwayWeapon(weaponHolder, weapon);
            IsEquipped = false;
        }

        private void ChooseWeapon(int index, ref bool switchWeapon)
        {
            if (!IsEquipped)
            {
                switchesWeapon[index].SwapWeapon();
                IsEquipped = true;

                switchWeapon = true;
            }
        }

        private void ChangeWeapons(ref bool weaponEquip, ref bool switchWeapon, int index)
        {
            if (weaponEquip)
            {
                AwayWeapon();
                ChooseWeapon(index, ref switchWeapon);
                weaponEquip = false;
            }
        }
    }
}