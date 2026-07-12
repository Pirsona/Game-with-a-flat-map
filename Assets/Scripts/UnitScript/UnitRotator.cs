using JetBrains.Annotations;
using UnityEngine;

public class UnitRotator : MonoBehaviour
{
    public void RotateToTarget( Quaternion target, float speed)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, Time.deltaTime * speed);
    }
}