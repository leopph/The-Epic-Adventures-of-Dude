using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections.Generic;
using TMPro;




public class AchievementListing : MonoBehaviour
{
    public GameObject m_EntryPrototype;
    private Color m_UnlockedColor = new Color(155f / 255f, 80f / 255f, 150f / 255f, 1);
    private Color m_LockedColor = new Color(75f/255f, 75f/255f, 75f/255f, 1);


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

            entry.GetComponent<Image>().color = PlayerPrefs.HasKey(AchievementManager.prefix + ach.Key) && PlayerPrefs.GetInt(AchievementManager.prefix + ach.Key) == 1 ? m_UnlockedColor : m_LockedColor;

            RectTransform rect = entry.GetComponent<RectTransform>();
            rect.anchoredPosition += new Vector2(0, -count * rect.rect.height);
            count++;

            GetComponent<RectTransform>().sizeDelta += new Vector2(0, rect.rect.height);
        }
    }
}
