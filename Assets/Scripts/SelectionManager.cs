using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance;
    private PlayerMovement selectedPlayer;

    private void Awake()
    {
        // Singleton gibi çalışsın
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Select(PlayerMovement newPlayer)
    {
        // Önce eski seçimi kapat
        if (selectedPlayer != null)
            selectedPlayer.isSelected = false;

        // Yeni seçimi aktif et
        selectedPlayer = newPlayer;
        selectedPlayer.isSelected = true;
    }
}