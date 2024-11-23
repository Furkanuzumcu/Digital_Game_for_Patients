using UnityEngine;

public class ElbowTracker : MonoBehaviour
{
    public Transform elbow;  // Avatarýn dirseðinin referansý

    void Update()
    {
        if (elbow != null)
        {
            // Dirseðin pozisyonuna git, ama parent-child iliþkisi olmadan
            transform.position = elbow.position;
        }
    }
}
