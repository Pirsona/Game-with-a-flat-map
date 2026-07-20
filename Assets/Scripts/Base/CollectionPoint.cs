using System;
using UnityEngine;

public class CollectionPoint : MonoBehaviour
{
    public event Action<Unit, Ore> UnitTaskFinished;

    public void FinishTask(Unit unit, Ore ore)
    {
        UnitTaskFinished?.Invoke(unit, ore);
    }
}
