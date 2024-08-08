using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ActorAbility : MonoBehaviour
{
    public static string[] ActorType_All = new string[3] { "劍士", "法師", "刺客" };
    public static string ActorName { get; set; } = "";     // 腳色名稱
    public static int TypeIndex = 0;
    public static string ActorType { get; set; } = ActorType_All[0];   // 腳色職業
    public static int ActorStar { get; set; } = 3;      // 腳色初始星級
    public static double ActorMaxExp { get; set; } = 100.0;   // 腳色MaxEXP
    public static double ActorExp { get; set; } = 0.0;   // 腳色EXP
    public static int ActorLv { get; set; } = 1;       // 腳色等級
    public static int ActorHp { get; set; } = 500;     // 腳色血量
    public static int ActorMp { get; set; } = 100;     // 腳色魔力
    public static int ActorStr { get; set; } = 10;   // 腳色力量
    public static int ActorAgile { get; set; } = 5;    // 腳色敏捷
    public static int ActorWisdom { get; set; } = 5;   // 腳色智慧
    public static int ActorAttack { get; set; } = 100;   // 腳色攻擊力
    public static int ActorDefense { get; set; } = 50;   // 腳色防禦力
    public static string percent { get; set; } = "0.00%";        // EXP百分比
    /// <summary>
    /// 設定腳色所有屬性，可動態調整
    /// </summary>
    /// <param name="actor_Name">腳色名稱</param>
    /// <param name="actor_Type">腳色職業</param>
    /// <param name="actorMaxExp">腳色MAX經驗值</param>
    /// /// <param name="actorExp">腳色當前經驗值</param>
    /// <param name="actor_Lv">腳色等級</param>
    /// <param name="actorHp">腳色血量</param>
    /// <param name="actorMp">腳色魔力</param>
    /// <param name="actorStr">腳色力量</param>
    /// <param name="actorAgi">腳色敏捷</param>
    /// <param name="actorWis">腳色智慧</param>
    /// <param name="actorAtk">腳色攻擊力</param>
    /// <param name="actorDef">腳色防禦力</param>
    public static void setActorAbilities
        (string actor_Name, string actor_Type, int actorStar, double actorMaxExp, double actorExp,
        int actor_Lv, int actorHp, int actorMp, int actorStr, int actorAgi,
        int actorWis, int actorAtk, int actorDef, int index)
    {
        ActorName = actor_Name;
        ActorType = actor_Type;
        ActorStar = actorStar;
        ActorMaxExp = actorMaxExp;
        ActorExp = actorExp;
        ActorLv = actor_Lv;
        ActorHp = actorHp;
        ActorMp = actorMp;
        ActorStr = actorStr;
        ActorAgile = actorAgi;
        ActorWisdom = actorWis;
        ActorAttack = actorAtk;
        ActorDefense = actorDef;
        TypeIndex = index;
    }
    
    public static void GetExp(double getExp)
    {
        ActorExp += getExp;
        while (true)
        {
            if(ActorExp >= ActorMaxExp)
            {
                ActorLv++;
                double flowExp = ActorExp - ActorMaxExp;
                ActorMaxExp += ((33.5f * ActorLv) + 52.5f);
                ActorExp = flowExp;
            }
            else
            {
                break;
            }
        }
        percent = ((ActorExp / ActorMaxExp) * 100.0).ToString("f2") + " % ";
    }
    
    public static void SetActorType(int TypeIndex)
    {
        ActorType = ActorType_All[TypeIndex];
        switch (TypeIndex)
        {
            case 0: // frighter
                setActorAbilities(ActorName, ActorType, 3,
                    160.33, 0.0, 1, SetHp(TypeIndex, 1, 20, 3), SetMp(TypeIndex, 1, 15, 3),
                    20, 25, 15, SetAtk(TypeIndex, 20, 25, 15, 3), SetDef(), 0);
                break;
            case 1: // magic
                setActorAbilities(ActorName, ActorType, 3,
                    160.33, 0.0, 1, SetHp(TypeIndex, 1, 5, 3), SetMp(TypeIndex, 1, 25, 3),
                    5, 15, 25, SetAtk(TypeIndex, 5, 15, 25, 3), SetDef(), 1);
                break;
            case 2: // Thieves
                setActorAbilities(ActorName, ActorType, 3,
                    160.33, 0.0, 1, SetHp(TypeIndex, 1, 10, 3), SetMp(TypeIndex, 1, 15, 3), 
                    10, 30, 15, SetAtk(TypeIndex, 10, 30, 15, 3), SetDef(), 2);
                break;
            default: break;
        }
    }

    private static int SetHp(int TypeIndex, int Level, int Str, int Star)
    {
        // set hp initial value
        switch (TypeIndex)
        {
            case 0: // frighter
                ActorHp = (450 + (Level * 20) + (Str * 21));                
                break;
            case 1: // magic
                ActorHp = ( 400 + (Level * 20) + (Str * 8) );
                break;
            case 2: // Thieves
                ActorHp = ( 400 + (Level * 20) +(Str * 10) );
                break;
            default: ActorHp = 500;
                break;
        }
        ActorHp += ActorHp * (int)(Star * 0.085);
        return ActorHp;
    }

    private static int SetMp(int TypeIndex, int Level, int Wis, int Star)
    {
        switch (TypeIndex)
        {
            case 0: // frighter
                ActorMp = 120 + (Level * 10) + 50;
                break;
            case 1: // magic
                ActorMp = (100 + (Level * 10) + (Wis * 10)) + 50;
                break;
            case 2: // Thieves
                ActorMp = 120 + (Level * 10) + 150;
                break;
            default:
                ActorMp = 100;
                break;
        }
        ActorMp += ActorMp * (int)(Star * 0.085);
        return ActorMp;
    }

    private static int SetAtk(int TypeIndex, int Str, int Agi, int Wis, int Star)
    {
        switch (TypeIndex)
        {
            case 0:
                ActorAttack = 100 + (int)(Str * 0.8) + (int)(Agi * 0.3);
                break;
            case 1:
                ActorAttack = 100 + (int)(Wis * 0.8);
                break;
            case 2:
                ActorAttack = 100 + (int)(Str * 0.3) + Agi;
                break;
            default:
                ActorAttack = 100;
                break;
        }
        ActorAttack += ActorAttack * (int)(Star * 0.085);
        return ActorAttack;
    }

    private static int SetDef()
    {
        return 10;
    }
}
