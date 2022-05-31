using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class Ghost : Monster
    {
        private int hp;
        private int speed;
        private static int ghostCounter = 0;

        public Ghost(int hp, int speed)
        {
            this.hp = hp;
            this.speed = speed;

            ghostCounter++;
        }

        public override Monster Clone()
        {
            return new Ghost(hp, speed);
        }

        public override void Talk()
        {
            Debug.Log(string.Format("안녕 나는 {0}유령인데, 체력은 {1}이고 " +
                "스피드는 {2}이야", ghostCounter, hp, speed));
        }
    }

}