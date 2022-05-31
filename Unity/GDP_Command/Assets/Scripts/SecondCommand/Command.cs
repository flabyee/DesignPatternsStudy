using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    public abstract void Execute(Animator anim, bool forward);
}

public class PerformJump : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        anim.SetTrigger(string.Format("isJumping{0}", forward ? "" : "R"));
    }
}

public class PerformPunch : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        string tempStr = forward ? "isPunching" : "isPunchingR";
        anim.SetTrigger(tempStr);
    }
}

public class PerformKick : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        string tempStr = forward ? "isKicking" : "isKickingR";
        anim.SetTrigger(tempStr);
    }
}

public class DoNothing : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        
    }
}

public class MoveForward : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        string tempStr = forward ? "isWalking" : "isWalkingR";
        anim.SetTrigger(tempStr);
    }
}