using System.Collections.Generic;
using UnityEngine;

public class UnitSelector : MonoBehaviour
{
    [SerializeField] private List<Unit> _startUnits;
    
    private List<Unit> _freeUnits;
    private List<Unit> _occupiedUnits = new List<Unit>();
    
    public int TotalUnits => _freeUnits.Count + _occupiedUnits.Count;

    private void Awake()
    {
        _freeUnits = new List<Unit>(_startUnits);
    }

    public void AddNewUnit(Unit unit)
    {
        _freeUnits.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        _freeUnits.Remove(unit);
        _occupiedUnits.Remove(unit);
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
