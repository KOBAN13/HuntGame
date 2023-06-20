namespace Weapon
{
    public class ARBullet : Bullets
    {
        public void Start()
        {
            Destroy(gameObject, 10f);
        }
    }
}
