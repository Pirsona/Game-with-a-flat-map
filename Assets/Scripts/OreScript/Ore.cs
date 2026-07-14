using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Ore : MonoBehaviour
{
    [SerializeField] private OreConfig _oreConfig;
    
    private Collider _collider;
    
    public float OreCost => _oreConfig.Cost;

    public event Action<Ore>  Collected;
    
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void DisableCollider()
    {
        _collider.enabled = false;
    }

    public void Collecting()
    {
        Collected?.Invoke(this);
    }

    public void ResetState()
    {
        _collider.enabled = true;
        transform.position = Vector3.zero;
    }
}