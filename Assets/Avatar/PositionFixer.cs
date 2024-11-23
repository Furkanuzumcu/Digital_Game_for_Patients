using UnityEngine;

public class PositionFixer : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        // Ba�lang�� pozisyonunu ve rotasyonunu sakla
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        // Pozisyonu ve rotasyonu her karede s�f�rla
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}
