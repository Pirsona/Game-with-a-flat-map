using UnityEngine;

[CreateAssetMenu(fileName = "NewBaseConfig", menuName = "Config/Base",order = 51)]
public class BaseConfig : ScriptableObject
{
    [SerializeField] private float _scanRadius;
    [SerializeField] private float _scanCooldown;
    
    public float ScanRadius => _scanRadius;
    public float ScanCooldown => _scanCooldown;
}