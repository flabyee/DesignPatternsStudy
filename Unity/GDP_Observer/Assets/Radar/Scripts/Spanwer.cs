using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spanwer : MonoBehaviour
{
    public GameObject eggPrefab;
    public GameObject medicPrefab;

    public Terrain terrain;

    TerrainData terrainData;

    private void Start()
    {
        terrainData = terrain.terrainData;

        InvokeRepeating("CreateEgg", 1, 1);
        InvokeRepeating("CreateMedic", 1, 1);
    }

    void CreateEgg()
    {
        int x = (int)Random.Range(0, terrainData.size.x);
        int z = (int)Random.Range(0, terrainData.size.z);

        Vector3 pos = new Vector3(x, 0, z);
        pos.y = terrain.SampleHeight(pos) + 10;

        GameObject eggObj = Instantiate(eggPrefab, pos, Quaternion.identity);
        eggObj.transform.SetParent(this.transform);
    }

    void CreateMedic()
    {
        int x = (int)Random.Range(0, terrainData.size.x);
        int z = (int)Random.Range(0, terrainData.size.z);

        Vector3 pos = new Vector3(x, 0, z);
        pos.y = terrain.SampleHeight(pos) + 10;

        GameObject medicObj = Instantiate(medicPrefab, pos, Quaternion.identity);
        medicObj.transform.SetParent(this.transform);
    }
}
