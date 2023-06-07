using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    WeaponManager weaponManager; //Getting the WeaponManager script.
    GameObject item; 
    Animator anim;

    public Transform rifleHolder; //This is the parent of the weapon of type Rifle.
    public Transform pistolHolder; //This is the parent of the weapon of type Pisotl.

    public bool canPickUp; //the variable to determine if we can pick up the weapon.
    public bool spaceLeft; ////the variable to determine if we still have space left to pick up a weapon.
    public bool canShoot; //the variable to determine if we can shoot or fire the weapon.

    // Start is called before the first frame update
    void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
        anim = GetComponent<Animator>();
        item = GetComponent<GameObject>();

        canShoot = false; //Setting the canShoot at the start of the Game Scene, to tell Unity that we can not shoot the weapon because it is not in the player's hands.
        spaceLeft = true; //We have space left to pick up a new or another weapon.
    }

    // Update is called once per frame
    void Update()
    {
        if (canPickUp == true && Input.GetKeyDown(KeyCode.F) && spaceLeft == true)
        {
            if (item.tag == "rifle") //If the weapon's tag is equal to Rifle, then we are going to get the RiflePickUp() function.
            {
                RiflePickUp(); //The RiflePickUp() function.
            }
            else if (item.tag == "pistol") //If the weapon's tag is equal to Pistol, then we are going to get the PistolPickUp() function.
            {
                PistolPickUp(); //The PistolPickUp() function.
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject)
        {
            Debug.Log("Press F to pick up"); //Prompting the user to press the F key to pick up the item.
            canPickUp = true; //Enabling the player or user to pick up the weapon or item.
            item = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject)
        {
            canPickUp = false; //if set to false then the player wont be able to pick up the weapon or item.
        }
    }

    void RiflePickUp()
    {
        Debug.Log(item.name);

        item.transform.SetParent(rifleHolder);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.Euler(Vector3.zero);
        item.transform.localScale = Vector3.one;

        item.GetComponent<Rigidbody>().isKinematic = true;
        item.GetComponent<Collider>().isTrigger = true;

        spaceLeft = false;
        canShoot = true;

        anim.SetTrigger("PickUpRifle");
        anim.SetBool("RifleOn", true);

        weaponManager.selectedWeapon = 2;
        weaponManager.canSwitch = true;
        weaponManager.rifleEquipped = true;
    }

    void PistolPickUp()
    {
        Debug.Log(item.name);

        item.transform.SetParent(pistolHolder);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.Euler(Vector3.zero);
        item.transform.localScale = Vector3.one;

        item.GetComponent<Rigidbody>().isKinematic = true;
        item.GetComponent<Collider>().isTrigger = true;

        spaceLeft = false;
        canShoot = true;

        weaponManager.selectedWeapon = 1;
        weaponManager.canSwitch = true;
        weaponManager.pistolEquipped = true;

        anim.SetTrigger("PickUpPistol");
        anim.SetBool("PistolOn", true);
    }
}
