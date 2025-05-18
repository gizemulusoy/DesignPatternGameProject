using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher2 : MonoBehaviour
{
    [SerializeField] private string AFInfo3; // Geçmek istediğin sahnenin adını yaz

    void Update()
    {
        // Sadece belirli sahnede sağ ok tuşu çalışsın
        if (SceneManager.GetActiveScene().name == "AFInfo2") // Bu kısmı geçiş yapmak istediğin sahne adıyla değiştir
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                LoadTargetScene();
            }
        }
    }

    void LoadTargetScene()
    {
        // Hedef sahneye geçiş yap
        SceneManager.LoadScene("AFInfo3");
    }
}