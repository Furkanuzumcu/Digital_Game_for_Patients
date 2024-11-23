using UnityEngine;

public class HorizontalAngleCalculator : MonoBehaviour
{
    public Transform shoulder;   // Omuz referansý
    public Transform upperArm;   // Üst kol referansý

    private Vector3 initialShoulderForward;

    void Start()
    {
        // Eðer referans yoksa hata almamak için kontrol et
        if (shoulder != null)
        {
            initialShoulderForward = shoulder.forward;
            initialShoulderForward.y = 0; // Y eksenini sýfýrlayarak yatay düzleme odaklan
            initialShoulderForward.Normalize();
        }
    }

    public float GetCurrentHorizontalAngle()
    {
        // Eðer referans eksikse geriye sýfýr döndür
        if (shoulder == null || upperArm == null)
        {
            return 0f;  // Hata almamak için default deðer döndürüyoruz
        }

        // Üst kolun yatay düzlemdeki yönünü al
        Vector3 upperArmDirection = upperArm.position - shoulder.position;
        upperArmDirection.y = 0;
        upperArmDirection.Normalize();

        // Omuzun baþlangýç yönü ile üst kol arasýndaki açýyý hesapla
        float angle = Vector3.SignedAngle(initialShoulderForward, upperArmDirection, Vector3.up);
        return angle;
    }
}
