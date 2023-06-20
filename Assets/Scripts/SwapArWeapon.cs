using UnityEngine;

public class SwapArWeapon : MonoBehaviour, ISwap
{
    public void AwayWeapon(Transform _weaponHolder, Transform _weapon)
    {
        TransformPostiton(_weaponHolder, _weapon);
    }

    public void SwapWeapon(Transform _moveWeapon, Transform _weapon)
    {
        TransformPostiton(_moveWeapon, _weapon);
    }

    private void TransformPostiton(Transform weaponMove, Transform _weapon)
    {
        _weapon.transform.SetParent(weaponMove);
        _weapon.transform.localPosition = Vector3.zero;
        _weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);     
    }
}
