using System;
using UnityEngine;

public class ObjectCapture : MonoBehaviour
{
    [SerializeField] private Transform _capturePoint;
    
    
    public void PickUpOre(Ore ore)
    {
        ore.transform.SetParent(_capturePoint);
        ore.transform.localPosition = Vector3.zero;
        ore.DisableCollider();
    }
}