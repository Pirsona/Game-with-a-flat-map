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
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    

    private void Awake()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        if(IsOccupied)
        {
            _mover.MoveToTarget(_targetPosition, Speed);
            _rotator.RotateToTarget(_targetPosition, RotationSpeed);

            if (Vector3Extensions.IsEnoughClose(transform.position, _targetPosition,CloseEnoughDistance) && _targetOre != null)
            {
                _objectCapture.PickUpOre(_targetOre);
                MoveToBase();
                _targetOre = null;
            }
            else if (Vector3Extensions.IsEnoughClose(transform.position, _startPosition,CloseEnoughDistance) && _targetOre == null)
            {
                _objectGiver.DropOre();
                IsOccupied = false;
            }
        }
    }


    public void MoveToOre(Ore ore)
    {
        Debug.Log("Go To Ore");
        IsOccupied = true; 
        _targetOre = ore;
        _targetPosition = ore.transform.position;
    }

    public void MoveToBase()
    {
        _targetPosition = _startPosition;
    }
}