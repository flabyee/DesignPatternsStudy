using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameObject playerObj;

    Animator anim;

    Command KeyQ, KeyW, KeyE, upArrow;

    List<Command> oldCommands = new List<Command>();

    Coroutine replayCoroutine;

    bool bStartReplay;  // 리플레이 스타트 신호용도
    bool bIsReplaying;  // 리플레이중인지?

    private void Start()
    {
        anim = playerObj.GetComponent<Animator>();

        KeyQ = new PerformJump();
        KeyW = new PerformKick();
        KeyE = new PerformPunch();
        upArrow = new MoveForward();

        Camera.main.GetComponent<CameraFollow360>().player = playerObj.transform;
    }

    private void Update()
    {
        if (!bIsReplaying)
            HandleInput();

        StartReplay();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            KeyQ.Execute(anim, true);
            oldCommands.Add(KeyQ);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            KeyW.Execute(anim, true);
            oldCommands.Add(KeyW);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            KeyE.Execute(anim, true);
            oldCommands.Add(KeyE);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            upArrow.Execute(anim, true  );
            oldCommands.Add(upArrow);
        }
        else if(Input.GetKeyDown(KeyCode.Z))
        {
            UndoLastCommand();
        }
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            bStartReplay = true;
        }
    }

    void UndoLastCommand()
    {
        if (oldCommands.Count > 0)
        {
            Command cmd = oldCommands[oldCommands.Count - 1];
            cmd.Execute(anim, false);
            oldCommands.RemoveAt(oldCommands.Count - 1);
        }
    }

    void StartReplay()
    {
        if(bStartReplay && oldCommands.Count > 0)
        {
            bStartReplay = false;
            if(replayCoroutine != null)
            {
                StopCoroutine(replayCoroutine);
            }

            replayCoroutine = StartCoroutine(ReplayCommands());
        }
    }

    IEnumerator ReplayCommands()
    {
        bIsReplaying = true;

        for(int i = 0; i < oldCommands.Count; i++)
        {
            oldCommands[i].Execute(anim, true);
            yield return new WaitForSeconds(1f);
        }

        bIsReplaying = false;
    }
}
