using UnityEngine;

public class ElbowTracker : MonoBehaviour
{
    public Transform elbow;  // Avatar�n dirse�inin referans�

    void Update()
    {
        if (elbow != null)
        {
            // Dirse�in pozisyonuna git, ama parent-child ili�kisi olmadan
            transform.position = elbow.position;
        }
    }
}
