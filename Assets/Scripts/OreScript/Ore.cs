using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Ore : MonoBehaviour
{
    [SerializeField] private OreConfig _oreConfig;
    
    private Collider _collider;
    
    
    public bool IsBooked { get; private set; } = false;
    public float OreCoast => _oreConfig.Coast;

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
}
