using UnityEngine;

public class Paddle : MonoBehaviour
{
    public HorizontalAngleCalculator angleCalculator;   // Omuz açısını hesaplayan script referansı

    // Paddle'ın hareket edebileceği x pozisyon sınırları
    private float minX = -8.7f;
    private float maxX = 8.7f;
    private float totalDistance = 17.4f; // -15.7 ile 15.7 arası mesafe
    public float maxSpeed = 30f;  // Maksimum hız

    // Açı aralığı
    private float minAngle = -80f;
    private float maxAngle = -30f;
    private float totalAngleRange = 50f; // -30 - (-80)

    private float targetX; // Hedef pozisyon

    void Update()
    {
        // Şu anki yatay omuz açısını al
        float currentAngle = angleCalculator.GetCurrentHorizontalAngle();

        // Hedef pozisyonu hesapla
        if (currentAngle >= -30f)
        {
            targetX = minX;
        }
        else if (currentAngle <= -80f)
        {
            targetX = maxX;
        }
        else
        {
            float angleOffset = currentAngle - minAngle; // Açının -80'den farkı
            float xOffset = (angleOffset / totalAngleRange) * totalDistance; // Açı farkına göre x offseti hesapla
            targetX = maxX - xOffset;
        }

        // Titremeyi azaltmak için Mathf.MoveTowards kullan
        float newX = Mathf.MoveTowards(transform.position.x, targetX, maxSpeed * Time.deltaTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // Debug için açı ve x pozisyonunu ekrana yazdır
        Debug.Log("Açı: " + currentAngle + " X Pozisyonu: " + transform.position.x);
    }

    // Paddle'ı sıfırlamak için kullanılan fonksiyon
    public void ResetPaddle()
    {
        transform.position = new Vector3(0f, transform.position.y, transform.position.z);
    }
}
