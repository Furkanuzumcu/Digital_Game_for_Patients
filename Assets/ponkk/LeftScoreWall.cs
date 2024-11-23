using UnityEngine;

public class LeftScoreWall : MonoBehaviour
{
    public ScoreManagerSol scoreManagerSol;  // Sol kol için ScoreManager
    public ScoreManager scoreManager;        // Sað kol için ScoreManager
    public topkontrol ballController;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            // Eðer ScoreManagerSol atanmýþsa, sol kol için skoru artýr
            if (scoreManagerSol != null)
            {
                
                ballController.ResetBallPosition();
            }
            // Eðer ScoreManager atanmýþsa, sað kol için skoru artýr
            else if (scoreManager != null)
            {
               
                ballController.ResetBallPosition();
            }
            else
            {
                Debug.LogWarning("ScoreManager referanslarý atanmadý!");
            }
        }
    }
}
