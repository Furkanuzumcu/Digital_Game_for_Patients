using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    public Transform paddle;   // Paddle'ın referansı
    public Transform elbowPositionTracker;  // ElbowTracker referansı

    // Raketin minimum ve maksimum y pozisyonları
    private float minY = -5.83f; // Pedalın minimum Y pozisyonu
    private float maxY = 2.22f;  // Pedalın maksimum Y pozisyonu
    private float totalDistance = 8.05f; // -5.83 ile 2.22 arası mesafe

    // Elbow'un Y pozisyonu aralığı
    private float minElbowY = -1.43f;  // Elbow'un minimum Y pozisyonu
    private float maxElbowY = -1.30f;  // Elbow'un maksimum Y pozisyonu

    // Yumuşatma hızı
    public float smoothingSpeed = 0.03f;

    // Pedal maksimum hızı
    public float maxPaddleSpeed = 7f;

    void Update()
    {
        // ElbowTracker'ın Y pozisyonunu al
        float elbowYPosition = elbowPositionTracker.position.y;

        float targetY; // Pedalın ulaşacağı hedef Y pozisyonu

        // Eğer elbow Y pozisyonu -1.40 veya daha küçükse, pedal pozisyonu minimum (yani -5.83) olacak
        if (elbowYPosition <= minElbowY)
        {
            targetY = minY;
        }
        // Eğer elbow Y pozisyonu -1.33 veya daha büyükse, pedal pozisyonu maksimum (yani 2.22) olacak
        else if (elbowYPosition >= maxElbowY)
        {
            targetY = maxY;
        }
        else
        {
            // Eğer elbow Y pozisyonu -1.40 ile -1.33 arasında ise orantılı olarak pedal pozisyonunu ayarla
            float elbowOffset = elbowYPosition - minElbowY;  // Elbow'un minimumdan farkı
            float elbowRange = maxElbowY - minElbowY;  // Elbow'un toplam hareket aralığı

            // Pedal pozisyonunu hesapla
            float normalizedElbow = elbowOffset / elbowRange; // Elbow'un normalize edilmiş değeri (0 ile 1 arasında)
            targetY = Mathf.Lerp(minY, maxY, normalizedElbow); // Pedalın hedef Y pozisyonunu hesapla
        }

        // Mevcut pozisyondan hedef pozisyona yumuşak geçiş, maksimum hız ile sınırlı
        float newY = Mathf.Lerp(paddle.position.y, targetY, smoothingSpeed);
        newY = Mathf.Clamp(newY, paddle.position.y - maxPaddleSpeed * Time.deltaTime, paddle.position.y + maxPaddleSpeed * Time.deltaTime);

        // Pedalın yeni Y pozisyonunu ayarla
        paddle.position = new Vector3(paddle.position.x, newY, paddle.position.z);

        // Debug log ile Elbow ve pedal pozisyonunu yazdır
        Debug.Log("Elbow Y Pozisyonu: " + elbowYPosition + " | Pedal Y Pozisyonu: " + paddle.position.y);
    }
}
