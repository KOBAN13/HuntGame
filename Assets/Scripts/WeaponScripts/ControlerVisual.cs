using UnityEngine;
using Weapon;
using Random = UnityEngine.Random;

public class ControlerVisual : MonoBehaviour
{
    [SerializeField] private Transform spawnMagazine;
    [SerializeField] private Transform spawnSleeve;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Animator AnimWeapon;
    protected ParticleSystem ParticleShoot; 
    [SerializeField] private WeaponsInfo weaponsInfo;
    public Transform ShootPoint => shootPoint;
    
    public void TriggerAnimation(string nameAnimation) => AnimWeapon.SetTrigger(nameAnimation);

    public void OnActivateEvent()
    {
        EventManager.SpawnSleeve += CreateNewSleepy;
        EventManager.WeaponAnimation += TriggerAnimation;
        EventManager.SpawnBullet += CreateNewBullets;
    }

    public void OnDisableEvent()
    {
        EventManager.SpawnSleeve -= CreateNewSleepy;
        EventManager.WeaponAnimation -= TriggerAnimation;
        EventManager.SpawnBullet -= CreateNewBullets;
    }
    
    public void DropMagazine() //ивент по выпаданию магазина из оружия
    {
        CreateNewCloneObject(weaponsInfo.PrefabMagazine, spawnMagazine, spawnMagazine.forward);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void CreateNewSleepy(int launchForce, int torsionForce)
    {
        Vector3 forseSleepy = spawnSleeve.right * 1f + spawnSleeve.up * Random.Range(0,1f);
        GameObject newSleeve = CreateNewCloneObject(weaponsInfo.PrefabSleeve, spawnSleeve,  forseSleepy * launchForce);
        newSleeve.GetComponent<Rigidbody>().AddTorque(Random.insideUnitSphere * torsionForce);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void CreateNewBullets(Vector3 newBulletDistanse, (float, float) spreadAngle)
    {
        Vector3 direction = 
            Quaternion.Euler(Random.Range(spreadAngle.Item1, spreadAngle.Item2),
                Random.Range(spreadAngle.Item1, spreadAngle.Item2),
                Random.Range(spreadAngle.Item1, spreadAngle.Item2)) * newBulletDistanse.normalized;
        CreateNewCloneObject(weaponsInfo.PrefabBullet, shootPoint, direction * weaponsInfo.BulletsInfo.AmmoSpeed, direction);
    }
        
    private GameObject CreateNewCloneObject(GameObject prefab, Transform transformObj, Vector3 forseClone, Vector3 direction = new Vector3())
    {
        GameObject newObject = Instantiate(prefab, transformObj.position, transformObj.rotation);
        if (direction != new Vector3())
            newObject.transform.forward = direction.normalized;   //эта строка устанавливает направелние обьекта новой пули в направлении newBulletDistance
        newObject.GetComponent<Rigidbody>().AddForce(forseClone, ForceMode.Impulse);
        return newObject;
    }
}
