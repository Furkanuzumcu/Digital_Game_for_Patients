using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Uygulama kapatılıyor...");
        Application.Quit();
    }
}
