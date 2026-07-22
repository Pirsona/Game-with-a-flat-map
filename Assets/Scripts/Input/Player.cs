using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputSystem _input;
    [SerializeField] private MoveObject _moveObject;
    
    private Base _selectedBase;

    private void OnEnable()
    {
        _input.BaseClicked += SelectBase;
        _input.GroundClicked += PlaceFlag;
    }

    private void OnDisable()
    {
        _input.BaseClicked -= SelectBase;
        _input.GroundClicked -= PlaceFlag;
    }

    private void SelectBase(Base clickedBase)
    {
        if (_selectedBase != null)
        {
            _selectedBase.SetStandardColor();
        }

        if (!clickedBase.ReadyToBuild())
        {
            return;
        }
        
        _selectedBase = clickedBase;
        
        _selectedBase.SetSelectingColor();
        
        _moveObject.GetObject(_selectedBase.GetFlagPosition());
    }

    private void PlaceFlag(Vector3 position)
    {
        if (_selectedBase != null)
        {
            _moveObject.RemoveObject();
            _selectedBase.SetFlagPosition(position);
            _selectedBase.SetStandardColor();
            _selectedBase = null;
        }
    }
}