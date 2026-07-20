using UnityEngine;

public class ObjectInteracting : MonoBehaviour
{
    [SerializeField] private Transform _capturePoint;
    
    public Ore PickUpOre(Ore ore)
    {
        ore.transform.SetParent(_capturePoint);
        ore.transform.localPosition = Vector3.zero;
        ore.DisableCollider();
        
        return ore;
    }
    
    public void DropOre(Ore ore)
    {
        ore.transform.SetParent(null);
        ore.Collect();
    }
}