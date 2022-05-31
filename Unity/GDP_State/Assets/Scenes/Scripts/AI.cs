using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    // 스테이트 머신을 사용할(필요한) 유닛에게 부착시킬것이다

    NavMeshAgent myAgent;
    Animator myAnim;

    public Transform playerTrm;

    State curState;

    private void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        myAnim = GetComponent<Animator>();

        curState = new Idle(this.gameObject, myAgent, myAnim, playerTrm);
    }

    private void Update()
    {
        curState = this.curState.Process();
    }
}
