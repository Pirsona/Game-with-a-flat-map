using UnityEngine;

public class UnitMover : MonoBehaviour
{
    
    public void MoveToTarget(Vector3 target, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
    }
}