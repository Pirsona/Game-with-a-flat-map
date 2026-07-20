using System.Collections;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    public const float CloseEnoughDistance = 1f;

    public IEnumerator MoveCoroutine(Vector3 targetPosition, float speed)
    {
        bool isMoving = true;
        
        while (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
            
            if (Vector3Extensions.IsEnoughClose(transform.position, targetPosition, CloseEnoughDistance))
            {
                isMoving = false;
            }
            
            yield return null;
        }
    }
}