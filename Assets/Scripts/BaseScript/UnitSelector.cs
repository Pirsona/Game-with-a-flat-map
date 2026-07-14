using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelector : MonoBehaviour
{
    [SerializeField] private List<Unit> _units;
    
    private List<Unit> _freeUnits;
    private List<Unit> _occupiedUnits = new List<Unit>();

    private void Awake()
    {
        _freeUnits = new List<Unit>(_units);
    }

    public Unit GetFreeUnit()
    {
        if(_freeUnits.Count > 0)
        {
            Unit unit = _freeUnits[0];
            _occupiedUnits.Add(unit);
            _freeUnits.RemoveAt(0);
            
            return unit;
        }
        return null;
    }
    
    public void ReleaseUnit(Unit unit)
    {
        if(_occupiedUnits.Contains(unit))
        {
            _occupiedUnits.Remove(unit);
            _freeUnits.Add(unit);
        }
    }
}
