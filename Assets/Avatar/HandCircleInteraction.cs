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

            // Sol elin ekran üzerindeki pozisyonunu al (Z'yi ihmal ederek)
            Vector3 leftHandPos = kinectManager.GetJointPosition(userId, (int)KinectInterop.JointType.HandLeft);
            Vector2 leftHandScreenPos = Camera.main.WorldToScreenPoint(new Vector3(leftHandPos.x, leftHandPos.y, Camera.main.nearClipPlane));

            // Sað elin ekran üzerindeki pozisyonunu al (Z'yi ihmal ederek)
            Vector3 rightHandPos = kinectManager.GetJointPosition(userId, (int)KinectInterop.JointType.HandRight);
            Vector2 rightHandScreenPos = Camera.main.WorldToScreenPoint(new Vector3(rightHandPos.x, rightHandPos.y, Camera.main.nearClipPlane));

            // Canvas üzerinden doðru pozisyonlarý elde etmek
            RectTransformUtility.ScreenPointToLocalPointInRectangle(leftCircle.rectTransform, leftHandScreenPos, Camera.main, out Vector2 leftHandLocalPos);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rightCircle.rectTransform, rightHandScreenPos, Camera.main, out Vector2 rightHandLocalPos);

            Vector2 leftCircleScreenPos = leftCircle.rectTransform.anchoredPosition;
            Vector2 rightCircleScreenPos = rightCircle.rectTransform.anchoredPosition;

            // Debug.Log ile pozisyonlarý yazdýr
            Debug.Log($"Sol El: {leftHandLocalPos}, Sol Yuvarlak: {leftCircleScreenPos}");
            Debug.Log($"Sað El: {rightHandLocalPos}, Sað Yuvarlak: {rightCircleScreenPos}");

            // Sol elin sol yuvarlaða yakýn olup olmadýðýný kontrol et
            if (Vector2.Distance(leftHandLocalPos, leftCircleScreenPos) <= circleRadius)
            {
                leftCircle.color = activeColor;
            }
            else
            {
                leftCircle.color = inactiveColor;
            }

            // Sað elin sað yuvarlaða yakýn olup olmadýðýný kontrol et
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
