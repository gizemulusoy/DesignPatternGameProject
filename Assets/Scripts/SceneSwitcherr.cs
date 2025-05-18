using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private string AFInfo2; // Geçmek istediğin sahnenin adını yaz

    void Update()
    {
        // Sadece belirli sahnede sağ ok tuşu çalışsın
        if (SceneManager.GetActiveScene().name == "AFInfo1") // Bu kısmı geçiş yapmak istediğin sahne adıyla değiştir
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
        SceneManager.LoadScene("AFInfo2");
    }
}