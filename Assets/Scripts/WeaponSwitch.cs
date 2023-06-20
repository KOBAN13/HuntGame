using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    private Invoker _invoker;
    private SwapArWeapon _swapArWeapon;
    [SerializeField] private Transform _weapon;
    [SerializeField] private Transform _weaponHolder;
    [SerializeField] private Transform _weaponMove;
    public bool isEquipped { get; private set; }
    private void Start()
    {
        _invoker = GetComponent<Invoker>();
        _swapArWeapon = GetComponent<SwapArWeapon>();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SwapAr();
            isEquipped = true;
        }
        if(Input.GetKeyUp(KeyCode.X))
        {
            AwayAr();
            isEquipped = false;
        }
    }

    private void SwapAr()
    {
        _invoker.SetCommand(_swapArWeapon);
        _invoker.SwapWeapon(_weaponMove, _weapon);
    }

    private void AwayAr()
    {
        _invoker.SetCommand(_swapArWeapon);
        _invoker.AwayWeapon(_weaponHolder, _weapon);
    }
}


public interface ISwap
{
    public void SwapWeapon(Transform _weaponMove, Transform _weapon);

    public void AwayWeapon(Transform _weaponHolder, Transform _weapon);
}

public class SwapPistol : ISwap
{
    public void AwayWeapon(Transform _weaponMove, Transform _weaponHolder)
    {
        throw new System.NotImplementedException();
    }

    public void SwapWeapon(Transform _weaponMove, Transform _weaponHolder)
    {
        throw new System.NotImplementedException();
    }
}