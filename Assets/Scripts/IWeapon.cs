using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public interface IWeapon
    {
        public void Reload();
        public void Shoot();
    }
}
