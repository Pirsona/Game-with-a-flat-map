using System;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Scaner _scanner;
    [SerializeField] private UnitSelector _unitSelector;
    [SerializeField] private OreDataBase _oreData;
    
    public event Action ValueOreChanged;
    
    public float ValueOre { get; private set; }

    private void OnEnable()
    {
        _scanner.OreFound += SendCollectOre;
    }

    private void OnDisable()
    {
        _scanner.OreFound -= SendCollectOre;
    }

    private void SendCollectOre(Ore ore)
    {
        _oreData.CheckIsNewOre(ore);
        
        SendUnit();
    }

    private void SendUnit()
    {
        while (true) 
        {
            Unit freeUnit = _unitSelector.GetFreeUnit();
            
            if (freeUnit == null) break; 

            Ore ore = _oreData.GetOreForUnit();
            
            if (ore != null)
            {
                freeUnit.BecameFree += BackUnit;
                freeUnit.MoveToOre(ore); 
            }
            else
            {
                _unitSelector.ReleaseUnit(freeUnit);
                break;
            }
        }
    }
    
    private void BackUnit(Unit unit, Ore ore)
    {
        unit.BecameFree -= BackUnit;
        _unitSelector.ReleaseUnit(unit); 
        
        ValueOre += ore.OreCost;
        _oreData.RemoveFromBook(ore);
        ValueOreChanged?.Invoke();
        
        SendUnit(); 
    }
}