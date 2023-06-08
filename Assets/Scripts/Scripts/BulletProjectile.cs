using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    ObjectHealth health; //The variable for the ObjectHealth script that is located on game objects the player is shooting at.

    public float damage = 10f; //the variable for how much damage is given to the ObjectHealth script.
    
    private void Start()
    {
        Destroy(gameObject, 2f); //Destroying the bullet game object after 2 seconds.
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy") //This if statement checks the tag of the game object that the BulletProjectile collided with, and does the following.
        {
            health = collision.transform.GetComponent<ObjectHealth>(); //Check if the game object has the ObjectHealth script attached to it.

            print("I hit the enemy" + collision.gameObject.name);

            health.TakeDamage(damage); //calling the TakeDamage() function from the ObjectHealth script to deduct the damage from the health.

            Destroy(gameObject); //Destroy the game object if object health is equals to 0.
        }
    }
}
