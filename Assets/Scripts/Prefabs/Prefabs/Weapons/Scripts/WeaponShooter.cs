using System.Collections;
using UnityEngine;

public class WeaponShooter : MonoBehaviour
{
    public float speed; //The speed at which the bullet gameObject will travel.  

    public int bulletPerMag; //The variable for how many bullets will be in the magazine after it has been reloaded.
    public int bulletsLeft; //The variable to show how many bullets the player have left in total.
    public int currentBullets; //The current bullets that the weapon has inside of the magazine.

    public Transform weaponHolder; //The weapon holder game object, used to determine the new parent of the current game object.
    public Transform shootPoint; //The shoot point game object is where the bullet game object will be spawned and shot from.
    public GameObject bulletPrefab; //The Bullet GameObject.
    public Camera tpsCam { get; set; } //The variable for the Camera component and calling the component from any game object.

    public bool canShootWeapon; //The variable to check if we can shoot the weapon.

    float fireRate = 0.1f;
    float reloadTime = 5f;
    float fireTimer;

    bool isReloading;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        tpsCam = FindObjectOfType<Camera>(); //
        anim = FindObjectOfType<Animator>();
        currentBullets = bulletPerMag;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.parent == weaponHolder) //This if-statement checks if the GameObject's parent is equal to the weapon holder,
            canShootWeapon = true; //if it is true, the weapon will be able to shoot or fire,
        else
            canShootWeapon = false; //else, if it is not true, the weapon will not be able to shoot or fire the weapon, as it is not in the player's hands.

        if(Input.GetButtonDown("Fire1") && canShootWeapon == true) //The if-statement if the user presses the Left Mouse Button. 
        {
            if (currentBullets > 0) //Checks if the weapon has bullets, if it is 0, then there are no bullets.
                FireWeapon(); //Calls the FireWeapon() function to shoot or fire the weapon.
            else if (bulletsLeft > 0 && !isReloading)
                StartCoroutine(DoReload()); 
        }

        if(Input.GetKeyDown(KeyCode.R)) //Getting the input from the user to reload the weapon.
        {
            if (currentBullets < bulletPerMag && bulletsLeft > 0 && !isReloading)
                StartCoroutine(DoReload());
        }

        if (fireTimer < fireRate)
            fireTimer += Time.deltaTime;
    }

    private void FireWeapon()
    {
        if (fireTimer < fireRate || currentBullets <= 0 || isReloading)
            return;

        Ray ray = tpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)); //Getting the Ray of the RayCast to point to the center of the screen.
        RaycastHit hit; //The hit variable, everything that the Ray hits, gets stored inside of the hit varaible.

        Vector3 targetPoint; //the TargetPoint variable for the Ray.
        if (Physics.Raycast(ray, out hit))
        {
            print("I'm looking at " + hit.transform.name); //this line prints out what the Ray is looking at. 
            targetPoint = hit.point; //Setting the targetPoint to be equal to hit.point.
        }
        else
        {
            print("I'm looking at nothing");
            targetPoint = ray.GetPoint(75); //If the Ray is not looking at something, then print out that it is looking at nothing.
        }

        Vector3 bulletDistance = targetPoint - shootPoint.position; //Getting the distance between the targetPoint and the shootpoint.

        Vector3 newBulletDirection = bulletDistance + new Vector3(0f, 0f, 0f); //Getting the new bullets direction.

        GameObject currentBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity); //Instantiates a new bullet at the shootpoint game object.
        currentBullet.transform.forward = newBulletDirection.normalized; //the direction for the new bullet to spawn at.

        currentBullet.GetComponent<Rigidbody>().AddForce(bulletDistance.normalized * speed, ForceMode.Impulse); //Getting the Rigidbody component from the bullet, and Adding force to it with AddForce method of the rigidbody.
        currentBullets--; //Deduct the bullets after firing or shooting the weapon.
        fireTimer = 0.0f;
    }

    public void Reload()
    {
        if (bulletsLeft <= 0)
            return;

        int BulletsToLoad = bulletPerMag - currentBullets;
        int BulletsToDeduct = (bulletsLeft >= BulletsToLoad) ? BulletsToLoad : bulletsLeft;

        bulletsLeft -= BulletsToDeduct;
        currentBullets += BulletsToDeduct;
    }

    private IEnumerator DoReload()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        isReloading = false;
        Reload();

        anim.SetTrigger("IsReloading");
    }
}
