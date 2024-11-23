using UnityEngine;

public class PaddleBoundary : MonoBehaviour
{
    public float upperBound = 4f; // Yukar�daki s�n�r
    public float lowerBound = -4f; // A�a��daki s�n�r

    void Update()
    {
        // Paddle'�n pozisyonunu s�n�rlarla k�s�tla
        Vector3 position = transform.position;
        position.y = Mathf.Clamp(position.y, lowerBound, upperBound);
        transform.position = position;
    }
}
