using UnityEngine;

public class Paddlesol : MonoBehaviour
{
    public HorizontalAngleCalculator angleCalculator;   // Omuz açýsýný hesaplayan script referansý

    // Paddle'ýn hareket edebileceði x pozisyon sýnýrlarý
    private float minX = -8.7f;  // Sol pozisyon (en sol)
    private float maxX = 8.7f;   // Sað pozisyon (en sað)
    private float totalDistance = 17.4f; // -15.7 ile 15.7 arasý mesafe
    public float maxSpeed = 10f;  // Maksimum hýz

    // Açý aralýðý
    private float minAngle = -150f;  // Minimum açý (en sað)
    private float maxAngle = -100f;  // Maksimum açý (en sol)
    private float totalAngleRange = 50f; // Açý farký -150 ile -100 arasýnda

    private float targetX; // Hedef pozisyon

    void Update()
    {
        // Þu anki yatay omuz açýsýný al
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
            float angleOffset = currentAngle - minAngle; // Açýnýn -150'den farký
            float xOffset = (angleOffset / totalAngleRange) * totalDistance; // Açý farkýna göre x offseti hesapla
            targetX = maxX - xOffset;
        }

        // Titremeyi azaltmak için Mathf.MoveTowards kullan
        float newX = Mathf.MoveTowards(transform.position.x, targetX, maxSpeed * Time.deltaTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // Debug için açý ve x pozisyonunu ekrana yazdýr
        Debug.Log("Açý: " + currentAngle + " X Pozisyonu: " + transform.position.x);
    }

    // Paddle'ý sýfýrlamak için kullanýlan fonksiyon
    public void ResetPaddle()
    {
        // Paddle'ý baþlangýç pozisyonuna sýfýrla (ortaya)
        transform.position = new Vector3(0f, transform.position.y, transform.position.z);
    }
}
