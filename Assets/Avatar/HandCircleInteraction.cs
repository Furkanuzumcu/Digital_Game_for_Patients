using UnityEngine;
using UnityEngine.UI;

public class HandCircleInteraction : MonoBehaviour
{
    public Image leftCircle;
    public Image rightCircle;
    public KinectManager kinectManager;
    public Color activeColor = Color.green;
    public Color inactiveColor = Color.red;
    public float circleRadius = 50f;

    void Update()
    {
        if (kinectManager && kinectManager.IsUserDetected())
        {
            long userId = kinectManager.GetPrimaryUserID();

            // Sol elin ekran �zerindeki pozisyonunu al (Z'yi ihmal ederek)
            Vector3 leftHandPos = kinectManager.GetJointPosition(userId, (int)KinectInterop.JointType.HandLeft);
            Vector2 leftHandScreenPos = Camera.main.WorldToScreenPoint(new Vector3(leftHandPos.x, leftHandPos.y, Camera.main.nearClipPlane));

            // Sa� elin ekran �zerindeki pozisyonunu al (Z'yi ihmal ederek)
            Vector3 rightHandPos = kinectManager.GetJointPosition(userId, (int)KinectInterop.JointType.HandRight);
            Vector2 rightHandScreenPos = Camera.main.WorldToScreenPoint(new Vector3(rightHandPos.x, rightHandPos.y, Camera.main.nearClipPlane));

            // Canvas �zerinden do�ru pozisyonlar� elde etmek
            RectTransformUtility.ScreenPointToLocalPointInRectangle(leftCircle.rectTransform, leftHandScreenPos, Camera.main, out Vector2 leftHandLocalPos);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rightCircle.rectTransform, rightHandScreenPos, Camera.main, out Vector2 rightHandLocalPos);

            Vector2 leftCircleScreenPos = leftCircle.rectTransform.anchoredPosition;
            Vector2 rightCircleScreenPos = rightCircle.rectTransform.anchoredPosition;

            // Debug.Log ile pozisyonlar� yazd�r
            Debug.Log($"Sol El: {leftHandLocalPos}, Sol Yuvarlak: {leftCircleScreenPos}");
            Debug.Log($"Sa� El: {rightHandLocalPos}, Sa� Yuvarlak: {rightCircleScreenPos}");

            // Sol elin sol yuvarla�a yak�n olup olmad���n� kontrol et
            if (Vector2.Distance(leftHandLocalPos, leftCircleScreenPos) <= circleRadius)
            {
                leftCircle.color = activeColor;
            }
            else
            {
                leftCircle.color = inactiveColor;
            }

            // Sa� elin sa� yuvarla�a yak�n olup olmad���n� kontrol et
            if (Vector2.Distance(rightHandLocalPos, rightCircleScreenPos) <= circleRadius)
            {
                rightCircle.color = activeColor;
            }
            else
            {
                rightCircle.color = inactiveColor;
            }
        }
    }
}
