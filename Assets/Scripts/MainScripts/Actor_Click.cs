using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Actor_Click : MonoBehaviour
{
    public Text txt;
    bool isClick = false;
    private void OnClick()
    {
        if (isClick)
        {
            txt.text = "點擊人物可以查看屬性\n請點下方的戰鬥圖示進入副本";
            isClick = false;
        }
        else
        {
            txt.text =
               "名稱:" + ActorAbility.ActorName + "\n" +
               "職業:" + ActorAbility.ActorType + "\n" +

               "經驗值:" + ActorAbility.ActorExp.ToString("f3") + " / " + ActorAbility.ActorMaxExp.ToString("f3") +
               " ( " + ActorAbility.percent + " )\n" +
               "等級:" + ActorAbility.ActorLv + "\n" +
               "HP:" + ActorAbility.ActorHp + "\n" +
               "MP:" + ActorAbility.ActorMp + "\n" +
               "力量(Str):" + ActorAbility.ActorStr + "\n" +
               "敏捷(Agi):" + ActorAbility.ActorAgile + "\n" +
               "智慧(Wis):" + ActorAbility.ActorWisdom + "\n" +
               "攻擊力(Atk):" + ActorAbility.ActorAttack + "\n" +
               "防禦力(Def):" + ActorAbility.ActorDefense + "\n" +
               "《再次點擊角色關閉瀏覽屬性》\n";

            isClick = true;
        }

        
    }
    // Start is called before the first frame update
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
