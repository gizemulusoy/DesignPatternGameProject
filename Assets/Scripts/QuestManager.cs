using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using System.Collections;

public class QuestManager : MonoBehaviour
{
    [FormerlySerializedAs("leyla")] [Header("Karakter ve Objeler")]
    public Transform leylaF;
    public Transform mudur;
    [FormerlySerializedAs("currentItem")] public Transform cansuCI;
    public Transform urunTasimaNoktasi;
    public Transform targetPlace;
    public Transform namıkN;
    public Transform nextControlArea;

    [Header("Görev ve UI")]
    public TMP_Text questText;
    public Button changeQuestButton;
    public Button nextButton;
    public TMP_Text nextButtonText;

    [Header("Katmanlar")]
    public LayerMask urunLayerMask;
    public LayerMask hedefAlan;

    [Header("Ayarlar")]
    public float mesafeLimit = 3f;
    public float namikMesafeLimit = 2f;

    private GameObject tasinanUrun = null;
    private bool butonaBasildi = false;
    private bool leylaMudurYakininda = false;
    private bool cansuMudurYakininda = false;
    private bool namikKontrolAktif = false;
    private bool namikCurrentItemYakininda = false;
    private bool checkedButonBasilmis = false;

    void Start()
    {
        questText.text = "İlk görev için uygun karakteri seç ve müdürün yanına git";
        changeQuestButton.onClick.AddListener(OnQuestButtonClick);
        nextButton.onClick.AddListener(OnNextButtonClick);
        nextButton.gameObject.SetActive(false);
    }

    void Update()
    {
        KarakterMesafeKontrolleri();
        TiklamaIslemleri();
        NamikHareketKontrol();
    }

    void KarakterMesafeKontrolleri()
    {
        float mesafeLeyla = Vector3.Distance(leylaF.position, mudur.position);
        float mesafeCansu = Vector3.Distance(leylaF.position, cansuCI.position);
        float mesafeNamikCurrentItem = Vector3.Distance(namıkN.position, cansuCI.position);

        if (mesafeLeyla <= mesafeLimit && !leylaMudurYakininda)
        {
            leylaMudurYakininda = true;
            if (!butonaBasildi) questText.text = "First() Leyla depoda getirecek ilk deterjan nerede? Kontrol için depoya gitmelisin.";
        }

        if (butonaBasildi && mesafeCansu <= mesafeLimit && !cansuMudurYakininda)
        {
            cansuMudurYakininda = true;
            questText.text = "Deterjan 4E'de. Ürünü depodan alıp reyondaki yerine koymalısın";
        }

        if (namikKontrolAktif && mesafeNamikCurrentItem <= namikMesafeLimit && !namikCurrentItemYakininda)
        {
            namikCurrentItemYakininda = true;
            if (checkedButonBasilmis)
            {
                questText.text = "Sıradaki ürünün yeri 4F.";
            }
        }
    }

    void TiklamaIslemleri()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (tasinanUrun == null)
            {
                RaycastHit2D urunHit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, urunLayerMask);
                if (urunHit.collider != null && urunHit.collider.CompareTag("Urun"))
                {
                    tasinanUrun = urunHit.collider.gameObject;
                    tasinanUrun.transform.SetParent(urunTasimaNoktasi);
                    tasinanUrun.transform.position = cansuCI.position + new Vector3(0.5f, 0.1f, 0);
                    questText.text = "Ürün başarıyla alındı.";
                }
            }
            else
            {
                RaycastHit2D hedefHit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, hedefAlan);
                if (hedefHit.collider != null && hedefHit.collider.CompareTag("HedefAlan"))
                {
                    tasinanUrun.transform.SetParent(targetPlace);
                    tasinanUrun.transform.position = targetPlace.position;
                    tasinanUrun = null;
                    questText.text = "Görev tamamlandı. Kontrol için Namık depoya gitmeli.";
                    namikKontrolAktif = true;
                }
            }
        }
    }

    void NamikHareketKontrol()
    {
        if (namikKontrolAktif)
        {
            float mesafeNamık = Vector3.Distance(namıkN.position, nextControlArea.position);
            if (mesafeNamık <= namikMesafeLimit)
            {
                nextButton.gameObject.SetActive(true);
                questText.text = "Namık kontrol alanında. Kontrol et butonuna tıkla.";
            }
        }
    }

    void OnNextButtonClick()
    {
        nextButtonText.text = "Checked";
        checkedButonBasilmis = true;
        StartCoroutine(DisableButtonAfterDelay());
    }

    IEnumerator DisableButtonAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        nextButton.gameObject.SetActive(false);
        nextButtonText.text = "Check";
        namikKontrolAktif = false;
        namikCurrentItemYakininda = false;
    }

    void OnQuestButtonClick()
    {
        butonaBasildi = true;
        questText.text = "Şimdi ilk ürünün yerini current item Cansu'ya söylemelisin.";
    }
}