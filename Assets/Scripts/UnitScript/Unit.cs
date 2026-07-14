using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private const float CloseEnoughDistance = 1f;
    
    [SerializeField] private UnitConfig _config;
    [SerializeField] private UnitMover _mover;
    [SerializeField] private UnitRotator _rotator;
    [SerializeField] private ObjectInteracting _objectInteracting;

    private Vector3 _targetPosition;
    private Ore _targetOre;
    private Ore _pickedOre;
    private Vector3 _startPosition;

    public event Action<Unit, Ore> BecameFree;
    
    public float Speed => _config.Speed;
    public float RotationSpeed => _config.RotationSpeed;
    public bool IsOccupied { get; private set; } = false;
    
    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if(IsOccupied)
        {
            _mover.MoveToTarget(_targetPosition, Speed);
            _rotator.RotateToTarget(_targetPosition, RotationSpeed);

            if (Vector3Extensions.IsEnoughClose(transform.position, _targetPosition,CloseEnoughDistance))
            {
                if (_targetOre != null)
                {
                    _pickedOre = _objectInteracting.PickUpOre(_targetOre);
                    MoveToBase();
                    _targetOre = null;
                }
                
                else if(_pickedOre != null)
                {
                    _objectInteracting.DropOre(_pickedOre);
                    IsOccupied = false;
                    BecameFree?.Invoke(this, _pickedOre);   
                    _pickedOre = null;
                }
            }
        }
    }

    private void MoveToBase()
    {
        _targetPosition = _startPosition;
    }
    
    public void MoveToOre(Ore ore)
    {
        IsOccupied = true; 
        _targetOre = ore;
        _targetPosition = ore.transform.position;
    }
}