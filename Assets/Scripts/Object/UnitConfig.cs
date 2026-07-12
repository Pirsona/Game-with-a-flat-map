using UnityEngine;

[CreateAssetMenu(fileName = "NewUnitConfig", menuName = "Config/Unit",order = 51)]
public class UnitConfig : ScriptableObject
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    
    public float Speed => _speed;
    public float RotationSpeed => _rotationSpeed;
}