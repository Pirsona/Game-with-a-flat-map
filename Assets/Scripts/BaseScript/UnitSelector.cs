using System.Collections.Generic;
using UnityEngine;

public class UnitSelector : MonoBehaviour
{
    [SerializeField] private List<Unit> _units;
    
    public Unit GetFreeUnit()
    {
        foreach (Unit unit in _units)
        {
            if (!unit.IsOccupied)
            {
                return unit;
            }
        }

        return null;
    }
}
