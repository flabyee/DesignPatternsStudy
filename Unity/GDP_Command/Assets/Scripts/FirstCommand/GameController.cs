using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern.RebindKeys
{
    public class GameController : MonoBehaviour
    {
        public MoveObject moveObj;
        /*
            // �߰��غ� ��ɵ�
            Undo, Redo, ���÷��� ���
        */


        // Ŀ�ǵ�� �� Ű�� ����
        private Command buttonW;
        private Command buttonA;
        private Command buttonS;
        private Command buttonD;

        private Stack<Command> undoCommands = new Stack<Command>();
        private Stack<Command> redoCommands = new Stack<Command>();

        bool isReplaying = false;
        private Vector3 startPos;
        private const float REPLAY_PAUSE_TIMER = 0.5f;

        private void Start()
        {
            // Ű�� ���ε�
            buttonW = new MoveForwardCommand(moveObj);
            buttonA = new TurnLeftCommand(moveObj);
            buttonS = new MoveBackCommand(moveObj);
            buttonD = new TurnRightCommand(moveObj);

            // ��ŸƮ ������ �������
            startPos = moveObj.transform.position;
        }

        private void Update()
        {
            if (isReplaying) return;

            // �����¿� Ű �Է� ó��
            if(Input.GetKeyDown(KeyCode.W))
            {
                ExecuteNewCommand(buttonW);
                undoCommands.Push(buttonW);
                redoCommands.Clear();
            }
            else if(Input.GetKeyDown(KeyCode.A))
            {
                ExecuteNewCommand(buttonA);
                undoCommands.Push(buttonA);
                redoCommands.Clear();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                ExecuteNewCommand(buttonS);
                undoCommands.Push(buttonS);
                redoCommands.Clear();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                ExecuteNewCommand(buttonD);
                undoCommands.Push(buttonD);
                redoCommands.Clear();
            }

            // 1. Ű���� ��� : �츮�� ���ϴ� Ű 2���� �ٲ㺻��
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if (isReplaying) return;

                print("swap");

                SwapKey(ref buttonW, ref buttonS);
            }

            // 2. Undo ���
            if (Input.GetKeyDown(KeyCode.U))
            {
                if (isReplaying) return;

                print("undo");
                if (undoCommands.Count > 0)
                {
                    Command lastCommand = undoCommands.Pop();
                    lastCommand.Undo();
                    redoCommands.Push(lastCommand);
                }
            }

            // 3. Redo ���
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (isReplaying) return;

                print("redo");
                if (redoCommands.Count > 0)
                {
                    Command nextCommand = redoCommands.Pop();
                    nextCommand.Excute();
                    undoCommands.Push(nextCommand);
                }
            }

            // 4. ���÷��� ���
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (isReplaying) return;

                isReplaying = true;

                print("replay");
                StartCoroutine(Replay());
            }
        }

        void ExecuteNewCommand(Command commandButton)
        {
            commandButton.Excute();
        }

        void SwapKey(ref Command key1, ref Command key2)
        {
            Command temp = key1;
            key1 = key2;
            key2 = temp;
        }

        IEnumerator Replay()
        {

            moveObj.transform.position = startPos;

            yield return new WaitForSeconds(REPLAY_PAUSE_TIMER);

            Command[] oldCommands = undoCommands.ToArray();

            for(int i = oldCommands.Length - 1; i >= 0; i--)
            {
                Command curCommand = oldCommands[i];

                curCommand.Excute();

                yield return new WaitForSeconds(REPLAY_PAUSE_TIMER);
            }

            isReplaying = false;
        }
    }
}
