using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    WeaponManager weaponManager; //The variable for the WeaponManager script to access it's public variables and methods.
    Animator anim; //The Animator component variable.

    float vertical;
    float horizontal;
    
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); //Geting the Animator Component from the player model inside the Inspector
        weaponManager = GetComponent<WeaponManager>(); //Geting the WeaponManager Component from the player model inside the Inspector
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        anim.SetFloat("Vertical", vertical);
        anim.SetFloat("Horizontal", horizontal);
        //anim.SetFloat("IsAiming", mouseY);

        //This is the condition for walking, when you press the Z key.
        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetBool("IsWalking", true);
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            anim.SetBool("IsWalking", false);
        }

        //This is the condition for Sprinting, when you press the Space key.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("IsSprinting", true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("IsSprinting", false);
        }

        //This is the condition for crouch - walking, when you press the C key.
        if (Input.GetKeyDown(KeyCode.C))
        {
            anim.SetBool("IsCrouching", true);
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            anim.SetBool("IsCrouching", false);
        }

        WeaponMovement(); //Calling the WeaponMovement() function.
    }

    public void WeaponMovement()
    {
        //This is the condition for walking while holding the Pistol, when you press the Z key.
        if (Input.GetKeyDown(KeyCode.Z) && weaponManager.pistolEquipped == true)
        {
            anim.SetBool("PistolWalking", true);
        }
        else if (Input.GetKeyUp(KeyCode.Z) && weaponManager.pistolEquipped == true)
        {
            anim.SetBool("PistolWalking", false);
        }

        //This is the condition for walking while holding the Rifle, when you press the Z key.
        if (Input.GetKeyDown(KeyCode.Z) && weaponManager.rifleEquipped == true)
        {
            anim.SetBool("RifleWalking", true);
        }
        else if (Input.GetKeyUp(KeyCode.Z) && weaponManager.rifleEquipped == true)
        {
            anim.SetBool("RifleWalking", false);
        }

        //This is the condition for crouch - walking while holding the Rifle, when you press the C key.
        if (Input.GetKeyDown(KeyCode.C) && weaponManager.rifleEquipped == true)
        {
            anim.SetBool("RifleCrouch", true);
        }
        else if (Input.GetKeyUp(KeyCode.C) && weaponManager.rifleEquipped == true)
        {
            anim.SetBool("RifleCrouch", false);
        }

        //This is the condition for crouch - walking while holding the Pistol, when you press the C key.
        if (Input.GetKeyDown(KeyCode.C) && weaponManager.pistolEquipped == true)
        {
            anim.SetBool("PistolCrouch", true);
        }
        else if (Input.GetKeyUp(KeyCode.C) && weaponManager.pistolEquipped == true)
        {
            anim.SetBool("PistolCrouch", false);
        }
    }
}
