using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{
    public abstract class Command
    {
        public abstract void Excute();

        public abstract void Undo();
    }
}