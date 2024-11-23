using UnityEngine;

public class HandProximityDetection : MonoBehaviour
{
    public GameObject leftHandMarker;  // Sol el markeri
    public GameObject rightHandMarker; // Sað el markeri

    public GameObject leftSphere;  // Sol küre
    public GameObject rightSphere; // Sað küre

    public GameObject paddle;  // Paddle (raket)

    public Material activeMaterial;
    public Material inactiveMaterial;

    public float detectionRange = 0.5f;  // Mesafe eþiði
    public float paddleSpeed = 5f;  // Paddle hýzý

    void Update()
    {
        // Sol el ile sol küre arasýndaki mesafeyi kontrol et
        float leftHandDistance = Vector3.Distance(leftHandMarker.transform.position, leftSphere.transform.position);
        if (leftHandDistance <= detectionRange)
        {
            leftSphere.GetComponent<Renderer>().material = activeMaterial;
            MovePaddle(Vector3.down);  // Sol el yaklaþtýðýnda paddle aþaðý hareket eder
        }
        else
        {
            leftSphere.GetComponent<Renderer>().material = inactiveMaterial;
        }

        // Sað el ile sað küre arasýndaki mesafeyi kontrol et
        float rightHandDistance = Vector3.Distance(rightHandMarker.transform.position, rightSphere.transform.position);
        if (rightHandDistance <= detectionRange)
        {
            rightSphere.GetComponent<Renderer>().material = activeMaterial;
            MovePaddle(Vector3.up);  // Sað el yaklaþtýðýnda paddle yukarý hareket eder
        }
        else
        {
            rightSphere.GetComponent<Renderer>().material = inactiveMaterial;
        }
    }

    // Paddle'ý belirtilen yönde hareket ettirir
    void MovePaddle(Vector3 direction)
    {
        paddle.transform.Translate(direction * paddleSpeed * Time.deltaTime);
    }
}
