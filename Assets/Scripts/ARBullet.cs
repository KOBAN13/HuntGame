using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ARBullet : Bullets
{
    public void Start()
    {
        Destroy(gameObject, 3f);
    }
}
