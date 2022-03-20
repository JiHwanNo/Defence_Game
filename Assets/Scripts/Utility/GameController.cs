using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [HideInInspector] public UI_Base m_CrruntUI;
    public List<UI_Base> m_uiRootInfo = new List<UI_Base>();

    private ObjectPool _objpool;

    public ObjectPool ObjPool { get { return Instance._objpool; } }

    private void Awake()
    {
        Object.DontDestroyOnLoad(transform);

        _objpool = new ObjectPool();

        Screen.fullScreen = true;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        
    }

    private void Start()
    {
        StartCoroutine(StartProcess());
    }

    IEnumerator StartProcess()
    {
        _objpool.Init();
        yield return null;

        //Update_GameState(GAMESTATE.MainPage);
        yield return null;
    }
    
    public void Update_GameState(GAMESTATE gamestate)   // UI 바꿔주는 함수
    {
        if (m_CrruntUI != null)
        {
            if (m_CrruntUI.m_GameState != gamestate)
                m_CrruntUI.EndUI();
            else
                return;
        }

        for (int i = 0, len = m_uiRootInfo.Count; i < len; i++)
        {
            if (m_uiRootInfo[i] == null) continue;
            if (m_uiRootInfo[i].m_GameState == gamestate)
            {
                m_uiRootInfo[i].gameObject.SetActive(true);
                m_CrruntUI = m_uiRootInfo[i];
            }
            else
                m_uiRootInfo[i].gameObject.SetActive(false);
        }

        m_CrruntUI.StartUI();
    }

    static string LanguageCode // 사용자 os언어를 받아 적용하는 코드
    {
        get
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.ChineseSimplified: return "zh_CN";
                case SystemLanguage.ChineseTraditional: return "zh_TW";
                case SystemLanguage.Korean: return "ko";
                case SystemLanguage.Japanese: return "ja";
                case SystemLanguage.French: return "fr";
                case SystemLanguage.German: return "de";
                default: return "en";
            }
        }
    }

}
