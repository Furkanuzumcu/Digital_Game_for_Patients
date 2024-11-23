using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Uygulama kapatýlýyor...");
        Application.Quit();
    }
}
