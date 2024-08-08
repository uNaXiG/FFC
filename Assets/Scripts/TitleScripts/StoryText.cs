using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryText : MonoBehaviour
{
    public Text showText;   // Text UI
    float textSpeed = 0.05f;    // show text speed
    string tempText;    // this text's temp
    int TextCount = 0;  //對話句數
    public GameObject ShowTitleName;   // title name
    public static bool isReadFinished = false;
    public GameObject canvas;
    public AudioSource BGM;

    // Text Animation //
    IEnumerator TextAnimation()
    {
        //Split each char into a char array
        foreach (char letter in tempText.ToCharArray())
        {
            //Add 1 letter each
            showText.text += letter;
            yield return 0;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    // BGM fade out //
    IEnumerator BgmFadeOut()
    {
        ShowTitleName.SetActive(true);
        int fadeTime = 80;
        while(fadeTime > 0)
        {
            yield return new WaitForSeconds(0.1f);
            fadeTime--;
            BGM.volume -= 0.01f;
        }        
    }

    // wait 3 second //
    IEnumerator WaitFor5Seconds()
    {
        int waitTime = 5;
        while(waitTime > 0)
        {
            yield return new WaitForSeconds(1f);
            waitTime--;
        }
    }
    
    private void Start()
    {
        StartCoroutine(WaitFor5Seconds());
        NextText();
        TextCount++;
    }
    private void Update()
    {
        if (!isReadFinished)
        {
            if ((showText.text == tempText) && (Input.anyKey || Input.GetMouseButtonDown(0)))
            {
                if (TextCount >= 12)
                {                    
                    isReadFinished = true;
                    showText.text = "";
                    StartCoroutine(BgmFadeOut());
                }

                else
                {
                    showText.text = "";
                    tempText = "";
                    NextText();
                    TextCount++;
                }
            }

        }
    }

    void NextText()
    {
        switch (TextCount)
        {
            case 0:
                tempText = "\t\t\t\t\t\t\t\t\t\t\t\t\t\n距今大約一百萬年前\n\n世間還是一片荒蕪\n\n沒有任何生命\n\n\n\n呈現出的是一幅名為絕望的畫" +
                    "\n\n\n\n\n\n\n\n< 按下任意鍵繼續 >";
                showText.text = "";
                break;
            case 1:
                tempText = "直到某日\n\n人類的王誕生了\n\n開始了繁榮的人界\n\n\n\n但最後人類因為慾望與貪婪而引發了人類間的戰爭" +
                    "\n\n\n\n\n\n\n\n< 按下任意鍵繼續 >";
                showText.text = "";
                break;
            case 2:
                tempText = "使得世界再度陷入黑暗與寂寥\n\n過了數萬年\n\n\n\n眾神祈為了拯救世界而來到人界\n\n並且使世間再度有了生機與希望" +
                    "\n\n\n\n\n\n\n\n< 按下任意鍵繼續 >";
                showText.text = "";
                break;
            case 3:
                tempText = "然而和平是短暫的\n\n隨著神祈的降臨\n\n黑暗中的魔鬼也在蠢蠢欲動\n\n他們覬覦著這個世界" +
                    "\n\n\n\n\n\n\n\n< 按下任意鍵繼續 >";
                showText.text = "";
                break;
            case 4:
                tempText = "充滿野心的魔族便向人界發動了掠奪戰\n\n目的是將這美麗的世界據為己有\n\n他們不顧一切\n\n用盡任何手段" +
                    "\n\n\n\n\n\n\n\n< 按下任意鍵繼續 >";
                showText.text = "";
                break;
            case 5:
                tempText = "貪欲纏身的人族\n\n\n" +
                    "聖明的神族\n\n\n" +
                    "邪惡卻無比強大的魔族\n\n\n" +
                    "便展開了約莫一百萬年的戰役" +
                    "\n\n\n\n\n\n\n\n< 按下任意鍵繼續 >";
                showText.text = "";
                break;
            case 6:
                tempText = "戰役持續期間史稱\"渾沌時期\"" +
                    "\n\n\n\n然而這段時期的所有秘密都被記錄於一本名為流火之章的書中" +
                    "\n\n\n\n\n\n\n\n< 按下任意鍵繼續 >";
                showText.text = "";
                break;
            case 8:
                tempText = "而關於這本書也流傳了眾多傳說" +
                    "\n\n(一)書中紀載了戰爭的所有秘密" +
                    "\n\n\n(二)書中說明了三界間的所有大小衝突以及各種事件" +
                    "\n\n\n(三)書的作者是造物主" +
                    "\n\n\n\n\n\n以及" +
                    "\n\n\n最重要也最恐怖的一點......" +
                    "\n\n\n\n< 按下任意鍵繼續 >";
                showText.text = "";
                break;
            case 9:
                tempText = "\n\n(四)書中記錄並詳細說明了支配世界的真正秘術" +
                    "\n\n\n\n\n\n\n\n< 按下任意鍵繼續 >";
                showText.text = "";
                break;
            case 10:
                tempText = "也因此吸引了大量的魔族以及貪婪的人類找尋" +
                    "\n\n使得世間再度陷入了第二個混沌時期" +
                    "\n\n稱為\"黑暗時期\"\n\n\n\n\n\n\n\n< 按下任意鍵繼續 >";
                showText.text = "";
                break;
            case 11:
                tempText = "然而人族當中扮演主角的你" +
                    "\n\n為了挽回美麗的世界而踏上了這條正義卻坎坷的道路" +
                    "\n\n\n\n\n\n\n\n< 按下任意鍵繼續 >";
                showText.text = "";
                break;
            case 12:
                tempText = "遊戲即將開始\n\n\n\n\n\n\n\n< 按下任意鍵繼續 >";
                showText.text = "";
                break;
            default:
                tempText = "";
                showText.text = "";
                break;
        }
        StartCoroutine(TextAnimation());
    }
}
