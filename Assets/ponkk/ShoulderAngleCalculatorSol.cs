using UnityEngine;

public class ShoulderAngleCalculatorSol : MonoBehaviour
{
    public Transform shoulder;   // Sol omuz transform referansý
    public Transform upperArm;   // Sol üst kol transform referansý
    public Transform forearm;    // Sol ön kol transform referansý

    private Vector3 initialShoulderDirection;

    void Start()
    {
        // Omuzun baþlangýç rotasyonunu kaydet
        initialShoulderDirection = (upperArm.position - shoulder.position).normalized;
    }

    void Update()
    {
        // Açý hesaplama fonksiyonunu sürekli çaðýr
        CalculateAngle();
    }

    public float GetCurrentAngle()
    {
        // Açý hesaplama kýsmý, dýþarýya açýyý döndürmek için fonksiyon
        return CalculateAngle();
    }

    private float CalculateAngle()
    {
        // Omuz ve üst kolun yön vektörlerini hesapla
        Vector3 currentShoulderDirection = (upperArm.position - shoulder.position).normalized;

        // Baþlangýç yönüne göre açýyý hesapla
        float angle = Vector3.Angle(initialShoulderDirection, currentShoulderDirection);

        // Üst kolun aþaðý veya yukarý hareketini kontrol et
        Vector3 cross = Vector3.Cross(initialShoulderDirection, currentShoulderDirection);
        if (cross.z < 0)
        {
            angle = -angle;
        }

        // Açý tersine çevriliyor: Kol yukarýda 180, aþaðýda 0 ve 90 derece düþürülüyor
        float finalAngle = Mathf.Clamp(180 - (angle + 90), 0, 180);

        // Açý deðerini geri döndür
        Debug.Log("Sol Kol Açý: " + finalAngle);
        return finalAngle;
    }
}
