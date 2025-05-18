using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Butona basıldığında çağrılacak fonksiyon
    public void StartGame()
    {
        // Sahne ismini burada belirt
        SceneManager.LoadScene("AFInfo1");
    }

    // Opsiyonel: Oyundan çıkmak için
    public void QuitGame()
    {
        Application.Quit();
    }
}
