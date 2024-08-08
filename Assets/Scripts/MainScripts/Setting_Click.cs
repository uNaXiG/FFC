using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Setting_Click : MonoBehaviour
{
    private Rect windowPosition;    // set "swtting windw" position
    private Rect continueButtonPos; // set "繼續"
    private Rect logoutButtonPos;   // set "登出"
    private Rect backTitleButtonPos;    // set "回到標題"
    public AudioSource clickSE;

    bool showWimdow = false;

    float windowX = .0f, windowY = .0f;
    float windowWidth = 200f, windowHeight = 160f;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    /*
    private void Update()
    {
        if (showWimdow)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if ((Input.mousePosition.x > 0 && Input.mousePosition.x < windowX)
                          || (Input.mousePosition.x < 960 && Input.mousePosition.x > windowX + windowWidth)
                          || (Input.mousePosition.y < 540 && Input.mousePosition.y > windowY + windowHeight)
                          || (Input.mousePosition.y > 0 && Input.mousePosition.y < windowY))
                {
                    showWimdow = false;
                }
            }
        }
    }*/

    private void OnClick()
    {
        if (showWimdow)
        {
            return;
        }            
        setWindowPos();
        setContinueButtonPos();
        setLogoutButtonPos();
        setBackButtonPos();
        clickSE.Play();
        showWimdow = true;        
    }
    
    private void setBackButtonPos()
    {
        float buttonWidth = 100f;//按鈕的寬度
        float buttonHeight = 30f;//按鈕的高度
        float buttonLeft = windowPosition.width * 0.5f - buttonWidth * 0.5f;//按鈕和window左邊的距離，目前的值會讓button顯示在window的正中央
        float buttonTop = windowPosition.height - buttonHeight - 100;//按鈕和window上面的距離，目前的值會讓button顯示在window的正中央

        backTitleButtonPos = new Rect(buttonLeft, buttonTop, buttonWidth, buttonHeight);

    }

    private void setLogoutButtonPos()
    {
        float buttonWidth = 100f;//按鈕的寬度
        float buttonHeight = 30f;//按鈕的高度
        float buttonLeft = windowPosition.width * 0.5f - buttonWidth * 0.5f;//按鈕和window左邊的距離，目前的值會讓button顯示在window的正中央
        float buttonTop = windowPosition.height - buttonHeight - 60;//按鈕和window上面的距離，目前的值會讓button顯示在window的正中央

        logoutButtonPos = new Rect(buttonLeft, buttonTop, buttonWidth, buttonHeight);
    }

    private void setContinueButtonPos()
    {
        float buttonWidth = 100f;//按鈕的寬度
        float buttonHeight = 30f;//按鈕的高度
        float buttonLeft = windowPosition.width * 0.5f - buttonWidth * 0.5f;//按鈕和window左邊的距離，目前的值會讓button顯示在window的正中央
        float buttonTop = windowPosition.height - buttonHeight - 20;//按鈕和window上面的距離，目前的值會讓button顯示在window的正中央

        continueButtonPos = new Rect(buttonLeft, buttonTop, buttonWidth, buttonHeight);
    }
    
    private void setWindowPos()
    {
        windowX = 1280 - windowWidth;
        windowY = 1024 - 73 - windowHeight;

        windowPosition = new Rect(windowX, windowY, windowWidth, windowHeight);// 將不可被拖曳的window設定在Game左上角
    }
    private void OnGUI()
    {
        if (showWimdow)
        {
            GUI.Window(0, windowPosition, windowEvent, "遊 戲 設 定");
        }         
    }

    private void windowEvent(int id)//處理視窗裡面要顯示的文字、按鈕、事件處理。必須要有一個為int的傳入參數
    {
        if (GUI.Button(logoutButtonPos, "登 出"))
        {
            Debug.Log("log out");
            StartCoroutine(waitforLogout());
            showWimdow = false;
        }
        if (GUI.Button(continueButtonPos, "繼 續"))//在window上顯示按鈕
        {
            showWimdow = false;
            clickSE.Play();
        }
        if(GUI.Button(backTitleButtonPos, "回到標題"))
        {
            showWimdow = false;
            LoadMainScene.isFristLoad = false;
            StartCoroutine(waitforFadeOut());
        }
    }
    
    public GameObject FadeOut;
    GameObject tempFadeOut;
    IEnumerator waitforFadeOut()
    {
        clickSE.Play();
        yield return 0;
        tempFadeOut = Instantiate(FadeOut);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }
    IEnumerator waitforLogout()
    {
        clickSE.Play();
        yield return new WaitForSeconds(0.25f);
        Application.Quit();
    }

    IEnumerator waitGoBack()
    {
        clickSE.Play();
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene("TitleScene");
    }

}