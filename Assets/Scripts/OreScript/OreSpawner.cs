using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace OreScript
{
    public class OreSpawner : Spawner<Ore>
    {
        [SerializeField] private float _scanRadius;
        [SerializeField] private float _cooldown;
        [SerializeField] private LayerMask _obstacleLayer;
        
        private bool _isSpawn = true;
        private WaitForSeconds _wait;
        private Coroutine _spawnCoroutine;


        [CanBeNull] public event Action<float> ReturnedOre;

        protected override void Awake()
        {
            base.Awake();
            _wait = new WaitForSeconds(_cooldown);
        }
        
        private void OnEnable()
        {
            _spawnCoroutine = StartCoroutine(Spawn());
        }

        private void OnDisable()
        {
            StopCoroutine(_spawnCoroutine);
        }

        private IEnumerator Spawn()
        {
            while (_isSpawn)
            { 
                yield return _wait;
                Ore spawnedOre = GetObject();
                spawnedOre.Delivered += Return;
                spawnedOre.transform.position = GetValidSpawnPosition();
            }
        }
        
        private Vector3 GetValidSpawnPosition()
        {
            bool isPositionFound = false;
            
            Vector3 randomPosition = Vector3.zero;
            Vector3 center = transform.position;
            Vector3 extents = transform.localScale / 2f;
            
            while (isPositionFound == false)
            {
                float xPosition = UnityEngine.Random.Range(center.x - extents.x, center.x + extents.x);
                float zPosition = UnityEngine.Random.Range(center.z - extents.z, center.z + extents.z);
                
                randomPosition = new Vector3(xPosition, center.y, zPosition);
                
                bool hasObject = Physics.CheckSphere(randomPosition, _scanRadius, _obstacleLayer);
                
                if (hasObject == false)
                {
                    isPositionFound = true;
                }
            }

            return randomPosition;
        }
        
        private void Return(Ore ore)
        {
            ore.ResetState();
            ore.Delivered -= Return;
            ReturnedOre?.Invoke(ore.OreCoast);
            ReturnObject(ore);
        }
    }
}