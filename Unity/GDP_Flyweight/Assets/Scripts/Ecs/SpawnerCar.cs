using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCar : MonoBehaviour
{
    public GameObject carPrefab;
    public GameObject mainCamObj;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 pos = new Vector3(10, 10, 10);
            GameObject carObj = Instantiate(carPrefab, pos, Quaternion.identity);

            mainCamObj.GetComponent<SmoothFollow>().target = carObj.transform;
        }
    }
}
