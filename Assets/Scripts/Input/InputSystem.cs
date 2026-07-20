using System;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    private const int LeftMouseButton = 0;
    
    [SerializeField] private Camera _camera; 

    public event Action<Base>  BaseClicked;
    public event Action<Vector3> GroundClicked;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                
                if (hit.collider.TryGetComponent(out Base baseComponent))
                {
                    Debug.Log("Base clicked " + baseComponent);
                    BaseClicked?.Invoke(baseComponent);
                }
                else if  (hit.collider.TryGetComponent(out Ground groundComponent))
                {
                    Debug.Log("Ground clicked " + groundComponent);
                    GroundClicked?.Invoke(hit.point);
                }
            }
        }
    }
}
