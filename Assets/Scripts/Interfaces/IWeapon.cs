using ScriptsHumanoid;
using Weapon;

public interface IWeapon
 {
     public void Reload();
     public void Shoot();
     
     public WeaponSwitch WeaponSwitch { get; }
     
 }
