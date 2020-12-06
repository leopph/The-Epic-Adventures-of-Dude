using UnityEngine;
using UnityEngine.Assertions;
using TMPro;
using System.Collections.Generic;




public class AchievementManager : MonoBehaviour
{
    private static AchievementManager m_Instance;

    [SerializeField] private const string prefix = "ach_";

    private Dictionary<string, string> m_Achievements = new Dictionary<string, string>();

    [SerializeField] private Transform m_Popup;
    private Animator m_Animator;
    private TextMeshProUGUI m_Text;

    private int m_EnemiesKilled = 0;




    private void Awake()
    {
        if (m_Instance != null)
        {
            if (m_Instance != this)
                Destroy(this);
        }
        else
        {
            m_Instance = this;
            DontDestroyOnLoad(this);

            m_Achievements.Add("So it begins...", "Kill an enemy");
            m_Achievements.Add("Massacre", "Kill 6 enemies");
            m_Achievements.Add("Congratulations", "Fall to your death.");

            foreach (string ach in m_Achievements.Keys)
                if (!PlayerPrefs.HasKey(prefix + ach))
                    PlayerPrefs.SetInt(prefix + ach, 0);

            EventSystem.current.onEnemyKilled += HandleEnemyKilled;
            EventSystem.current.onFallenToDeath += HandleFallenToDeath;
        }
    }


    private void Start()
    {
        Assert.IsNotNull(m_Popup);
        m_Animator = m_Popup.GetComponent<Animator>();
        m_Text = m_Popup.GetComponentsInChildren<TextMeshProUGUI>()[1];
    }


    private void OnDestroy()
    {
        EventSystem.current.onEnemyKilled -= HandleEnemyKilled;
        EventSystem.current.onFallenToDeath -= HandleFallenToDeath;
    }




    public void AchievementAchieved(string name)
    {
        if (!m_Achievements.ContainsKey(name))
            Debug.LogError("NO ACHIEVEMENT WITH NAME \"" + name + "\"");

        if (PlayerPrefs.GetInt(prefix + name) != 0)
            return;

        m_Text.text = name;
        m_Animator.SetTrigger("Pop");
        PlayerPrefs.SetInt(prefix + name, 1);
    }


    public void HandleEnemyKilled()
    {
        if (++m_EnemiesKilled == 1)
            AchievementAchieved("So it begins...");
        else if (m_EnemiesKilled == 6)
            AchievementAchieved("Massacre");
    }

    public void HandleFallenToDeath() { AchievementAchieved("Congratulations"); }
}
