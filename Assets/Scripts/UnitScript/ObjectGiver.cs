using UnityEngine;

public class ObjectGiver : MonoBehaviour
{
    public void DropOre(Ore ore)
    {
        ore.transform.SetParent(null);
        ore.Deliver();
    }
}