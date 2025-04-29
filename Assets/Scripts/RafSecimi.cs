using UnityEngine;
using TMPro;

public class RafSecimi : MonoBehaviour
{
    public TMP_Text gorevText;
    public TMP_Text bilgiText;
    public float moveSpeed = 5f;

    private string[] gorevIsimleri = new string[]
    {
        "Arka kasa ", "Sağ kasa", "Sol kasa", "Üst raf", "Alt raf",
        "Alt ön raf", "İç raf sol", "İç üst raf ", "İç alt raf ", "Sağ kapak", "Sol kapak"
    };

    private string[] objeAdlari;
    private int gorevIndex = 0;
    private string secilenDolap;
    private bool cevapBekleniyor = true;

    private Vector2[] targetPositionsGri;
    private Vector2[] targetPositionsKahve;

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
                new Vector2(5.56f, 0.21f), //gri arka
                new Vector2(6.72f, 0.16f), //gri sa9
                new Vector2(4.42f, 0.12f), //gri sol
                new Vector2(5.67f, 1.45f), //gri üst
                new Vector2(5.64f, -1.1f), //gri alt 
                new Vector2(5.84f, -1.33f), //gri alt ön
                new Vector2(5.93f, -0.58f), //gri sol raf
                new Vector2(6.4f, -0.06f), //gri raf 1  
                new Vector2(6.33f, -0.58f), //gri raf 2
                new Vector2(7.24f, 0.11f), // gri sağ kapak
                new Vector2(4.97f, 0.14f) //gri sol kapak
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
                new Vector2(-6.019217f, 0.749076f), //kahve arka
                new Vector2(-4.88f, 0.6629671f), //kahve sağ
                new Vector2(-7.14f, 0.61f), //kahve sol
                new Vector2(-6, 1.97f), //kahve üst
                new Vector2(-5.87f, -0.55f), //kahve alt
                new Vector2(-5.73f, -0.76f), //kahve alt ön
                new Vector2(-5.7f, -0.04f), //kahve sol raf
                new Vector2(-5.24f, 0.49f), //kahve raf 1
                new Vector2(-5.3f, -0.04f), //kahve raf2
                new Vector2(-4.33f, 0.62f), //kahve sağ kapak
                new Vector2(-6.59f, 0.72f) //kahve sol kapak
            };
        }

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

                    // Objeyi hareket ettir
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
            // Objeyi başlangıçta hareket ettirmiyoruz
        }
    }

    void BilgiMetniTemizle()
    {
        bilgiText.text = "";
        cevapBekleniyor = true;
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
}