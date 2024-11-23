using UnityEngine;

public class PaddleBoundary : MonoBehaviour
{
    public float upperBound = 4f; // Yukarýdaki sýnýr
    public float lowerBound = -4f; // Aþaðýdaki sýnýr

    void Update()
    {
        // Paddle'ýn pozisyonunu sýnýrlarla kýsýtla
        Vector3 position = transform.position;
        position.y = Mathf.Clamp(position.y, lowerBound, upperBound);
        transform.position = position;
    }
}
