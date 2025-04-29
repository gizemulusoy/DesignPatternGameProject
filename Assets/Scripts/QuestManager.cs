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

    [Header("Görev ve UI")]
    public TMP_Text questText;
    public Button changeQuestButton;

    [Header("Katmanlar")]
    public LayerMask urunLayerMask;

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
        UrunSecVeTasi();
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

    void UrunSecVeTasi()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, urunLayerMask))
            {
                GameObject hedef = hit.collider.gameObject;

                if (hedef.CompareTag("Urun"))
                {
                    if (tasinanUrun == null)
                    {
                        tasinanUrun = hedef;

                        // Rigidbody ayarları
                        Rigidbody rb = tasinanUrun.GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            rb.isKinematic = true;
                            rb.useGravity = false;
                        }

                        // Taşıma işlemi
                        tasinanUrun.transform.SetParent(urunTasimaNoktasi);
                        tasinanUrun.transform.localPosition = Vector3.zero;

                        questText.text = "Ürün başarıyla alındı.";
                    }
                }
                else
                {
                    questText.text = "Yanlış ürün!";
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
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
    }

    void OnQuestButtonClick()
    {
        butonaBasildi = true;
    }

    void ChangeQuestAfterDelay()
    {
        questText.text = "Current item yanıma gelsin.";
    }
}
