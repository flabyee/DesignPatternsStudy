using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    // ������Ʈ �ӽ��� �����(�ʿ���) ���ֿ��� ������ų���̴�

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
