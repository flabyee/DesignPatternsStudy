using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern.RebindKeys
{
    public class TurnLeftCommand : Command
    {
        private MoveObject moveObject;

        // ������
        public TurnLeftCommand(MoveObject moveObject)
        {
            this.moveObject = moveObject;
        }

        public override void Excute()
        {
            moveObject.TurnLeft();
        }

        public override void Undo()
        {
            moveObject.TurnRight();
        }
    }
}