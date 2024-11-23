using UnityEngine;

public class ShoulderAngleCalculator : MonoBehaviour
{
    public Transform shoulder;   // Omuz transform referansý
    public Transform upperArm;   // Üst kol transform referansý
    public Transform body;       // Gövde veya sýrt referansý

    private Vector3 initialShoulderDirection;
    private float previousAngle = 0f;
    private float smoothingSpeed = 0.05f; // Genel yumuþatma hýzý
    private float lowPassFactor = 0.1f;   // Düþük geçiþli filtre faktörü

    private float filteredAngle = 0f;     // Filtrelenmiþ açý

    void Start()
    {
        // Omuzun baþlangýç rotasyonunu kaydet
        initialShoulderDirection = (upperArm.position - shoulder.position).normalized;
    }

    void Update()
    {
        // Sürekli açýyý hesaplamak için
        float currentAngle = CalculateAngle();

        // Low-pass filter uygulayarak ani deðiþimleri yumuþat
        filteredAngle = Mathf.Lerp(filteredAngle, currentAngle, lowPassFactor);

        // Açý deðerini debug log ile yazdýr
        Debug.Log("Omuz ile üst kol arasýndaki filtrelenmiþ açý: " + filteredAngle);
    }

    public float GetCurrentAngle()
    {
        // Açý hesaplama kýsmý, dýþarýya filtrelenmiþ açýyý döndürmek için fonksiyon
        return filteredAngle;
    }

    private float CalculateAngle()
    {
        // Omuz ve üst kolun yön vektörlerini hesapla
        Vector3 currentShoulderDirection = (upperArm.position - shoulder.position).normalized;

        // Baþlangýç yönüne göre açýyý hesapla
        float angle = Vector3.Angle(initialShoulderDirection, currentShoulderDirection);

        // Eðer üst kolun yukarý hareketi varsa açýyý doðru þekilde sýnýrlandýr
        Vector3 cross = Vector3.Cross(initialShoulderDirection, currentShoulderDirection);
        if (cross.z < 0)
        {
            angle = -angle;
        }

        // Açý sýnýrlarýný doðru olarak güncelle
        float finalAngle = Mathf.Clamp(angle + 90, 0, 180);

        // Genel yumuþatma ile açýyý daha pürüzsüz hale getirelim
        finalAngle = Mathf.Lerp(previousAngle, finalAngle, smoothingSpeed);

        previousAngle = finalAngle;

        // Açý deðerini geri döndür
        return finalAngle;
    }
}