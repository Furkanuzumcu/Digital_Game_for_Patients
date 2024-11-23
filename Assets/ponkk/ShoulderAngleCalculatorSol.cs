using UnityEngine;

public class ShoulderAngleCalculatorSol : MonoBehaviour
{
    public Transform shoulder;   // Sol omuz transform referans�
    public Transform upperArm;   // Sol �st kol transform referans�
    public Transform forearm;    // Sol �n kol transform referans�

    private Vector3 initialShoulderDirection;

    void Start()
    {
        // Omuzun ba�lang�� rotasyonunu kaydet
        initialShoulderDirection = (upperArm.position - shoulder.position).normalized;
    }

    void Update()
    {
        // A�� hesaplama fonksiyonunu s�rekli �a��r
        CalculateAngle();
    }

    public float GetCurrentAngle()
    {
        // A�� hesaplama k�sm�, d��ar�ya a��y� d�nd�rmek i�in fonksiyon
        return CalculateAngle();
    }

    private float CalculateAngle()
    {
        // Omuz ve �st kolun y�n vekt�rlerini hesapla
        Vector3 currentShoulderDirection = (upperArm.position - shoulder.position).normalized;

        // Ba�lang�� y�n�ne g�re a��y� hesapla
        float angle = Vector3.Angle(initialShoulderDirection, currentShoulderDirection);

        // �st kolun a�a�� veya yukar� hareketini kontrol et
        Vector3 cross = Vector3.Cross(initialShoulderDirection, currentShoulderDirection);
        if (cross.z < 0)
        {
            angle = -angle;
        }

        // A�� tersine �evriliyor: Kol yukar�da 180, a�a��da 0 ve 90 derece d���r�l�yor
        float finalAngle = Mathf.Clamp(180 - (angle + 90), 0, 180);

        // A�� de�erini geri d�nd�r
        Debug.Log("Sol Kol A��: " + finalAngle);
        return finalAngle;
    }
}
