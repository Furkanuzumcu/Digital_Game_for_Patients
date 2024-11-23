using UnityEngine;

public class HandPositionSync : MonoBehaviour
{
    public Transform rightHand;  // Sa� elin Transform'u
    public Transform leftHand;   // Sol elin Transform'u

    public Transform rightHandMarker;  // Sa� elin markeri
    public Transform leftHandMarker;   // Sol elin markeri

    void Update()
    {
        // Sa� el marker'�n�n pozisyonunu sa� elin pozisyonuna ayarla
        rightHandMarker.position = rightHand.position;
        rightHandMarker.rotation = rightHand.rotation;

        // Sol el marker'�n�n pozisyonunu sol elin pozisyonuna ayarla
        leftHandMarker.position = leftHand.position;
        leftHandMarker.rotation = leftHand.rotation;
    }
}
