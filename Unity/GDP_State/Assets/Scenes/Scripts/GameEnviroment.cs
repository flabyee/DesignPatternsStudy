using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameEnviroment
{
    private static GameEnviroment _instance;

    public List<GameObject> CheckpointList = new List<GameObject>();
    public GameObject runaway;

    public static GameEnviroment Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameEnviroment();
                _instance.CheckpointList.AddRange(GameObject.FindGameObjectsWithTag("Checkpoint"));

                // ��������Ʈ�� �̸������� ����
                _instance.CheckpointList = _instance.CheckpointList.OrderBy(waypoint => waypoint.name).ToList();

                Instance.runaway = GameObject.Find("Runaway");
            }

            return _instance;
        }
    }
}
