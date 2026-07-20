using UnityEngine;

[CreateAssetMenu(fileName = "NewOreConfig", menuName = "Config/Ore",order = 51)]
public class OreConfig : ScriptableObject
{
    [SerializeField] private int _cost;

    public int Cost => _cost;
}
