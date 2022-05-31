using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern.RebindKeys
{
    public class MoveBackCommand : Command
    {
        private MoveObject moveObject;

        // »ý¼ºÀÚ
        public MoveBackCommand(MoveObject moveObject)
        {
            this.moveObject = moveObject;
        }

        public override void Excute()
        {
            moveObject.MoveBack();
        }

        public override void Undo()
        {
            moveObject.MoveForward();
        }
    }
}
