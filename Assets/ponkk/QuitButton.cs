using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Uygulama kapat�l�yor...");
        Application.Quit();
    }
}
