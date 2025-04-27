using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public Transform leyla;
    public Transform mudur;
    public Transform currentItem; // yeni karakter

    public TMP_Text questText;
    public float mesafeLimit = 3f;

    public Button changeQuestButton;

    private int mudurYaklasmaSayisi = 0;
    private bool butonaBasildi = false;
    private bool leylaMudurYakininda = false;
    private bool currentItemMudurYakininda = false; // currentItem için de kontrol

    void Start()
    {
        questText.text = "İlk görev için uygun karakteri seç ve müdürün yanına git";
        changeQuestButton.onClick.AddListener(OnQuestButtonClick);
    }

    void Update()
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

    void OnQuestButtonClick()
    {
        butonaBasildi = true;
    }

    void ChangeQuestAfterDelay()
    {
        questText.text = "Current item yanıma gelsin.";
    }
} 