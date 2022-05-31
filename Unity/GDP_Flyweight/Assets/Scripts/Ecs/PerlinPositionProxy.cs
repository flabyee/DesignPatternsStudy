using UnityEngine;
using System;
using Unity.Entities;
using Unity.Mathematics;

public class PerlinPositionProxy : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var data = new PerlinPosition { };
        dstManager.AddComponentData(entity, data);
    }
}
