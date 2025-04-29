using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuestManager : MonoBehaviour
{
    [Header("Karakter ve Objeler")]
    public Transform leyla;
    public Transform mudur;
    public Transform currentItem;
    public Transform urunTasimaNoktasi;
    public Transform targetPlace;

    [Header("Görev ve UI")]
    public TMP_Text questText;
    public Button changeQuestButton;

    [Header("Katmanlar")]
    public LayerMask urunLayerMask;
    public LayerMask hedefAlan;

    [Header("Ayarlar")]
    public float mesafeLimit = 3f;

    private GameObject tasinanUrun = null;
    private int mudurYaklasmaSayisi = 0;
    private bool butonaBasildi = false;
    private bool leylaMudurYakininda = false;
    private bool currentItemMudurYakininda = false;

    void Start()
    {
        questText.text = "İlk görev için uygun karakteri seç ve müdürün yanına git";
        changeQuestButton.onClick.AddListener(OnQuestButtonClick);
    }

    void Update()
    {
        KarakterMesafeKontrolleri();
        TiklamaIslemleri();
    }

    void KarakterMesafeKontrolleri()
    {
        float mesafeLeyla = Vector2.Distance(leyla.position, mudur.position);
        float mesafeCurrentItem = Vector2.Distance(currentItem.position, mudur.position);

        // LEYLA müdüre yaklaşırsa
        if (mesafeLeyla <= mesafeLimit && !leylaMudurYakininda)
        {
            mudurYaklasmaSayisi++;
            leylaMudurYakininda = true;

            if (mudurYaklasmaSayisi == 1)
            {
                questText.text = "First() Leyla depoda getirecek ilk deterjan nerede? Kontrol için depoya gitmelisin.";
            }
            else if (mudurYaklasmaSayisi == 2 && butonaBasildi)
            {
                questText.text = "İlk deterjanın yeri 4A.";
                Invoke(nameof(ChangeQuestAfterDelay), 3f);
            }
        }
        else if (mesafeLeyla > mesafeLimit && leylaMudurYakininda)
        {
            leylaMudurYakininda = false;
        }

        // CURRENT ITEM müdüre yaklaşırsa
        if (mesafeCurrentItem <= mesafeLimit && !currentItemMudurYakininda)
        {
            currentItemMudurYakininda = true;
            questText.text = "Sen 4A'daki deterjanı getirmeli ve reyondaki yerine koymalısın.";
        }
        else if (mesafeCurrentItem > mesafeLimit && currentItemMudurYakininda)
        {
            currentItemMudurYakininda = false;
        }
    }

    void TiklamaIslemleri()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 1. Ürün seçme
            if (tasinanUrun == null)
            {
                RaycastHit2D urunHit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, urunLayerMask);
                if (urunHit.collider != null && urunHit.collider.CompareTag("Urun"))
                {
                    tasinanUrun = urunHit.collider.gameObject;

                    Rigidbody2D rb = tasinanUrun.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.isKinematic = true;
                        rb.gravityScale = 0;
                    }

                    tasinanUrun.transform.SetParent(urunTasimaNoktasi); // Child yapma
                    tasinanUrun.transform.position = currentItem.position + new Vector3(0.5f, 0.1f, 0); // SABİT KALDI

                    questText.text = "Ürün başarıyla alındı.";
                }
            }
            else
            {
                // 2. Ürün bırakma (Hedef alana tıklama)
                RaycastHit2D hedefHit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, hedefAlan);
                if (hedefHit.collider != null && hedefHit.collider.CompareTag("HedefAlan"))
                {
                    tasinanUrun.transform.SetParent(null);

                    Rigidbody2D rb = tasinanUrun.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.isKinematic = false;
                        rb.gravityScale = 1;
                    }
                    
                    tasinanUrun.transform.SetParent(targetPlace);
                    tasinanUrun.transform.position = targetPlace.position + new Vector3(0f, 0f, 0);

                    
                    //tasinanUrun = null;
                    questText.text = "Görevi tamamladın, şimdi müdürün yanına dönmelisin.";
                }
            }
        }
    }

   /* private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HedefAlan") && tasinanUrun != null)
        {
            tasinanUrun.transform.SetParent(null);

            Rigidbody rb = tasinanUrun.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }

            tasinanUrun = null;
            questText.text = "Görevi tamamladın, şimdi müdürün yanına dönmelisin.";
        }
    }*/

    void OnQuestButtonClick()
    {
        butonaBasildi = true;
    }

    void ChangeQuestAfterDelay()
    {
        questText.text = "Current item yanıma gelsin.";
    }
}
