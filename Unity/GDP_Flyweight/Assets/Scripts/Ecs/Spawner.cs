using Unity.Entities;
public struct Spawner : IComponentData
{
    public Entity Prefab;
    public int Width;
    public int Depth;
}
