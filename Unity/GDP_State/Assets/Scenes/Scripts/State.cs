using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public partial class State
{
    public enum eState  // 가질 수 있는 상태 나열
    {
        IDLE, PATROL, PURSUE, ATTACK, DEAD, RUNAWAY
    };

    public enum eEvent  // 이벤트 나열
    {
        ENTER, UPDATE, EXIT
    };

    public eState stateName;

    protected eEvent curEvent;

    protected GameObject myObj;
    protected NavMeshAgent myAgent;
    protected Animator myAnim;
    protected Transform playerTrm;  // 타겟팅 할 플레이어의 트랜스폼

    protected State nextState;  // 다음 상태를 나타냄

    float detectDist = 10.0f;
    float detectAngle = 30.0f;
    float shootDist = 7.0f;

    public State(GameObject obj, NavMeshAgent agent, Animator anim, Transform targetTrm)
    {
        myObj = obj;
        myAgent = agent;
        anim = myAnim;
        playerTrm = targetTrm;

        // 최초 이벤트를 엔터로
        curEvent = eEvent.ENTER;
    }

    public virtual void Enter() { curEvent = eEvent.UPDATE; }
    public virtual void Update() { curEvent = eEvent.UPDATE; }
    public virtual void Exit() { curEvent = eEvent.EXIT; }

    public State Process()
    {
        if (curEvent == eEvent.ENTER) Enter();
        if (curEvent == eEvent.UPDATE) Update();
        if (curEvent == eEvent.EXIT)
        {
            Exit();
            return nextState;
        }

        return this;
    }


    // 추적 로직
    public bool CanSeePlayer()
    {
        Vector3 dir = playerTrm.position - myObj.transform.position;
        float angle = Vector3.Angle(dir, myObj.transform.forward);

        if(dir.magnitude < detectDist && angle < detectAngle)
        {
            return true;
        }

        return false;
    }

    // 공격 범위 체크 로직
    public bool CanAttackPlayer()
    {
        Vector3 dir = playerTrm.position - myObj.transform.position;
        if(dir.magnitude < shootDist)
        {
            return true;
        }

        return false;
    }

    public bool isBackPlayer()
    {
        Vector3 dir = playerTrm.position - myObj.transform.position;
        float angle = Vector3.Angle(dir, myObj.transform.forward * -1); // or myObj.transform.position - playerTrm.position

        if (dir.magnitude < detectDist && angle < detectAngle)
        {
            return true;
        }

        return false;
    }
}

public class Idle : State
{
    public Idle(GameObject obj, NavMeshAgent agent, Animator anim, Transform targetTrm)
        : base(obj, agent, anim, targetTrm)
    {
        stateName = eState.IDLE;
    }

    public override void Enter()
    {
        myAgent.speed = 0;
        myAgent.isStopped = true;

        //myAnim.SetTrigger("isIdle");
        base.Enter();
    }

