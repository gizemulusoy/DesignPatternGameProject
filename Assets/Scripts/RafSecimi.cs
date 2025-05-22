using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RafSecimi : MonoBehaviour
{
    public TMP_Text gorevText;
    public TMP_Text bilgiText;
    public TMP_Text scoreText; // Skor metni

    public float moveSpeed = 5f;

    private string[] gorevIsimleri = new string[]
    {
        "Arka kasa ", "Sağ kasa", "Sol kasa", "Üst kasa", "Alt kasa",
        "Alt ön kasa", "İç raf sol", "İç üst raf ", "İç alt raf ", "Sağ kapak", "Sol kapak"
    };

    private string[] objeAdlari;
    private int gorevIndex = 0;
    private string secilenDolap;
    private bool cevapBekleniyor = true;

    private Vector2[] targetPositionsGri;
    private Vector2[] targetPositionsKahve;

    private int score = 0;

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

            targetPositionsGri = new Vector2[]
            {
                new Vector2(5.56f, 0.21f),
                new Vector2(6.72f, 0.16f),
                new Vector2(4.42f, 0.12f),
                new Vector2(5.67f, 1.45f),
                new Vector2(5.64f, -1.1f),
                new Vector2(5.84f, -1.33f),
                new Vector2(5.93f, -0.58f),
                new Vector2(6.4f, -0.06f),
                new Vector2(6.33f, -0.58f),
                new Vector2(7.24f, 0.11f),
                new Vector2(4.97f, 0.14f)
            };
        }
        else if (secilenDolap == "kahve")
        {
            objeAdlari = new string[]
            {
                "KahveArka", "KahveSag", "KahveSol", "KahveUst", "KahveAlt",
                "KahveAltOn", "KahveRafSol", "KahveRaf1", "KahveRaf2", "KahveKapakSag", "KahveKapakSol"
            };

            targetPositionsKahve = new Vector2[]
            {
                new Vector2(5.56f, 0.21f),
                new Vector2(6.72f, 0.16f),
                new Vector2(4.42f, 0.12f),
                new Vector2(5.67f, 1.45f),
                new Vector2(5.64f, -1.1f),
                new Vector2(5.84f, -1.33f),
                new Vector2(5.93f, -0.58f),
                new Vector2(6.4f, -0.06f),
                new Vector2(6.33f, -0.58f),
                new Vector2(7.24f, 0.11f),
                new Vector2(4.97f, 0.14f)
            };
        }

        GuncelleGorevMetni();
        bilgiText.text = "";
        SkoruGuncelle();
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

                    // Doğruysa 5 puan ekle
                    score += 5;
                    SkoruGuncelle();

                    MoveObjectToTarget(objeninAdi, gorevIndex);
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
                        
                        
                        Invoke("LoadNextScene", 5f);
                        
                    
                        
                    }
                }
                else
                {
                    bilgiText.text = "Tekrar dene.";
                    cevapBekleniyor = false;

                    // Yanlışsa 1 puan çıkar
                    score -= 1;
                    SkoruGuncelle();

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
    
    void LoadNextScene()
    {
        SceneManager.LoadScene("AFInfoAra");
    }

    void MoveObjectToTarget(string objectName, int index)
    {
        GameObject targetObject = GameObject.Find(objectName);
        Vector2 targetPosition = Vector2.zero;

        if (secilenDolap == "gri" && index < targetPositionsGri.Length)
        {
            targetPosition = targetPositionsGri[index];
        }
        else if (secilenDolap == "kahve" && index < targetPositionsKahve.Length)
        {
            targetPosition = targetPositionsKahve[index];
        }

        if (targetObject != null)
        {
            StartCoroutine(MoveToTargetCoroutine(targetObject, targetPosition));
        }
    }

    System.Collections.IEnumerator MoveToTargetCoroutine(GameObject targetObject, Vector2 targetPosition)
    {
        while (Vector2.Distance(targetObject.transform.position, targetPosition) > 0.05f)
        {
            targetObject.transform.position = Vector2.MoveTowards(targetObject.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        targetObject.transform.position = targetPosition;
    }

    void SkoruGuncelle()
    {
        scoreText.text = "Puan: " + score.ToString();
    }
}
