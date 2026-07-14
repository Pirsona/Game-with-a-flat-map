using UnityEngine;

[CreateAssetMenu(fileName = "NewOreConfig", menuName = "Config/Ore",order = 51)]
public class OreConfig : ScriptableObject
{
    [SerializeField] private float _cost;

    public float Cost => _cost;
}
