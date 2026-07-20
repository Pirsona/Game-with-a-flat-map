using System.Collections;
using UnityEngine;

public class UnitRotator : MonoBehaviour
{
    private const float MinimumAngle = 0.1f;

    public IEnumerator RotateCoroutine(Vector3 target, float speed)
    {
        bool IsRotating = true;

        while (IsRotating)
        {

            Vector3 direction = target - transform.position;
            direction.y = 0;

            if (direction == Vector3.zero)
            {
                yield break;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            if (Quaternion.Angle(transform.rotation, targetRotation) < MinimumAngle)
            {
                transform.rotation = targetRotation;

                IsRotating = false;
            }
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * speed);
            
            yield return null;
        }
    }
}