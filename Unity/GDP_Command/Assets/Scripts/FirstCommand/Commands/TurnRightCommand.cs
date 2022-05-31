using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern.RebindKeys
{
    public class TurnRightCommand : Command
    {
        private MoveObject moveObject;

        // »ý¼ºÀÚ
        public TurnRightCommand(MoveObject moveObject)
        {
            this.moveObject = moveObject;
        }

        public override void Excute()
        {
            moveObject.TurnRight();
        }

        public override void Undo()
        {
            moveObject.TurnLeft();
        }
    }
}
