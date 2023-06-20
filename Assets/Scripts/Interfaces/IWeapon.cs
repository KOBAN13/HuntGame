using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IWeapon
 {
     public void Reload();
     public void Shoot();

     public void Update();

     public void Start();
}
