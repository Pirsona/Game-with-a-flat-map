using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float Speed => _config.Speed;
    public float RotationSpeed => _config.RotationSpeed;
    public bool IsOccupied { get; private set; } = false;


    [SerializeField] private UnitConfig _config;
    [SerializeField] private UnitMover _mover;
    [SerializeField] private UnitRotator _rotator;
    [SerializeField] private ObjectCapture _objectCapture;
    [SerializeField] private ObjectGiver _objectGiver;


    private Transform _currentTarget;
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    

    private void Awake()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        
    }


    public void MoveToOre(Ore ore)
    {
        Debug.Log("Go To Ore");
        IsOccupied = true;
        _mover.MoveToTarget(ore.transform.position, Speed);
        _rotator.RotateToTarget(ore.transform.rotation, RotationSpeed);
    }

    public void MoveToBase()
    {
        _mover.MoveToTarget(_startPosition, Speed);
        _rotator.RotateToTarget(_startRotation, RotationSpeed);
        IsOccupied = false;
    }
}
