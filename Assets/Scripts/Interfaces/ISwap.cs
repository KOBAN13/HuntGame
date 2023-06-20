using UnityEngine;

public interface ISwap
{
    public void SwapWeapon(Transform _weaponMove, Transform _weapon);

    public void AwayWeapon(Transform _weaponHolder, Transform _weapon);
}
