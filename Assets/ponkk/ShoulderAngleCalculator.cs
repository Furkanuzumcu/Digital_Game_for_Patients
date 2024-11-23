using UnityEngine;

public class ShoulderAngleCalculator : MonoBehaviour
{
    public Transform shoulder;   // Omuz transform referans�
    public Transform upperArm;   // �st kol transform referans�
    public Transform body;       // G�vde veya s�rt referans�

    private Vector3 initialShoulderDirection;
    private float previousAngle = 0f;
    private float smoothingSpeed = 0.05f; // Genel yumu�atma h�z�
    private float lowPassFactor = 0.1f;   // D���k ge�i�li filtre fakt�r�

    private float filteredAngle = 0f;     // Filtrelenmi� a��

    void Start()
    {
        // Omuzun ba�lang�� rotasyonunu kaydet
        initialShoulderDirection = (upperArm.position - shoulder.position).normalized;
    }

    void Update()
    {
        // S�rekli a��y� hesaplamak i�in
        float currentAngle = CalculateAngle();

        // Low-pass filter uygulayarak ani de�i�imleri yumu�at
        filteredAngle = Mathf.Lerp(filteredAngle, currentAngle, lowPassFactor);

        // A�� de�erini debug log ile yazd�r
        Debug.Log("Omuz ile �st kol aras�ndaki filtrelenmi� a��: " + filteredAngle);
    }

    public float GetCurrentAngle()
    {
        // A�� hesaplama k�sm�, d��ar�ya filtrelenmi� a��y� d�nd�rmek i�in fonksiyon
        return filteredAngle;
    }

    private float CalculateAngle()
    {
        // Omuz ve �st kolun y�n vekt�rlerini hesapla
        Vector3 currentShoulderDirection = (upperArm.position - shoulder.position).normalized;

        // Ba�lang�� y�n�ne g�re a��y� hesapla
        float angle = Vector3.Angle(initialShoulderDirection, currentShoulderDirection);

        // E�er �st kolun yukar� hareketi varsa a��y� do�ru �ekilde s�n�rland�r
        Vector3 cross = Vector3.Cross(initialShoulderDirection, currentShoulderDirection);
        if (cross.z < 0)
        {
            angle = -angle;
        }

        // A�� s�n�rlar�n� do�ru olarak g�ncelle
        float finalAngle = Mathf.Clamp(angle + 90, 0, 180);

        // Genel yumu�atma ile a��y� daha p�r�zs�z hale getirelim
        finalAngle = Mathf.Lerp(previousAngle, finalAngle, smoothingSpeed);

        previousAngle = finalAngle;

        // A�� de�erini geri d�nd�r
        return finalAngle;
    }
}