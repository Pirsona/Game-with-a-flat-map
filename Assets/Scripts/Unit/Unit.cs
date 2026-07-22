using System;
using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitConfig _config;
    [SerializeField] private UnitMover _mover;
    [SerializeField] private UnitRotator _rotator;
    [SerializeField] private ObjectInteracting _objectInteracting;


    private Vector3 _startPosition;
    private CollectionPoint _collectionPoint;
    private Coroutine _activeJob;

    public event Action<Unit> BaseBuildComplete;
    
    public float Speed => _config.Speed;
    public float RotationSpeed => _config.RotationSpeed;
    public bool IsBusy { get; private set; } = false;
    
    private void Start()
    {
        _startPosition = transform.position;
    }
    
    public void MoveToOre(Ore ore, CollectionPoint collectionPoint)
    {
        if (_activeJob != null)
        {
            StopCoroutine(_activeJob);
        }
    
        if(_collectionPoint == null)
        {
            _collectionPoint = collectionPoint;
        }
        
        IsBusy = true; 
        _activeJob = StartCoroutine(Collect(ore, collectionPoint));
    }

    public void MoveToBase(Vector3 basePosition)
    {
        if (_activeJob != null)
        {
            StopCoroutine(_activeJob);
        }

        IsBusy = true;
        _activeJob = StartCoroutine(BuildBase(basePosition));
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

        IsBusy = false;
        
        _collectionPoint.FinishTask(this, ore);
    }

    private IEnumerator BuildBase(Vector3 targetPosition)
    {
        _startPosition = targetPosition;
        _collectionPoint = null;
        
        yield return _rotator.RotateCoroutine(targetPosition, RotationSpeed);
        yield return _mover.MoveCoroutine(targetPosition, Speed);
        
        BaseBuildComplete?.Invoke(this);
        IsBusy = false;
    }
}