    public override void Update()
    {
        if (CanSeePlayer())
        {
            nextState = new Pursue(myObj, myAgent, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
        else if (isBackPlayer())
        {
            nextState = new RunAway(myObj, myAgent, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
        else if (Random.Range(0, 5000) < 10)
        {
            nextState = new Patrol(myObj, myAgent, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
    }

    public override void Exit()
    {
        //myAnim.ResetTrigger("isIdle");
        base.Exit();
    }
}

public class Patrol : State
{
    int curIndex = -1;

    public Patrol(GameObject obj, NavMeshAgent agent, Animator anim, Transform targetTrm)
        : base(obj, agent, anim, targetTrm)
    {
        stateName = eState.PATROL;
        myAgent.speed = 2;
        myAgent.isStopped = false;
    }

    public override void Enter()
    {
        curIndex = 0;   // 초기 인덱스 세팅

        //myAnim.SetTrigger("isWalking");

        // 가장 가까운 웨이포인트로 가는 로직
        float lastDist = Mathf.Infinity;

        for(int i = 0; i < GameEnviroment.Instance.CheckpointList.Count - 1; i++)
        {
            GameObject thisWP = GameEnviroment.Instance.CheckpointList[i];
            float distance = Vector3.Distance(myObj.transform.position, 
                thisWP.transform.position);
            if(lastDist > distance)
            {
                lastDist = distance;
            }
        }

        base.Enter();
    }

    public override void Update()
    {
        // 체크포인트 순회 로직
        if(myAgent.remainingDistance < 1)
        {
            if(curIndex >= GameEnviroment.Instance.CheckpointList.Count - 1)
            {
                curIndex = 0;
            }
            else
            {
                curIndex++;
            }

            myAgent.SetDestination(GameEnviroment.Instance.CheckpointList[curIndex].transform.position);
        }

        if (CanSeePlayer())
        {
            nextState = new Pursue(myObj, myAgent, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
        else if(isBackPlayer())
        {
            nextState = new RunAway(myObj, myAgent, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
        else if (Random.Range(0, 5000) < 10)
        {
            nextState = new Idle(myObj, myAgent, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
    }

    public override void Exit()
    {
        //myAnim.ResetTrigger("isWalking");
        base.Exit();
    }
}

public class Pursue : State
{
    public Pursue(GameObject obj, NavMeshAgent agent, Animator anim, Transform targetTrm)
        : base(obj, agent, anim, targetTrm)
    {
        stateName = eState.PURSUE;
        myAgent.speed = 5;
        myAgent.isStopped = false;
    }

    public override void Enter()
    {
        //myAnim.SetTrigger("isRunning");
        base.Enter();
    }

    public override void Update()
    {
        myAgent.SetDestination(playerTrm.position);

        if(myAgent.hasPath)
        {
            if(CanAttackPlayer())
            {
                nextState = new Attack(myObj, myAgent, myAnim, playerTrm);
                curEvent = eEvent.EXIT;
            }
            else if(!CanSeePlayer())
            {
                nextState = new Patrol(myObj, myAgent, myAnim, playerTrm);
                curEvent = eEvent.EXIT;
            }
        }
    }

    public override void Exit()
    {
        //myAnim.ResetTrigger("isWalking");
        base.Exit();
    }
}


public class Attack : State
{
    float rotationSpeed = 2.0f;

    
    public Attack(GameObject obj, NavMeshAgent agent, Animator anim, Transform targetTrm)
        : base(obj, agent, anim, targetTrm)
    {
        stateName = eState.ATTACK;
    }

    public override void Enter()
    {
        //myAnim.SetTrigger("isRunning");
        base.Enter();
    }

    public override void Update()
    {
        // 내 각도를 플레이어 방향으로 
        Vector3 dir = playerTrm.position - myObj.transform.position;
        float angle = Vector3.Angle(dir, myObj.transform.forward);
        dir.y = 0;  // 혹시 몰라서

        myObj.transform.rotation = Quaternion.Slerp(myObj.transform.rotation,
            Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);


        // 타겟팅된 유닛이 사라졌는지 체크
        if (!CanSeePlayer())
        {
            nextState = new Idle(myObj, myAgent, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
    }

    public override void Exit()
    {
        //myAnim.ResetTrigger("isWalking");
        base.Exit();
    }
}

public class RunAway : State
{
    public RunAway(GameObject obj, NavMeshAgent agent, Animator anim, Transform targetTrm)
        : base(obj, agent, anim, targetTrm)
    {
        stateName = eState.RUNAWAY;
    }

    public override void Enter()
    {
        myAgent.speed = 5;
        myAgent.isStopped = false;

        //myAnim.SetTrigger("isRunning");
        base.Enter();
    }

    public override void Update()
    {
        myAgent.SetDestination(GameEnviroment.Instance.runaway.transform.position);

        if(myAgent.remainingDistance < 1)
        {
            nextState = new Patrol(myObj, myAgent, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
    }

    public override void Exit()
    {
        //myAnim.ResetTrigger("isWalking");
        base.Exit();
    }
}