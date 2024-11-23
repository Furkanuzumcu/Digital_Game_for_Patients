using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ResetZonesol : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnBallMiss();
        }
        else
        {
            Debug.LogError("GameManager.Instance is null. Make sure GameManager is properly initialized.");
        }
    }

}
