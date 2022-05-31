using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern.RebindKeys
{
    public class MoveObject : MonoBehaviour
    {
        // 오브젝트가 키입력을 받아서 한번에 이동 가능한 수치

        private const float MOVE_STEP_DISTANCE = 1f;

        public void MoveForward()
        {
            Move(Vector3.up);
        }
        public void MoveBack()
        {
            Move(Vector3.down);
        }
        public void TurnLeft()
        {
            Move(Vector3.left);
        }
        public void TurnRight()
        {
            Move(Vector3.right);
        }

        void Move(Vector3 dir)
        {
            transform.Translate(dir * MOVE_STEP_DISTANCE);
        }
    }
}


