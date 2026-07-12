using System;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Scanning _scanner;
    [SerializeField] private UnitSelector _unitSelector;
    [SerializeField] private UnitSpawner _unitSpawner;

    private void OnEnable()
    {
        _scanner.OreFound += OreFound;
    }

    private void OnDisable()
    {
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
}
 