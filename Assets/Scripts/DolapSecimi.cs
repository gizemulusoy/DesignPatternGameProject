using UnityEngine;
using UnityEngine.UI;

public class DolapSecimi : MonoBehaviour
{
    public GameObject secimPaneli;  // Yazı ve butonları içeren panel
    public Button Gri;              // Gri buton
    public Button Kahverengi;       // Kahverengi buton
    public GameObject rafSecimiObjesi; // RafSecimi'nin bağlı olduğu GameObject

    void Start()
    {
        // Null kontrolü ekleyelim
        if (Gri == null || Kahverengi == null || secimPaneli == null || rafSecimiObjesi == null)
        {
            Debug.LogError("Bir veya daha fazla referans eksik! Lütfen tüm nesneleri bağlayın.");
            return; // Eğer bir referans eksikse kodu çalıştırma
        }

        Gri.onClick.AddListener(() => DolapSec("gri"));
        Kahverengi.onClick.AddListener(() => DolapSec("kahve"));
        
        // RafSecimi objesini başta devre dışı bırak
        rafSecimiObjesi.SetActive(false);
    }

    void DolapSec(string renk)
    {
        Debug.Log("Seçilen dolap: " + renk);

        // Seçimi PlayerPrefs ile kaydet
        PlayerPrefs.SetString("SecilenDolap", renk);
        PlayerPrefs.Save();

        Gri.gameObject.SetActive(false);
        Kahverengi.gameObject.SetActive(false);
        
        // Paneli kapat
        secimPaneli.SetActive(false);
        
        // RafSecimi objesini aktif hale getir
        rafSecimiObjesi.SetActive(true);
    }
}