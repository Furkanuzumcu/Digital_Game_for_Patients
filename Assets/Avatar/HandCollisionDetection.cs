using UnityEngine;

public class HandProximityDetection : MonoBehaviour
{
    public GameObject leftHandMarker;  // Sol el markeri
    public GameObject rightHandMarker; // Sa� el markeri

    public GameObject leftSphere;  // Sol k�re
    public GameObject rightSphere; // Sa� k�re

    public GameObject paddle;  // Paddle (raket)

    public Material activeMaterial;
    public Material inactiveMaterial;

    public float detectionRange = 0.5f;  // Mesafe e�i�i
    public float paddleSpeed = 5f;  // Paddle h�z�

    void Update()
    {
        // Sol el ile sol k�re aras�ndaki mesafeyi kontrol et
        float leftHandDistance = Vector3.Distance(leftHandMarker.transform.position, leftSphere.transform.position);
        if (leftHandDistance <= detectionRange)
        {
            leftSphere.GetComponent<Renderer>().material = activeMaterial;
            MovePaddle(Vector3.down);  // Sol el yakla�t���nda paddle a�a�� hareket eder
        }
        else
        {
            leftSphere.GetComponent<Renderer>().material = inactiveMaterial;
        }

        // Sa� el ile sa� k�re aras�ndaki mesafeyi kontrol et
        float rightHandDistance = Vector3.Distance(rightHandMarker.transform.position, rightSphere.transform.position);
        if (rightHandDistance <= detectionRange)
        {
            rightSphere.GetComponent<Renderer>().material = activeMaterial;
            MovePaddle(Vector3.up);  // Sa� el yakla�t���nda paddle yukar� hareket eder
        }
        else
        {
            rightSphere.GetComponent<Renderer>().material = inactiveMaterial;
        }
    }

    // Paddle'� belirtilen y�nde hareket ettirir
    void MovePaddle(Vector3 direction)
    {
        paddle.transform.Translate(direction * paddleSpeed * Time.deltaTime);
    }
}
