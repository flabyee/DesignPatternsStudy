using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flyweight
{
    public class FlyweightController : MonoBehaviour
    {
        List<Heavy> heavyList = new List<Heavy>();
        List<Flyweight> flyweightList = new List<Flyweight>();

        private void Start()
        {
            int numberOfObjs = 100000;

            // 데이터를 공유하지 않는 헤비객체를 생성해본다
            //for (int i = 0; i < numberOfObjs; i++)
            //{
            //    Heavy newHeavy = new Heavy();

            //    heavyList.Add(newHeavy);
            //}

            // 플라이웨잇 패턴 적용
            DummyData dData = new DummyData();
            for (int i = 0; i < numberOfObjs; i++)
            {
                Flyweight newFlyweight = new Flyweight(dData);

                flyweightList.Add(newFlyweight);
            }
        }
    }
}

