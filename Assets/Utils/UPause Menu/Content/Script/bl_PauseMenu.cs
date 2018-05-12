using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using XboxCtrlrInput;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
 

public class bl_PauseMenu : MonoBehaviour {

    public static PauseState m_PauseState = PauseState.None;
    public bool onlyOption = false;
    /// <summary>
    /// Global var for know is pause game
    /// </summary>
    public static bool m_Pause = false;
    [Header("Pause Main")]
    public bool useTimeScale = true;
    public bool LookCursor = true;
    public GameObject PauseUI = null;
    public string m_PauseShowAnim = "PauseMenuShow";
    public string m_PauseHideAnim = "PauseMenuHide";
    public string m_PauseMovedHideAnim = "PauseMenuMovedHide";
    public string m_PauseMoveAnim = "PauseMenuToLeft";
    public string m_PauseMoveReturnAnim = "PauseMenuToCenter";
    [Space(5)]
    [Header("Pause Options")]
    public GameObject OptionsUI = null;
    public string OptionsHideAnim = "OptionsHide";
    [Space(5)]
    [Header("Pause Credits")]
    public GameObject CreditsUI = null;
    public string CreditsHideAnim = "CreditsHide";
    [Space(5)]
    public CanvasGroup Overlay = null;
    [Range(0.0f,1.0f)]
    public float MaxAlpha = 0.75f;
    //private 
    private bool isMoved = false;

    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        if (PauseUI != null)
        {
            PauseUI.SetActive(false);
        }
        if (OptionsUI != null)
        {
            OptionsUI.SetActive(false);
        }
        if (CreditsUI != null)
        {
            CreditsUI.SetActive(false);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        #if UNITY_EDITOR
            LookCursor = false;
        #endif
        if (Overlay)
            Overlay.gameObject.SetActive(true);
        if (LookCursor)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    bool initied = false;
    void Update()
    {
        if (initied) {
            if (!onlyOption &&  Input.GetKeyDown(KeyCode.Escape))
            {
                DoPause();
            }
            if (!onlyOption &&  XCI.GetButtonDown(XboxButton.B))
            {
                if (m_PauseState == PauseState.Main)
                    DoPause();
                else if (m_Pause == true)
                    DoMain();
            }
            if (Input.GetKeyDown(KeyCode.Escape) || XCI.GetButtonDown(XboxButton.B) || XCI.GetButtonDown(XboxButton.Start)) {
                if (m_Pause) 
                    DoPause();
            }
        } else {
            initied = !Input.GetKey(KeyCode.Escape) && !XCI.GetButton(XboxButton.B) && !XCI.GetButton(XboxButton.Start);
        }
        
        //Fade effect
        if (Overlay != null)
        {
            if (m_Pause && Overlay.alpha < MaxAlpha)
            {
                Overlay.alpha = Mathf.Lerp(Overlay.alpha, MaxAlpha, Time.unscaledDeltaTime * 5);
            }
            else if (Overlay.alpha > 0.0f)
            {
                Overlay.alpha = Mathf.Lerp(Overlay.alpha, 0.0f, Time.unscaledDeltaTime * 5);
            }
        }
        if (m_Pause)
            Time.timeScale = 0;
        
    }
    /// <summary>
    /// 
    /// </summary>
    public void DoPause()
    {
        initied = false;
        if (PauseUI != null)
        {
            //True or False
            m_Pause = !m_Pause;
            if (m_Pause)
            {
                //Active Pause UI with animation
                PauseUI.SetActive(true);
                if (!onlyOption)
                    PauseUI.GetComponent<Animator>().Play(m_PauseShowAnim,0,0);
                else
                    OptionsUI.SetActive(true);
                m_PauseState = PauseState.Main;
                if (LookCursor)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                if (useTimeScale)
                {
                    Time.timeScale = 0;
                }
            }
            else
            {
                if (useTimeScale)
                {
                    Time.timeScale = 1;
                }
                if (LookCursor)
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
                //This animation content a event for auto desactive
                //when animation finished
                if (isMoved)
                {
                    PauseUI.GetComponent<Animator>().Play(m_PauseMovedHideAnim, 0, 0);
                }
                else if (!onlyOption)
                {
                    PauseUI.GetComponent<Animator>().Play(m_PauseHideAnim, 0, 0);
                } else {
                    OptionsUI.SetActive(false);
                    var menu = GameObject.FindGameObjectWithTag("Menu");
                    menu.SetActive(false);
                    menu.SetActive(true);
                }
                //If options active, then hide too
                if (OptionsUI.activeSelf)
                {
                    OptionsUI.GetComponent<Animator>().SetBool("show", false);
                }
                //If you do not want to disable animation for event
                //use this:
                //StartCoroutine(DesactiveInTime(PauseUI,2f);
                if  (CreditsUI)
                    if (CreditsUI.activeSelf)
                    {
                        CreditsUI.GetComponent<Animator>().SetBool("show", false);
                    }

                m_PauseState = PauseState.None;
            }
        }else{
        
            Debug.LogError("Pause UI is Emty please add this in inspector");
        }
        isMoved = false;
    }
    /// <summary>
    /// 
    /// </summary>
    public void DoMain()
    {
        if (onlyOption) {
            DoPause();
            return;
        }
        
        if (OptionsUI.activeSelf)
        {
            OptionsUI.GetComponent<Animator>().SetBool("show", false);
        }
        if (CreditsUI)
        if (CreditsUI.activeSelf)
        {
            CreditsUI.GetComponent<Animator>().SetBool("show", false);
        }
        PauseUI.GetComponent<Animator>().Play(m_PauseMoveReturnAnim, 0, 0);
        PauseUI.GetComponent<Script_UIDefaultSelected>().selectDefault();
        isMoved = false;
        m_PauseState = PauseState.Main;

    }
    /// <summary>
    /// 
    /// </summary>
    public void DoOptions()
    {
        if (!OptionsUI.activeSelf )
        {
            if (CreditsUI.activeSelf)
            {
                CreditsUI.GetComponent<Animator>().SetBool("show", false);
            }
            //This animation have a event to call Options UI show
            if (!isMoved)
            {
                PauseUI.GetComponent<Animator>().Play(m_PauseMoveAnim, 0, 0);
            }
            else
            {
                OptionsUI.SetActive(true);
                OptionsUI.GetComponent<Animator>().SetBool("show", true);
            }            
            isMoved = true;
            //If you do not want to disable animation for event
            //use this:
            m_PauseState = PauseState.Options;
        }
        else
        {
            OptionsUI.GetComponent<Animator>().SetBool("show", false);
            PauseUI.GetComponent<Animator>().Play(m_PauseMoveReturnAnim, 0, 0);
            isMoved = false;
        }
    }

    public void DoMainMenu() {
        DoPause();
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// 
    /// </summary>
    public void DoCredits()
    {
        if (!CreditsUI.activeSelf )
        {
            
            if (OptionsUI.activeSelf)
            {
                OptionsUI.GetComponent<Animator>().SetBool("show", false);
            }
            //This animation have a event to call Options UI show
            if (!isMoved)
            {
                PauseUI.GetComponent<Animator>().Play(m_PauseMoveAnim, 0, 0);
            }
            else
            {
                CreditsUI.SetActive(true);
                CreditsUI.GetComponent<Animator>().SetBool("show", true);
            }
            isMoved = true;
            m_PauseState = PauseState.Credits;
        }
        else
        {
            CreditsUI.GetComponent<Animator>().SetBool("show", false);
            PauseUI.GetComponent<Animator>().Play(m_PauseMoveReturnAnim, 0, 0);
            isMoved = false;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
    /// <summary>
    /// Simple Restart Scene
    /// </summary>
    public void SimpleRestart()
    {
        //m_Pause = false;
        //m_PauseState = PauseState.None;
        DoPause();
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<Script_GameManager_PVP>().restart(false);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="go"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator DesactiveInTime(GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        go.SetActive(false);
    }
}