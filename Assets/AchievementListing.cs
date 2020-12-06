using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using TMPro;



public class AchievementListing : MonoBehaviour
{
    public GameObject m_EntryPrototype;




    private void Start()
    {
        Assert.IsNotNull(m_EntryPrototype);

        int count = 0;
        foreach (KeyValuePair<string, string> ach in AchievementManager.instance.Achievements)
        {
            GameObject entry = Instantiate(m_EntryPrototype);

            entry.transform.SetParent(transform, false);
            entry.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ach.Key;
            entry.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ach.Value;
            RectTransform rect = entry.GetComponent<RectTransform>();
            rect.anchoredPosition += new Vector2(0, -count * rect.rect.height);
            count++;

            GetComponent<RectTransform>().sizeDelta += new Vector2(0, rect.rect.height);
        }
    }
}
