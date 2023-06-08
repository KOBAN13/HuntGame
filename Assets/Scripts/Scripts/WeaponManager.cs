using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Transform weapons;
    public int selectedWeapon;

    private WeaponPickUp pickUp;

    private Animator anim;

    public Transform rifleOff;
    public Transform rifleParent;
    public Transform pistolParent;
    public Transform pistolOff;

    public bool canSwitch;
    public bool pistolEquipped;
    public bool rifleEquipped;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); //Geting the Animator Component from the player model inside the Inspector
        pickUp = GetComponent<WeaponPickUp>(); //Geting the WeaponPickUp Component from the player model inside the Inspector
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0 && canSwitch == true) //This checks if the User has scrolled the mouse wheel up and if he can switch to the next weapon
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0; //The weapon gets set here.
            else
                selectedWeapon++; //If not, the selectedWeapon increments and the next weapon is selected.

            SwitchWeapon();
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0 && canSwitch == true) //This checks if the User has scrolled the mouse wheel down and if he can switch to the previous weapon
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--; //If not, the selectedWeapon increments and the previous weapon is selected.

            //Calling the SwitchWeapon() method.
            SwitchWeapon();
        }
    }

    void SwitchWeapon()
    {
        if(selectedWeapon == 0)
        {
            Debug.Log("No weapon selected");

            //Setting and controlling the animations on the player to release the weapon
            anim.SetTrigger("Release");
            anim.SetBool("RifleOn", false);
            anim.SetBool("PistolOn", false);

            rifleEquipped = false;
            pistolEquipped = false;
        }

        if(selectedWeapon == 1)
        {
            Debug.Log("Pistol Selected");

            //Setting and controlling the animations on the player to grab the pistol
            anim.SetTrigger("Grabbing");
            anim.SetBool("PistolOn", true);

            pistolEquipped = true; //Setting the boolean variable to true, to tell the player that the pistol is equipped
        }

        if (selectedWeapon != 1 || selectedWeapon == 2)
        {
            Debug.Log("Pistol not selected");

            anim.SetTrigger("Release"); 
            anim.SetBool("PistolOn", false);

            pistolEquipped = false; //Setting the boolean variable, pistolEquipped to false, to tell the player that the pistol is not equipped
            pickUp.spaceLeft = true; //Setting the boolean variable, spaceLeft to true, to tell the player that he can pick up or equip a weapon
        }
        
        if (selectedWeapon == 2)
        {
            Debug.Log("Rifle selected");

            //Setting and controlling the animations on the player to grab the rifle weapon
            anim.SetTrigger("Grabbing");
            anim.SetBool("RifleOn", true);

            rifleEquipped = true; //Setting the boolean variable to true, to tell the player that the rifle weapon is equipped
        }
        else if(selectedWeapon != 2 || selectedWeapon == 1)
        {
            Debug.Log("Rifle not selected");

            anim.SetTrigger("Release");
            anim.SetBool("RifleOn", false);

            rifleEquipped = false; //Setting the boolean variable, rifleEquipped to false, to tell the player that the rifle weapon is not equipped
            pickUp.spaceLeft = true; //Setting the boolean variable, spaceLeft to true, to tell the player that he can pick up or equip a weapon
        }
    }

    //When the RifleGrab animation plays, this animation event will take place and the following will happen.
    public void RifleGrabEvent()
    {
        foreach(Transform child in rifleOff.transform) //The foreach statement to look for the object inside the rifleOff parent.
        {
            Debug.Log("Equipping");

            if (child.tag == "rifle") //This tells the script to look for the tag on the object, if it is Rifle, proceed.
            {
                child.SetParent(rifleParent); //This will set the parent of the object to the Rifle parent, on the player object.

                child.transform.localPosition = Vector3.zero; //This will set the object's local position to 0.
                child.transform.localRotation = Quaternion.Euler(Vector3.zero); //This will set the object's local rotation to 0.
                child.transform.localScale = Vector3.one; //This will set the object's local scale to 1.

                child.GetComponent<Rigidbody>().isKinematic = true; //Setting the isKinematic variable in the Rigidbody component to true
                child.GetComponent<Collider>().isTrigger = true; //Setting the isTrigger variable in the Collider component to true
                pickUp.canShoot = true;
            }
        }
    }

    public void RifleReleaseEvent()
    {
        foreach (Transform child in rifleParent.transform) //The foreach statement to look for the object inside the rifleParent parent.
        {
            Debug.Log("Changing");

            if (child.tag == "rifle") //This tells the script to look for the tag on the object, if it is Rifle, proceed.
            {
                child.SetParent(rifleOff); //This will set the parent of the object to the rifleOff parent, on the player object.

                child.transform.localPosition = Vector3.zero; //This will set the object's local position to 0.
                child.transform.localRotation = Quaternion.Euler(Vector3.zero); //This will set the object's local rotation to 0.
                child.transform.localScale = Vector3.one; //This will set the object's local scale to 1.

                child.GetComponent<Rigidbody>().isKinematic = true; //Setting the isKinematic variable in the Rigidbody component to true.
                child.GetComponent<Collider>().isTrigger = true; //Setting the isTrigger variable in the Collider component to true.
                pickUp.canShoot = false;
            }
        }
    }

    public void PistolGrabEvent()
    {
        foreach (Transform child in pistolOff.transform) //The foreach statement to look for the object inside the pistolOff parent.
        {
            Debug.Log("Equipping");

            if (child.tag == "pistol") //This tells the script to look for the tag on the object, if it is Pistol, proceed.
            {
                child.SetParent(pistolParent); //This will set the parent of the object to the Pistol parent, on the player object.

                child.transform.localPosition = Vector3.zero; //This will set the object's local position to 0.
                child.transform.localRotation = Quaternion.Euler(Vector3.zero); //This will set the object's local rotation to 0.
                child.transform.localScale = Vector3.one; //This will set the object's local scale to 1.

                child.GetComponent<Rigidbody>().isKinematic = true; //Setting the isKinematic variable in the Rigidbody component to true.
                child.GetComponent<Collider>().isTrigger = true; //Setting the isTrigger variable in the Collider component to true.
                pickUp.canShoot = true;
            }
        }
    }

    public void PistolReleaseEvent()
    {
        foreach (Transform child in pistolParent.transform) //The foreach statement to look for the object inside the pistolParent obejct
        {
            Debug.Log("Changing");

            if (child.tag == "pistol") //This tells the script to look for the tag on the object, if it is Pistol, proceed.
            {
                child.SetParent(pistolOff); //This will set the parent of the object to the pistolOff parent, on the player object.

                child.transform.localPosition = Vector3.zero; //This will set the object's local position to 0.
                child.transform.localRotation = Quaternion.Euler(Vector3.zero); //This will set the object's local rotation to 0.
                child.transform.localScale = Vector3.one; //This will set the object's local scale to 1.

                child.GetComponent<Rigidbody>().isKinematic = true; //Setting the isKinematic variable in the Rigidbody component to true.
                child.GetComponent<Collider>().isTrigger = true; //Setting the isTrigger variable in the Collider component to true.
                pickUp.canShoot = false;
            }
        }
    }
}
