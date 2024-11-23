using UnityEngine;

public class LeftScoreWall : MonoBehaviour
{
    public ScoreManagerSol scoreManagerSol;  // Sol kol i�in ScoreManager
    public ScoreManager scoreManager;        // Sa� kol i�in ScoreManager
    public topkontrol ballController;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            // E�er ScoreManagerSol atanm��sa, sol kol i�in skoru art�r
            if (scoreManagerSol != null)
            {
                
                ballController.ResetBallPosition();
            }
            // E�er ScoreManager atanm��sa, sa� kol i�in skoru art�r
            else if (scoreManager != null)
            {
               
                ballController.ResetBallPosition();
            }
            else
            {
                Debug.LogWarning("ScoreManager referanslar� atanmad�!");
            }
        }
    }
}
