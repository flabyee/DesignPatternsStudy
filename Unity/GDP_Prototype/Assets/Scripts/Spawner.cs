using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject cubePrefab;

    private void Update()
    {
        if(Random.Range(0, 100) < 10)
        {
            Instantiate(cubePrefab, this.transform.position, Quaternion.identity);
        }

    }
}