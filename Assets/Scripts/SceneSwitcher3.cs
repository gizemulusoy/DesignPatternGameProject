using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher3 : MonoBehaviour
{
    [SerializeField] private string nextScene;     // Sağ ok ile gidilecek sahne
    [SerializeField] private string previousScene; // Sol ok ile gidilecek sahne

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && !string.IsNullOrEmpty(nextScene))
        {
            SceneManager.LoadScene(nextScene);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && !string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene);
        }
    }
}