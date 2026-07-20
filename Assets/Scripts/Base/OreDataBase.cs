using System.Collections.Generic;
using UnityEngine;

public class OreDataBase : MonoBehaviour
{
    private List<Ore> _freeOres = new List<Ore>();
    private List<Ore> _bookedOre = new List<Ore>();
    
    public Ore GetOreForUnit()
    {
        if (_freeOres.Count > 0)
        {
            Ore ore = _freeOres[0];
            _bookedOre.Add(ore);
            _freeOres.RemoveAt(0);
            return ore;
        }
        else
        {
            return null;
        }
    }

    public void AddFreeOre(Ore ore)
    {
        if(!_freeOres.Contains(ore) && !_bookedOre.Contains(ore))
        {
            _freeOres.Add(ore);
        }
    }
    
    
    public void RemoveFromBook(Ore ore)
    {
        _bookedOre.Remove(ore);
    }
}