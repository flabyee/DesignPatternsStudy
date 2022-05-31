using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProCubeSpanwer : MonoBehaviour
{
    private void Update()
    {
        if (Random.Range(0, 100) < 10)
        {
            ProcCube.CreateCube(this.transform.position);
        }
    }
}
