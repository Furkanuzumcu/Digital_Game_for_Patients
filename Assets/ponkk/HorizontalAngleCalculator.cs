using UnityEngine;

public class HorizontalAngleCalculator : MonoBehaviour
{
    public Transform shoulder;   // Omuz referans�
    public Transform upperArm;   // �st kol referans�

    private Vector3 initialShoulderForward;

    void Start()
    {
        // E�er referans yoksa hata almamak i�in kontrol et
        if (shoulder != null)
        {
            initialShoulderForward = shoulder.forward;
            initialShoulderForward.y = 0; // Y eksenini s�f�rlayarak yatay d�zleme odaklan
            initialShoulderForward.Normalize();
        }
    }

    public float GetCurrentHorizontalAngle()
    {
        // E�er referans eksikse geriye s�f�r d�nd�r
        if (shoulder == null || upperArm == null)
        {
            return 0f;  // Hata almamak i�in default de�er d�nd�r�yoruz
        }

        // �st kolun yatay d�zlemdeki y�n�n� al
        Vector3 upperArmDirection = upperArm.position - shoulder.position;
        upperArmDirection.y = 0;
        upperArmDirection.Normalize();

        // Omuzun ba�lang�� y�n� ile �st kol aras�ndaki a��y� hesapla
        float angle = Vector3.SignedAngle(initialShoulderForward, upperArmDirection, Vector3.up);
        return angle;
    }
}
