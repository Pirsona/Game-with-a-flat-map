using UnityEngine;

public class Ore : MonoBehaviour
{
    public bool IsBooked { get; private set; } = false;


    public void BookOre()
    {
        IsBooked = true;
    }
}
