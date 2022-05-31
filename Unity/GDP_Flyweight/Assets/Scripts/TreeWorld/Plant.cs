using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    [SerializeField]
    private PlantDataSO plantInfo;

    private SetPlantInfo spi;

    private void Start()
    {
        plantInfo.SetRandomName();
        plantInfo.SetRandomThreat();

        spi = GameObject.FindWithTag("PlantInfo").GetComponent<SetPlantInfo>();
    }

    public bool IsDangerous()
    {
        if(plantInfo.Threat >= PlantDataSO.eThreat.Moderate)
        {
            return true;
        }

        return false;
    }

    private void OnMouseDown()
    {
        spi.OpenPlantPanel();
        spi.planeName.text = plantInfo.Name;
        spi.phreatLevel.text = plantInfo.Threat.ToString();
        spi.plantIcon.GetComponent<RawImage>().texture = plantInfo.Icon;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && plantInfo.Threat >= PlantDataSO.eThreat.Moderate)
        {
            PlayerController.dead = true;
        }
    }
}
