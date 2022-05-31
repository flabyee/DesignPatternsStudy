using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public abstract class Monster
    {
        // 모든 몬스터의 부모 클래스
        // 몬스터의 표준 -> 클론
        // 몬스터는 말을 할 줄 안다

        public abstract Monster Clone();

        public abstract void Talk();

        
        // 이동
        // 공격
    }
}

