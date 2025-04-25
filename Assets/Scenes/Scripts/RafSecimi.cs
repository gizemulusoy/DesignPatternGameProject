using UnityEngine;
using TMPro;

public class RafSecimi : MonoBehaviour
{
    public TMP_Text gorevText;
    public TMP_Text bilgiText;

    private string[] gorevIsimleri = new string[]
    {
        "Arka kasa ", "Sağ kasa", "Sol kasa", "Üst raf", "Alt raf",
        "Alt ön raf", "İç raf sol", "İç üst raf ", "İç alt raf ", "Sağ kapak", "Sol kapak"
    };

    private string[] objeAdlari;
    private int gorevIndex = 0;
    private string secilenDolap;
    private bool cevapBekleniyor = true;

    void Start()
    {
        secilenDolap = PlayerPrefs.GetString("SecilenDolap", "gri").ToLower();

        if (secilenDolap == "gri")
        {
            objeAdlari = new string[]
            {
                "GriArka", "GriSag", "GriSol", "GriUst", "GriAlt",
                "GriAltOn", "GriRafSol", "GriRaf1", "GriRaf2", "GriKapakSag", "GriKapakSol"
            };
        }
        else if (secilenDolap == "kahve")
        {
            objeAdlari = new string[]
            {
                "KahveArka", "KahveSag", "KahveSol", "KahveUst", "KahveAlt",
                "KahveAltOn", "KahveRafSol", "KahveRaf1", "KahveRaf2", "KahveKapakSag", "KahveKapakSol"
            };
        }

        // İlk görev tanımını yazdır
        GuncelleGorevMetni();
        bilgiText.text = "";
    }

    void Update()
    {
        if (!cevapBekleniyor) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                string objeninAdi = hit.collider.gameObject.name;

                if (objeninAdi == objeAdlari[gorevIndex])
                {
                    bilgiText.text = "Doğru!";
                    cevapBekleniyor = false;
                    gorevIndex++;

                    if (gorevIndex < gorevIsimleri.Length)
                    {
                        Invoke("YeniGorev", 1f);
                    }
                    else
                    {
                        gorevText.text = "";
                        bilgiText.text = "Tebrikler! Görev tamam.";
                        Invoke("BilgiMetniTemizle", 2f);
                    }
                }
                else
                {
                    bilgiText.text = "Tekrar dene.";
                    cevapBekleniyor = false;
                    Invoke("BilgiMetniTemizle", 1f);
                }
            }
        }
    }

    void YeniGorev()
    {
        bilgiText.text = "";
        cevapBekleniyor = true;
        GuncelleGorevMetni();
    }

    void GuncelleGorevMetni()
    {
        if (gorevIndex < gorevIsimleri.Length)
        {
            gorevText.text = gorevIsimleri[gorevIndex] + " seç";
        }
    }

    void BilgiMetniTemizle()
    {
        bilgiText.text = "";
        cevapBekleniyor = true;
    }
}
