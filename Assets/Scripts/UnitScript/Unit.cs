using UnityEngine;

public class Unit : MonoBehaviour
{
    private const float CloseEnoughDistance = 1f;
    
    public float Speed => _config.Speed;
    public float RotationSpeed => _config.RotationSpeed;
    public bool IsOccupied { get; private set; } = false;
    
    [SerializeField] private UnitConfig _config;
    [SerializeField] private UnitMover _mover;
    [SerializeField] private UnitRotator _rotator;
    [SerializeField] private ObjectCapture _objectCapture;
    [SerializeField] private ObjectGiver _objectGiver;

    private Vector3 _targetPosition;
    private Ore _targetOre;
    private Ore _pickedOre;
    private Vector3 _startPosition;

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

            if (Vector3Extensions.IsEnoughClose(transform.position, _targetPosition,CloseEnoughDistance) && _targetOre != null)
            {
                _pickedOre = _objectCapture.PickUpOre(_targetOre);
                MoveToBase();
                _targetOre = null;
            }
            else if (Vector3Extensions.IsEnoughClose(transform.position, _startPosition,CloseEnoughDistance) && _pickedOre != null)
            {
                _objectGiver.DropOre(_pickedOre);
                _pickedOre = null;
                IsOccupied = false;
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