using UnityEngine;

public class HandPositionSync : MonoBehaviour
{
    public Transform rightHand;  // Sað elin Transform'u
    public Transform leftHand;   // Sol elin Transform'u

    public Transform rightHandMarker;  // Sað elin markeri
    public Transform leftHandMarker;   // Sol elin markeri

    void Update()
    {
        // Sað el marker'ýnýn pozisyonunu sað elin pozisyonuna ayarla
        rightHandMarker.position = rightHand.position;
        rightHandMarker.rotation = rightHand.rotation;

        // Sol el marker'ýnýn pozisyonunu sol elin pozisyonuna ayarla
        leftHandMarker.position = leftHand.position;
        leftHandMarker.rotation = leftHand.rotation;
    }
}
