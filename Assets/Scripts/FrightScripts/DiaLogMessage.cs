using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaLogMessage : MonoBehaviour
{
    public GameObject[] objToShow = new GameObject[4];

    //dialog object //
    public Image[] Actor_img = new Image[3];
    public Text Actor_text;

    public Image Boss_img;
    public Text Boss_text;

    static bool isActorMessage = false;
    static bool isBossMessage = false;

    // isFrist load? //
    static bool isFristLoad = true;

    // Start is called before the first frame update
    void Start()
    {
        if (isFristLoad)
        {
            isActorMessage = true;
            isFristLoad = false;
            Actor_text.text = "";
            Boss_text.text = "";
            DiaLoging();
        }        
    }

    // Update is called once per frame
    void Update()
    {
        if(isActorMessage && Actor_text.text == tempText)
        {
            if (Input.GetMouseButtonDown(0))
            {
                dialogCount++;
                isActorMessage = false;
                isBossMessage = true;
                DiaLoging();
            }
        }
        else if (isBossMessage && Boss_text.text == tempText)
        {
            if (Input.GetMouseButtonDown(0))
            {
                dialogCount++;
                isActorMessage = true;
                isBossMessage = false;
                DiaLoging();
            }
        }
        if (dialogCount > 7)
        {
            isActorMessage = false;
            isBossMessage = false;
            Actor_img[ActorAbility.TypeIndex].color = new Color(255, 255, 255);
        }
        if(!isActorMessage && !isBossMessage)
        {
            SetObjectActice();
        }
    }

    int dialogCount = 0;
    void DiaLoging()
    {
        switch (dialogCount)
        {
            case 0:
                Boss_img.color = new Color(0, 0, 0);
                Actor_img[ActorAbility.TypeIndex].color = new Color(255, 255, 255);
                Actor_text.text = "";
                tempText = "[ " + ActorAbility.ActorName + " ] ：\n" +
                    "站住！你就是魔族首領尤舞吧！？";
                StartCoroutine(TextAnimation(0));
                break;
            case 1:
                Boss_img.color = new Color(255, 255, 255);
                Actor_img[ActorAbility.TypeIndex].color = new Color(0, 0, 0);
                Boss_text.text = "";
                tempText = "[ 尤舞 ] \n咯咯咯咯\n是人類小兒吧？\n你也是上一批死狀悽慘的傢伙們的夥伴嗎？\n\n" +
                    "（不對，她散發的氣息不像人類）";
                StartCoroutine(TextAnimation(1));
                break;
            
            case 2:
                Boss_img.color = new Color(0, 0, 0);
                Actor_img[ActorAbility.TypeIndex].color = new Color(255, 255, 255);
                Actor_text.text = "";
                tempText = "[ " + ActorAbility.ActorName + " ] ：\n" +
                    "閉嘴！我們人類的領土也是你們魔族可以輕易踐踏的嗎！？";
                StartCoroutine(TextAnimation(0));
                break;
            case 3:
                Boss_img.color = new Color(255, 255, 255);
                Actor_img[ActorAbility.TypeIndex].color = new Color(0, 0, 0);
                Boss_text.text = "";
                tempText = "[ 尤舞 ] \n（難道他自己還沒發覺到嗎......）" +
                    "\n\n哈哈哈哈哈！\n那好吧，如果你能打敗我的話，我答應你再也不侵略人界。";
                StartCoroutine(TextAnimation(1));
                break;
            case 4:
                Boss_img.color = new Color(0, 0, 0);
                Actor_img[ActorAbility.TypeIndex].color = new Color(255, 255, 255);
                Actor_text.text = "";
                tempText = "[ " + ActorAbility.ActorName + " ] ：\n" +
                    "這可是你說的！雖然我對魔族的承諾是不抱有任何期待的。";
                StartCoroutine(TextAnimation(0));
                break;
            case 5:
                Boss_img.color = new Color(255, 255, 255);
                Actor_img[ActorAbility.TypeIndex].color = new Color(0, 0, 0);
                Boss_text.text = "";
                tempText = "[ 尤舞 ] \n不試試看怎麼知道呢，咯咯咯咯咯。";
                StartCoroutine(TextAnimation(1));
                break;
            case 6:
                Boss_img.color = new Color(0, 0, 0);
                Actor_img[ActorAbility.TypeIndex].color = new Color(255, 255, 255);
                Actor_text.text = "";
                tempText = "[ " + ActorAbility.ActorName + " ] ：\n" +
                    "沒錯，最少、最少，我要保護我生長的人界，盪平魔族！！";
                StartCoroutine(TextAnimation(0));
                break;
            case 7:
                Boss_img.color = new Color(255, 255, 255);
                Actor_img[ActorAbility.TypeIndex].color = new Color(0, 0, 0);
                Boss_text.text = "";
                tempText = "[ 尤舞 ] \n口氣不小，你就試試看吧。";
                StartCoroutine(TextAnimation(1));
                break;
            default:break;

        }

    }

    // Text Animation //
    string tempText = "";
    float textSpeed = 0.01f;

    IEnumerator TextAnimation(int sayer)
    {        
        // is Actor say
        if (sayer == 0)
        {
            foreach (char letter in tempText.ToCharArray())
            {
                Actor_text.text += letter;
                yield return 0;
                yield return new WaitForSeconds(textSpeed);
            }
        }
        // is boss say
        else
        {
            foreach (char letter in tempText.ToCharArray())
            {
                Boss_text.text += letter;
                yield return 0;
                yield return new WaitForSeconds(textSpeed);
            }
        }
    }

    // Set object Active //
    void SetObjectActice()
    {
        for (int i = 0; i < objToShow.Length - 1; i++)
        {
            objToShow[i].SetActive(true);
        }
        objToShow[objToShow.Length - 1].SetActive(false);
    }
}

