using UnityEngine;

public class UnitSpawner : Spawner<Unit>
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _scanRadius;
    [SerializeField] private LayerMask _obstacleLayer;
    
    public Unit SpawnUnit()
    {
        Unit unit = GetObject();

        unit.transform.position = GetValidSpawnPosition();
        
        return unit;
    }
    
    private Vector3 GetValidSpawnPosition()
    {
        bool isPositionFound = false;
            
        Vector3 randomPosition = Vector3.zero;
        Vector3 center = _spawnPoint.position;
        Vector3 extents = _spawnPoint.localScale / 2f;
            
        while (isPositionFound == false)
        {
            float xPosition = UnityEngine.Random.Range(center.x - extents.x, center.x + extents.x);
            float zPosition = UnityEngine.Random.Range(center.z - extents.z, center.z + extents.z);
                
            randomPosition = new Vector3(xPosition, center.y, zPosition);
                
            bool hasObject = Physics.CheckSphere(randomPosition, _scanRadius, _obstacleLayer);
                
            if (hasObject == false)
            {
                isPositionFound = true;
            }
        }

        return randomPosition;
    }
    
}
