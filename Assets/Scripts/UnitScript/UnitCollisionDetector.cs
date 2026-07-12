using UnityEngine;

namespace UnitScript
{
    public class UnitCollisionDetector  : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Ore ore))
            {
                if (ore.IsBooked)
                {
                    
                }
            }
        }
    }
}