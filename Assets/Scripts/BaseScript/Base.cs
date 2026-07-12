using System;
using OreScript;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Scanning _scanner;
    [SerializeField] private UnitSelector _unitSelector;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private OreSpawner _oreSpawner;
    
    public float ValueOre { get; private set; }
    
    public event Action ValueOreChange;

    private void OnEnable()
    {
        _oreSpawner.ReturnedOre += OreCollected;
        _scanner.OreFound += OreFound;
    }

    private void OnDisable()
    {
        _oreSpawner.ReturnedOre -= OreCollected;
        _scanner.OreFound -= OreFound;
    }

    private void OreFound(Ore ore)
    {
        Debug.Log("Searching Unit");
        Unit freeUnit = _unitSelector.GetFreeUnit();
        
        
        if (freeUnit != null)
        {
            Debug.Log("Find Unit " + freeUnit.name);
            ore.BookOre();
            freeUnit.MoveToOre(ore);
        }
    }
    
    private void OreCollected(float value)
    {
        ValueOre += value;
        ValueOreChange?.Invoke();
    }
}