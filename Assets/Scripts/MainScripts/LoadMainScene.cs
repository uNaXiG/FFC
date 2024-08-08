using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMainScene : MonoBehaviour
{
    public Text showText;   // Text UI
    public Text showKingText;   // Text for king
    public GameObject King;     // King img
    public GameObject ClickCanvas;  
    public AudioSource clickSE; 
    
    public static bool isFristLoad = true;    // is Frist Load Scene?
    public GameObject fadeIn;   // Load Scene fadeIn Prefab
    public GameObject[] btn_Actor = new GameObject[3];    // Set data finished to set actor img

    public GameObject SetActorData;     // Set Data
    public Dropdown list;   // drop down list
    public Button checkBtn; // send the data
    public InputField SetName;  // Set Actor name's input
    public GameObject InputIsNull;     // input text is null
    public GameObject InputIsNull_pwd;     // input text is null
    public InputField SetPwd;   // Set Passwd

    public GameObject ShowLog;
    public GameObject dialogCanvas;
    GameObject tempFadeIn;  // temp fade in prefab
    float textSpeed = 0.01f;    // show text speed
    string tempText;    // this text's temp
    
    // Fade in //
    IEnumerator FadeIn()
    {
        tempFadeIn = Instantiate(fadeIn);
        yield return new WaitForSeconds(1);
        Destroy(tempFadeIn);
    }


    bool isActorSay = true;
    bool isKingSay = false;
    // Text Animation //
    IEnumerator TextAnimation()
    {        
        //Split each char into a char array
        foreach (char letter in tempText.ToCharArray())
        {
            if (!isDialogFininshed)
            {
                if (isActorSay)
                    showText.text += letter;
                else if (isKingSay)
                    showKingText.text += letter;
            }
            yield return 0;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
        if (!isFristLoad)
        {
            btn_Actor[ActorAbility.TypeIndex].SetActive(true);
            ShowLog.SetActive(true);
            ShowLog.GetComponent<Text>().text = "點擊人物可以查看屬性\n請點下方的戰鬥圖示進入副本";
            Destroy(dialogCanvas);
            Destroy(ClickCanvas);
        }
        else
        {
            ActorAbility.SetActorType(0);
            showText.text = "";
            tempText = "現在選擇的是\n<<劍士>>\n使用劍為武器的戰鬥職業，\n通常在戰鬥中，較均衡的能力都使得他在各方面都能表現傑出。\n\n\n" +
                "體力\n★★★★★\n攻擊\n★★★☆☆\n爆擊\n★☆☆☆☆\n生存力\n★★★★☆";
            StartCoroutine(TextAnimation());
            SetActorData.SetActive(true);
            InputIsNull.SetActive(false);
            InputIsNull_pwd.SetActive(false);
            SetName.onValueChanged.AddListener(isInputing);
            SetPwd.onValueChanged.AddListener(isInputing_pwd);
            checkBtn.onClick.AddListener(Check);
            AddItems();
            isFristLoad = false;
        }        
    }

    bool isRuningText = false;
    bool isSelectedFinish = false;
    int dialogCount = 0;
    bool isDialogFininshed = false;

    void Update()
    {
        if (!isDialogFininshed)
        {
            if (isActorSay && showText.text != tempText) isRuningText = true;
            else if (isKingSay && showKingText.text != tempText) isRuningText = true;
            else
            {
                isRuningText = false;
            }
            if (isSelectedFinish && Input.GetMouseButtonDown(0) && !isRuningText)
            {
                ShowDialog();
                if (!isRuningText) dialogCount++;
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0) && !isRuningText)
            {
                tempText = "";
                btn_Actor[ActorAbility.TypeIndex].GetComponent<Image>().color = new Color(255, 255, 255);
                King.SetActive(false);
                isKingSay = false;
                isActorSay = true;
                Destroy(ClickCanvas);
                showKingText.text = "";
                Destroy(dialogCanvas);
                ShowLog.SetActive(true);
                ShowLog.GetComponent<Text>().text = "點擊人物可以查看屬性\n請點下方的戰鬥圖示進入副本";
            }
        }
    }

    private void isInputing_pwd(string arg0)
    {
        if (SetPwd.text != "")
        {
            InputIsNull_pwd.SetActive(false);
        }
        else InputIsNull_pwd.SetActive(true);

    }

    private void isInputing(string arg0)
    {
        if (SetName.text != "")
        {
            InputIsNull.SetActive(false);
        }
        else InputIsNull.SetActive(true);
    }

    void AddItems()
    {
        List<string> ActorType = new List<string>()
        {
            ActorAbility.ActorType_All[0],
            ActorAbility.ActorType_All[1],
            ActorAbility.ActorType_All[2]
        };
        list.AddOptions(ActorType);
        list.onValueChanged.AddListener(selectedItem);
        ActorAbility.TypeIndex = list.value;
        Debug.Log(ActorAbility.ActorType_All[ActorAbility.TypeIndex]);
    }
    int n ;

    private void selectedItem(int arg0)
    {
        if (isRuningText)
        {
            list.value = n;
            return;
        }
        else
        {
            ActorAbility.TypeIndex = list.value;
            n = list.value;
            switch (list.value)
            {
                case 0:
                    showText.text = "";
                    tempText = "現在選擇的是\n<<劍士>>\n使用劍為武器的戰鬥職業，\n通常在戰鬥中，較均衡的能力都使得他在各方面都能表現傑出。\n\n\n" +
                        "體力\n★★★★★\n攻擊\n★★★☆☆\n爆擊\n★☆☆☆☆\n生存力\n★★★★☆";
                    break;
                case 1:
                    showText.text = "";
                    tempText = "現在選擇的是\n<<法師>>\n使用上古魔法進行戰鬥，有各式各樣的法術使用，戰鬥中較不需要消除，而仰賴技能戰鬥的職業。\n\n\n" +
                        "體力\n★★☆☆☆\n攻擊\n★★★★☆\n爆擊\n★★★☆☆\n生存力\n★★☆☆☆";
                    break;
                case 2:
                    showText.text = "";
                    tempText = "現在選擇的是\n<<刺客>>\n以暗殺為主要戰鬥方式的職業，不管在攻擊或是爆發力上都是最優秀的，缺點就是受到傷害時不容易恢復。\n\n\n" +
                        "體力\n★★★☆☆\n攻擊\n★★★★★\n爆擊\n★★★★★\n生存力\n★☆☆☆☆";
                    break;
            }
            StartCoroutine(TextAnimation());
        }
    }


    public AudioSource[] Actor_Say = new AudioSource[2];
    private void Check()
    {
        if (SetName.text == "" || SetPwd.text == "")
        {
            if(SetName.text == "")
                InputIsNull.SetActive(true);
            InputIsNull_pwd.SetActive(true);
        }
        else if(!isRuningText)
        {
            ActorAbility.ActorName = SetName.text;
            ActorAbility.SetActorType(ActorAbility.TypeIndex);
            SetActorData.SetActive(false);
            //Destroy(ClickCanvas);
            if (ActorAbility.TypeIndex == 0 || ActorAbility.TypeIndex == 2)
                Actor_Say[0].Play();
            else if (ActorAbility.TypeIndex == 1)
            {
                Actor_Say[1].Play();
            }
            isSelectedFinish = true;
            showText.text = "";
            tempText = "人界首領有話跟你說，請點擊任意處\n\n\t\t\t\t\t\n\n\n\n\t\t\t\t";
            StartCoroutine(TextAnimation());
            btn_Actor[ActorAbility.TypeIndex].SetActive(true);
            
        }
    }
    void ShowDialog()
    {
        showKingText.color = new Color(255, 255, 255);
        showText.color = new Color(255, 255, 255);
        if (!isRuningText)
        {
            switch (dialogCount)
            {
                case 0:
                    King.SetActive(true);
                    isKingSay = true;
                    isActorSay = false;
                    btn_Actor[ActorAbility.TypeIndex].GetComponent<Image>().color = new Color(0, 0, 0);
                    showText.text = "";
                    showKingText.text = "";
                    tempText = "\n［人界首領：冰雨］\n\n" + ActorAbility.ActorName + "，我這邊有一件事情要拜託你，" +
                        "\n由於事態緊急，希望你可以接受！";
                    StartCoroutine(TextAnimation());
                    break;
                case 1:
                    isKingSay = false;
                    isActorSay = true;
                    King.GetComponent<Image>().color = new Color(0, 0, 0);
                    btn_Actor[ActorAbility.TypeIndex].GetComponent<Image>().color = new Color(255, 255, 255);
                    showText.text = "";
                    showKingText.text = "";

                    tempText = "\n既然是冰雨大人的請求，我義不容辭。\n" +
                        "至於是什麼事情這麼緊急呢？";
                    StartCoroutine(TextAnimation());
                    break;
                case 2:
                    isKingSay = true;
                    isActorSay = false;
                    King.GetComponent<Image>().color = new Color(255, 255, 255);
                    btn_Actor[ActorAbility.TypeIndex].GetComponent<Image>().color = new Color(0, 0, 0);
                    showText.text = "";
                    showKingText.text = "";
                    tempText = "\n［人界首領：冰雨］\n\n就在前不久，位於西南方的小鎮，似乎受到了魔族的攻擊，\n具小鎮的居民表示，" +
                        "\n發動攻擊的魔族外型與魔族首領相當一致，我在懷疑魔族已經做好將魔爪伸出，並侵略人界的準備了。";
                    StartCoroutine(TextAnimation());
                    break;
                case 3:
                    isKingSay = false;
                    isActorSay = true;
                    King.GetComponent<Image>().color = new Color(0, 0, 0);
                    btn_Actor[ActorAbility.TypeIndex].GetComponent<Image>().color = new Color(255, 255, 255);
                    showText.text = "";
                    showKingText.text = "";

                    tempText = "\n您、您是說，\n魔族首領，尤、尤舞？！";
                    StartCoroutine(TextAnimation());
                    break;
                case 4:
                    isKingSay = true;
                    isActorSay = false;
                    King.GetComponent<Image>().color = new Color(255, 255, 255);
                    btn_Actor[ActorAbility.TypeIndex].GetComponent<Image>().color = new Color(0, 0, 0);
                    showText.text = "";
                    showKingText.text = "";
                    tempText = "\n［人界首領：冰雨］\n\n對，但我在推測那並不是他本人，而只是他製造出來的幻影，\n" +
                        "意圖聲東擊西，但事態嚴重程度依然不可忽視，所以我才找你過來。";
                    StartCoroutine(TextAnimation());
                    break;
                case 5:
                    isKingSay = false;
                    isActorSay = true;
                    King.GetComponent<Image>().color = new Color(0, 0, 0);
                    btn_Actor[ActorAbility.TypeIndex].GetComponent<Image>().color = new Color(255, 255, 255);
                    showText.text = "";
                    showKingText.text = "";

                    tempText = "\n那好，我即刻出發！";
                    StartCoroutine(TextAnimation());
                    break;
                case 6:
                    isKingSay = true;
                    isActorSay = false;
                    King.GetComponent<Image>().color = new Color(255, 255, 255);
                    btn_Actor[ActorAbility.TypeIndex].GetComponent<Image>().color = new Color(0, 0, 0);
                    showText.text = "";
                    showKingText.text = "\n［人界首領：冰雨］\n\n拜託你了，我最信任的家人。";
                    isDialogFininshed = true;
                    break;
                default: break;
            }
        }
        
        
    }

}
