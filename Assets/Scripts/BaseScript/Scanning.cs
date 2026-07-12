using System;
using System.Collections;
using UnityEngine;


public class Scanning : MonoBehaviour
{
    private const bool IsScanning = true;
    private const int StandardArayLength = 20;
    
    [SerializeField] private float _scanningRadius;
    [SerializeField] private float _cooldown;
    [SerializeField] private LayerMask _layerMask;
    
    private WaitForSeconds _wait;
    private Coroutine _coroutine;
    private Collider[] _colliders;
    
    public event Action<Ore> OreFound;

    private void Awake()
    {
        _colliders = new Collider[StandardArayLength];
        _wait = new WaitForSeconds(_cooldown);
    }

    private void OnEnable()
    {
        _coroutine = StartCoroutine(ScanForOre());
    }

    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }
    
    private IEnumerator ScanForOre()
    {
        while (IsScanning)
        {
            yield return _wait;
            
            int colliders = Physics.OverlapSphereNonAlloc(transform.position, _scanningRadius, _colliders, _layerMask);

            for (int i = 0; i < colliders; i++)
            {
                Collider currentCollider = _colliders[i];
                
                if(currentCollider.TryGetComponent(out Ore ore) && ore.IsBooked == false)
                {
                    OreFound?.Invoke(ore);
                    break;
                }
            }
        }
    }
}