using UnityEngine;

public class Paddlesol : MonoBehaviour
{
    public HorizontalAngleCalculator angleCalculator;   // Omuz a��s�n� hesaplayan script referans�

    // Paddle'�n hareket edebilece�i x pozisyon s�n�rlar�
    private float minX = -8.7f;  // Sol pozisyon (en sol)
    private float maxX = 8.7f;   // Sa� pozisyon (en sa�)
    private float totalDistance = 17.4f; // -15.7 ile 15.7 aras� mesafe
    public float maxSpeed = 10f;  // Maksimum h�z

    // A�� aral���
    private float minAngle = -150f;  // Minimum a�� (en sa�)
    private float maxAngle = -100f;  // Maksimum a�� (en sol)
    private float totalAngleRange = 50f; // A�� fark� -150 ile -100 aras�nda

    private float targetX; // Hedef pozisyon

    void Update()
    {
        // �u anki yatay omuz a��s�n� al
        float currentAngle = angleCalculator.GetCurrentHorizontalAngle();

        // Hedef pozisyonu hesapla
        if (currentAngle >= maxAngle)
        {
            targetX = minX;
        }
        else if (currentAngle <= minAngle)
        {
            targetX = maxX;
        }
        else
        {
            float angleOffset = currentAngle - minAngle; // A��n�n -150'den fark�
            float xOffset = (angleOffset / totalAngleRange) * totalDistance; // A�� fark�na g�re x offseti hesapla
            targetX = maxX - xOffset;
        }

        // Titremeyi azaltmak i�in Mathf.MoveTowards kullan
        float newX = Mathf.MoveTowards(transform.position.x, targetX, maxSpeed * Time.deltaTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // Debug i�in a�� ve x pozisyonunu ekrana yazd�r
        Debug.Log("A��: " + currentAngle + " X Pozisyonu: " + transform.position.x);
    }

    // Paddle'� s�f�rlamak i�in kullan�lan fonksiyon
    public void ResetPaddle()
    {
        // Paddle'� ba�lang�� pozisyonuna s�f�rla (ortaya)
        transform.position = new Vector3(0f, transform.position.y, transform.position.z);
    }
}
