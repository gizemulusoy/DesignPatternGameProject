using UnityEngine;
using TMPro; // TextMeshPro'yu kullanmak için gerekli

public class PlayerControl : MonoBehaviour
{
    public GameObject buttonObject; // UI Button objesi
    public TextMeshProUGUI buttonText; // TextMeshPro Text bileşeni
    private bool buttonClicked = false; // Butona tıklanıp tıklanmadığını kontrol et
    private float buttonDisappearTime = 3f; // Butonun kaybolacağı süre (3 saniye)
    
    void Update()
    {
        // Eğer butona tıklanmışsa, kaybolmaya başlayacak
        if (buttonClicked)
        {
            buttonDisappearTime -= Time.deltaTime; // Zamanı azalt
            if (buttonDisappearTime <= 0f)
            {
                HideButton(); // Butonu gizle
                buttonClicked = false; // Buton tıklama durumunu sıfırla
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !buttonClicked) // Oyuncu belirli alana girdiğinde
        {
            ShowButton(); // Butonu göster
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Oyuncu alandan çıktığında
        {
            HideButton(); // Butonu gizle
        }
    }

    // Butonu ekranda göster
    void ShowButton()
    {
        buttonObject.SetActive(true); // Butonu aktif et
        buttonText.text = "Kontrol Et"; // Buton metnini TextMeshPro ile ayarla
    }

    // Butonu gizle
    void HideButton()
    {
        buttonObject.SetActive(false); // Butonu gizle
        buttonText.text = ""; // Buton metnini temizle
        buttonDisappearTime = 3f; // Zamanı sıfırla
    }

    // Butona tıklanmışsa yapılacak işlemler
    public void OnButtonClicked()
    {
        Debug.Log("butona bastın");
        buttonText.text = "Kontrol Edildi"; // Buton metnini değiştir
        buttonClicked = true; // Buton tıklama durumunu true yap
    }
}