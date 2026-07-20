using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputSystem _input;
    [SerializeField] private SelectObject _selectObject;
    [SerializeField] private MoveObject _moveObject;
    [SerializeField] private ShowObject _showObject;
    
    private Base _selectingBase;

    private void OnEnable()
    {
        _input.BaseClicked += Select;
        _input.GroundClicked += Place;
    }

    private void OnDisable()
    {
        _input.BaseClicked -= Select;
        _input.GroundClicked -= Place;
    }

    private void Select(Base baseSeleted)
    {
        
    }
    
    private void Place(Vector3 position)
    {
        
    }
}