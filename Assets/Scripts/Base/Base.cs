using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Base : MonoBehaviour
{
    [SerializeField] private BaseConfig _baseConfig;
    [SerializeField] private BaseSpawner _baseSpawner;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private UnitSelector _unitSelector;
    [SerializeField] private OreDataBase _oreData;
    [SerializeField] private CollectionPoint _collectionPoint;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private BaseColor _baseColor;
    [SerializeField] private Transform _flagPosition;
    
    private BaseState _baseState = BaseState.SpawningUnit;
    
    public event Action ValueOreChanged;

    public enum BaseState
    {
        SpawningUnit,
        BuildingBase,
        WaitingForBuild
    }
    
    public int OreCount { get; private set; }

    private void OnEnable()
    {
        _scanner.OreFound += SendCollectOre;
        _collectionPoint.UnitTaskFinished += UnitReturned;
    }

    private void OnDisable()
    {
        _collectionPoint.UnitTaskFinished -= UnitReturned;
        _scanner.OreFound -= SendCollectOre;
    }

    public void SetDataBase(OreDataBase dataBase)
    {
        _oreData = dataBase;
    }
    
    public void SetSelectingColor()
    {
       _baseColor.SetColor(_baseColor.SelectingColor);
    }

    public void SetStandardColor()
    {
        _baseColor.SetColor(_baseColor.StandardColor);
    }

    public void SetFlagPosition(Vector3 flagPosition)
    {
        flagPosition.y += _flagPosition.position.y;
        _flagPosition.position = flagPosition;
        _baseState = BaseState.BuildingBase;   
    }

    public void BaseBuilt(Unit unitBuilding)
    {
        unitBuilding.BaseBuildComplete -= BaseBuilt;
        _flagPosition.gameObject.SetActive(false);
        _baseState = BaseState.SpawningUnit;
        _baseSpawner.Spawn(_flagPosition.position, unitBuilding, _oreData);
    }

    public void AddUnitInBase(Unit addingUnit)
    {
        _unitSelector.AddNewUnit(addingUnit);
    }

    public bool ReadyToBuild()
    {
        if (_unitSelector.TotalUnits <= 1) 
        {
            return false;
        }


        if (_baseState != BaseState.SpawningUnit)
        {
            return false;    
        }

        return true;
    }

    public Transform GetFlagPosition()
    {
        _flagPosition.gameObject.SetActive(true);
        return _flagPosition.transform;
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
    
    private void UnitReturned(Unit unit, Ore ore)
    {
        
        _unitSelector.ReleaseUnit(unit);

        OreCount += ore.OreCost;
        _oreData.RemoveFromBook(ore);
        ValueOreChanged?.Invoke();
        
        TrySpendResources();
        
        SendUnit(); 
    }

    private void TrySpendResources()
    {
        if (OreCount >= _baseConfig.ValueCreateUnit && _baseState == BaseState.SpawningUnit)
        { 
            OreCount -= _baseConfig.ValueCreateUnit; 
            ValueOreChanged?.Invoke();
            
            Unit createdUnit =  _unitSpawner.SpawnUnit(); 
            _unitSelector.AddNewUnit(createdUnit);
        }
        else if (OreCount >= _baseConfig.ValueCreateBuilding && _baseState == BaseState.BuildingBase)
        {
            Unit freeUnit = _unitSelector.GetFreeUnit();

            if (freeUnit != null && _unitSelector.TotalUnits > 1)
            {
                OreCount -= _baseConfig.ValueCreateBuilding;
                ValueOreChanged?.Invoke();
                
                _unitSelector.RemoveUnit(freeUnit);
                
                freeUnit.BaseBuildComplete += BaseBuilt;
                freeUnit.MoveToBase(_flagPosition.position);
                
                _baseState = BaseState.WaitingForBuild;
            }
        }
    }
}