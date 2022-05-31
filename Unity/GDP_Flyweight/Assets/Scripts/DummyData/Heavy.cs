using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flyweight
{
    public class Heavy
    {
        private float health;

        private DummyData dData;

        public Heavy()
        {
            health = Random.Range(10f, 100f);

            dData = new DummyData();
        }
    }
}