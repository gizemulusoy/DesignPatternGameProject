using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public Transform leyla;              // Leyla karakteri
    public Transform mudur;              // Müdür karakteri
    public TMP_Text questText;           // TextMeshPro Text objesi
    public float mesafeLimit = 3f;       // Ne kadar yaklaşınca yazı değişecek?
    public GameObject questWorldSprite;

    private bool mudureYaklasti = false;

    void Start()
    {
        questText.text = "Hadi görevi öğrenmek için müdürün yanına git";
        questWorldSprite.SetActive(false);
    }

    void Update()
    {
        float mesafe = Vector2.Distance(leyla.position, mudur.position);

        if (!mudureYaklasti && mesafe <= mesafeLimit)
        {
            mudureYaklasti = true;
            questText.text = "hasNext() Leyla depoda getirecek makarna var mı kontrol et";
            questWorldSprite.SetActive(true);
        }
    }
}