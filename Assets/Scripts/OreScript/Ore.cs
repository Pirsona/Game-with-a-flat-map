using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Ore : MonoBehaviour
{
    [SerializeField] private OreConfig _oreConfig;
    
    private Collider _collider;
    
    public bool IsBooked { get; private set; } = false;
    public float OreCoast => _oreConfig.Coast;

    public event Action<Ore>  Delivered;
    
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void BookOre()
    {
        IsBooked = true;
    }

    public void DisableCollider()
    {
        _collider.enabled = false;
    }

    public void Deliver()
    {
        Delivered?.Invoke(this);
    }

    public void ResetState()
    {
        IsBooked = false;
        _collider.enabled = true;
        transform.position = Vector3.zero;
    }
}