using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public abstract class Monster
    {
        // ��� ������ �θ� Ŭ����
        // ������ ǥ�� -> Ŭ��
        // ���ʹ� ���� �� �� �ȴ�

        public abstract Monster Clone();

        public abstract void Talk();

        
        // �̵�
        // ����
    }
}

