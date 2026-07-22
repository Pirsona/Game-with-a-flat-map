using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerMask;
    
    private Transform _objectTransform;
    
    private void Update()
    {
        if (_objectTransform != null)
        {
           if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit,Mathf.Infinity, _layerMask))
           {
               float currentY = _objectTransform.position.y;
               
               _objectTransform.position =  new  Vector3(hit.point.x, currentY,hit.point.z);
           }
        } 
    }
    
    public void GetObject(Transform objectTransform)
    {
        _objectTransform = objectTransform;
    }

    public void RemoveObject()
    {
        _objectTransform = null;
    }
    
}