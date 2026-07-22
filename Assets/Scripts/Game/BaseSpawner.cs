using UnityEngine;

public class BaseSpawner : Spawner<Base>
{
    private const float BaseSpawnHeightOffset = 1f;
    
    public Base Spawn(Vector3 buildPosition, Unit unitBuild, OreDataBase oreDataBase)
    {
        Base createdBase = GetObject();
        
        createdBase.transform.position = new Vector3(buildPosition.x, BaseSpawnHeightOffset ,buildPosition.z);
        createdBase.SetDataBase(oreDataBase);
        createdBase.AddUnitInBase(unitBuild);
        
        
        return createdBase;
    }
}