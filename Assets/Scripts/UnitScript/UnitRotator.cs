using UnityEngine;

public class UnitRotator : MonoBehaviour
{
    public void RotateToTarget( Vector3 target, float speed)
    {
        Vector3 targetDirection = target - transform.position;
        targetDirection.y = 0;
        
        if(targetDirection == Vector3.zero)
        {
            return;
        }
        
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * speed);
    }
}