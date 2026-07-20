using UnityEngine;

[CreateAssetMenu(fileName = "NewBaseConfig", menuName = "Config/Base",order = 51)]
public class BaseConfig : ScriptableObject
{
    [SerializeField] private int _valueCreateUnit;
    [SerializeField] private int _valueCreateBuilding;
    
    public int ValueCreateUnit => _valueCreateUnit;
    public int ValueCreateBuilding => _valueCreateBuilding;
}