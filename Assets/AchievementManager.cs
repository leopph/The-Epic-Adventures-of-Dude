using UnityEngine;
using UnityEngine.Assertions;
using TMPro;
using System.Collections.Generic;




public class AchievementManager : MonoBehaviour
{
    private static AchievementManager m_Instance;
    public static AchievementManager instance => m_Instance;

    [SerializeField] private const string prefix = "ach_";

    private Dictionary<string, string> m_Achievements = new Dictionary<string, string>
    {
        { "So it begins...", "Kill an enemy" },
        { "Massacre", "Kill 6 enemies" },
        { "Congratulations", "Fall to your death." }
    };
    public Dictionary<string, string> Achievements => m_Achievements;

    [SerializeField] private Transform m_Popup;
    private Animator m_Animator;
    private TextMeshProUGUI m_Text;

    private int m_EnemiesKilled = 0;




    private void Awake()
    {
        m_Instance = this;

        foreach (string ach in m_Achievements.Keys)
            if (!PlayerPrefs.HasKey(prefix + ach))
                PlayerPrefs.SetInt(prefix + ach, 0);
    }


    private void Start()
    {
        if (m_Popup != null)
        {
            m_Animator = m_Popup.GetComponent<Animator>();
            m_Text = m_Popup.GetComponentsInChildren<TextMeshProUGUI>()[1];
        }

        EventSystem.current.onEnemyKilled += HandleEnemyKilled;
        EventSystem.current.onFallenToDeath += HandleFallenToDeath;
    }


    private void OnDestroy()
    {
        EventSystem.current.onEnemyKilled -= HandleEnemyKilled;
        EventSystem.current.onFallenToDeath -= HandleFallenToDeath;
    }




    private void AchievementAchieved(string name)
    {
        if (!m_Achievements.ContainsKey(name))
            Debug.LogError("NO ACHIEVEMENT WITH NAME \"" + name + "\"");

        if (PlayerPrefs.GetInt(prefix + name) != 0)
            return;

        m_Text.text = name;
        m_Animator.SetTrigger("Pop");
        PlayerPrefs.SetInt(prefix + name, 1);
    }


    private void HandleEnemyKilled()
    {
        if (++m_EnemiesKilled == 1)
            AchievementAchieved("So it begins...");
        else if (m_EnemiesKilled == 6)
            AchievementAchieved("Massacre");
    }

    private void HandleFallenToDeath() { AchievementAchieved("Congratulations"); }
}
