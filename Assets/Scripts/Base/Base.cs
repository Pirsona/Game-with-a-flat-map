using System;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private BaseConfig _baseConfig;
    [SerializeField] private Scaner _scanner;
    [SerializeField] private UnitSelector _unitSelector;
    [SerializeField] private OreDataBase _oreData;
    [SerializeField] private CollectionPoint _collectionPoint;
    [SerializeField] private UnitSpawner _unitSpawner;
    
    
    private BaseState _baseState = BaseState.SpawningUnit;
    
    public event Action ValueOreChanged;

    public enum BaseState
    {
        SpawningUnit,
        BuildingBase
    }
    
    public int ValueOre { get; private set; }

    private void OnEnable()
    {
        _scanner.OreFound += SendCollectOre;
        _collectionPoint.UnitTaskFinished += BackUnit;
    }

    private void OnDisable()
    {
        _collectionPoint.UnitTaskFinished -= BackUnit;
        _scanner.OreFound -= SendCollectOre;
    }

    private void SendCollectOre(Ore ore)
    {
        _oreData.AddFreeOre(ore);
        
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
                freeUnit.MoveToOre(ore, _collectionPoint); 
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
        
        _unitSelector.ReleaseUnit(unit);

        ValueOre += ore.OreCost;
        _oreData.RemoveFromBook(ore);
        ValueOreChanged?.Invoke();
        
        CheckValueOre();
        
        SendUnit(); 
    }

    private void CheckValueOre()
    {
        if (ValueOre >= _baseConfig.ValueCreateUnit && _baseState == BaseState.SpawningUnit)
        { 
            ValueOre -= _baseConfig.ValueCreateUnit; 
            ValueOreChanged?.Invoke();
            
            Unit createdUnit =  _unitSpawner.SpawnUnit(); 
            _unitSelector.AddNewUnit(createdUnit);
        }
        else if (ValueOre >= _baseConfig.ValueCreateBuilding && _baseState == BaseState.BuildingBase)
        {
            
        }
    }
}