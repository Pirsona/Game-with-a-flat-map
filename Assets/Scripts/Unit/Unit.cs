using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private const float CloseEnoughDistance = 1f;
    
    [SerializeField] private UnitConfig _config;
    [SerializeField] private UnitMover _mover;
    [SerializeField] private UnitRotator _rotator;
    [SerializeField] private ObjectInteracting _objectInteracting;


    private Vector3 _startPosition;
    private CollectionPoint _collectionPoint;
    
    public float Speed => _config.Speed;
    public float RotationSpeed => _config.RotationSpeed;
    public bool IsOccupied { get; private set; } = false;
    
    private void Start()
    {
        _startPosition = transform.position;
    }
    
    private IEnumerator Collect(Ore ore, CollectionPoint basePoint)
    {
        Vector3 targetPosition = ore.transform.position;
        
        yield return _rotator.RotateCoroutine(targetPosition, RotationSpeed);
        yield return _mover.MoveCoroutine(targetPosition, Speed);
      
        _objectInteracting.PickUpOre(ore);
        
        yield return _rotator.RotateCoroutine(_startPosition, RotationSpeed);
        yield return _mover.MoveCoroutine(_startPosition, Speed);
        
        _objectInteracting.DropOre(ore);

        IsOccupied = false;
        
        _collectionPoint.FinishTask(this, ore);
    }
    
    public void MoveToOre(Ore ore, CollectionPoint collectionPoint)
    {
        if(_collectionPoint == null)
        {
            _collectionPoint = collectionPoint;
        }
        
        IsOccupied = true; 
        
        StartCoroutine(Collect(ore, collectionPoint));
    }
}