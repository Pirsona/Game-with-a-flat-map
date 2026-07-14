using System.Collections.Generic;
using UnityEngine;

public class OreDataBase : MonoBehaviour
{
    private List<Ore> _findingOre = new List<Ore>();
    private List<Ore> _bookedOre = new List<Ore>();
    
    public Ore GetOreForUnit()
    {
        if (_findingOre.Count > 0)
        {
            Ore ore = _findingOre[0];
            _bookedOre.Add(ore);
            _findingOre.RemoveAt(0);
            return ore;
        }
        else
        {
            return null;
        }
    }

    public void CheckIsNewOre(Ore ore)
    {
        if(!_findingOre.Contains(ore) && !_bookedOre.Contains(ore))
        {
            _findingOre.Add(ore);
        }
    }
    
    
    public void RemoveFromBook(Ore ore)
    {
        _bookedOre.Remove(ore);
    }
}