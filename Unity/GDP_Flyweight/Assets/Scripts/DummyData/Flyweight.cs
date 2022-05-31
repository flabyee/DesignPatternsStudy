using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flyweight
{
    public class Flyweight
    {
        private float health;

        private DummyData dData;

        public Flyweight(DummyData dData)
        {
            health = Random.Range(10f, 100f);

            this.dData = dData;
        }
    }
}
