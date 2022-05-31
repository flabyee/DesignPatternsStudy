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

            // �����͸� �������� �ʴ� ���ü�� �����غ���
            //for (int i = 0; i < numberOfObjs; i++)
            //{
            //    Heavy newHeavy = new Heavy();

            //    heavyList.Add(newHeavy);
            //}

            // �ö��̿��� ���� ����
            DummyData dData = new DummyData();
            for (int i = 0; i < numberOfObjs; i++)
            {
                Flyweight newFlyweight = new Flyweight(dData);

                flyweightList.Add(newFlyweight);
            }
        }
    }
}

