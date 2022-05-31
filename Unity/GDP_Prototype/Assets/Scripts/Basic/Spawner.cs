using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class Spawner
    {
        public Monster prototype;

        public Spawner(Monster prototype)
        {
            this.prototype = prototype;
        }

        public Monster SpawnerMonster()
        {
            return prototype.Clone();
        }
    }
}

