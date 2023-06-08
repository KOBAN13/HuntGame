using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    public float health = 50f; //The health varaible of the object that the script is on, initialized to 50f.

    public void TakeDamage(float amount) //The TakeDamage Function to calculate the gameObject's health after it has taken damage. 
    {
        health -= amount; //Deduct the health with the amount in the TakeDamage parameter.

        if(health <= 0) 
        {
            Destroy(gameObject); //If the health reaches 0, then Destroy the game object and remove it from the scene.
        }
    }
}
