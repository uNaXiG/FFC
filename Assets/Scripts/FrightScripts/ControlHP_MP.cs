using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControlHP_MP : MonoBehaviour
{
    // Set HP //
    static int temp_MaxHP = ActorAbility.ActorHp;
    static int ActorMaxHp = ActorAbility.ActorHp;   // 最大血量
    static int NowActorHp = ActorMaxHp;     // 當前血量
    public Text ShowActorHp;    // 顯示血量
    public RectTransform HpBar, HurtBar;   // Actor 血量條
    float HPBarWidth; // 血量寬度
    float HpBarWidth_temp;

    // Set MP //
    static int ActorMaxMp = ActorAbility.ActorMp;   // 最大魔力
    static int NowActorMp = ActorMaxMp;     // 當前魔力
    public Text ShowActorMp;    // 顯示魔力
    public RectTransform MpBar, MPDown;   // Actor 魔力條
    float MPBarWidth; // 魔力值寬度
    float MpBarWidth_temp;

    // Set Boss HP //
    static System.Random r = new System.Random();
    static int BossMaxHp = 2000 + r.Next(-100, 600);   // Boss最大血量
    static int NowBossHp = BossMaxHp;     // 當前BOSS血量
    public Text ShowBossHp;    // 顯示Boss血量
    public RectTransform BossHpBar, HpDown;   // Boss 血條
    float BossHPBarWidth; // Boss血量寬度
    float BossHpBarWidth_temp;


    public static bool BossDead = false;
    public static bool ActorDead = false;

    // wait game over //
    public Text GameOverText;
    public GameObject fadeOut;
    bool isOver = false;

    IEnumerator FadeOut()
    {
        isOver = true;
        Instantiate(fadeOut);
        yield return 0;
        yield return new WaitForSeconds(1.5f);
        if(BossDead)
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);  // Boss is dead
        else if (ActorDead)
            UnityEngine.SceneManagement.SceneManager.LoadScene(4);  //Actor is dead
    }

    IEnumerator WaitForGameOver()
    {
        int waitTime = 5;
        if (BossDead)
        {
            while (waitTime > 0)
            {
                GameOverText.text = "你打倒敵人了  在" + waitTime.ToString() + " 秒後結束遊戲";
                yield return new WaitForSeconds(1f);
                waitTime--;
            }
        }
        else if (ActorDead)
        {
            while (waitTime > 0)
            {
                GameOverText.text = "你陣亡了  在" + waitTime.ToString() + " 秒後結束遊戲";
                yield return new WaitForSeconds(1f);
                waitTime--;
            }
        }
        if(!isOver)
            StartCoroutine(FadeOut());
    }

    // Play actor attack se //

    public AudioSource[] ActorAttack = new AudioSource[2];
    public void PlayAttack()
    {
        if (ActorAbility.TypeIndex == 0 || ActorAbility.TypeIndex == 2)
        {
            ActorAttack[0].Play();
        }
        else if (ActorAbility.TypeIndex == 1)
        {
            ActorAttack[1].Play();
        }
    }

    private void Start()
    {
        if (SetBroad.isDead)
        {
            ActorMaxHp = temp_MaxHP;
            NowActorHp = ActorMaxHp;
            BossMaxHp = 2000 + r.Next(-100, 600);
            NowBossHp = BossMaxHp;
        }
        HpBarWidth_temp = HpBar.sizeDelta.x;
        ShowActorHp.text = ActorAbility.ActorHp + " / " + ActorAbility.ActorHp;
        MpBarWidth_temp = MpBar.sizeDelta.x;
        ShowActorMp.text = ActorAbility.ActorMp + " / " + ActorAbility.ActorMp;

        BossHpBarWidth_temp = BossHpBar.sizeDelta.x;
        ShowBossHp.text = NowBossHp + " / " + BossMaxHp;
    }
    
    void Update()
    {
        // boss damage //
        if (SetBroad.isTimesUp)
        {
            SetBroad.isTimesUp = false;
            Boss_HP_Damage(SetBroad.damage);
            HP_Damage(SetBroad.health);
        }

        // actor damage //
        if (SetBroad.BossIsAttack)
        {            
            HP_Damage(SetBroad.damage);
        }
                        
        // game over? //
        if(BossDead)
        {
            StartCoroutine(WaitForGameOver());
        }
        else if (ActorDead)
        {
            StartCoroutine(WaitForGameOver());
        }

        if (HpDown.sizeDelta.x > BossHpBar.sizeDelta.x)
        {
            HpDown.sizeDelta += new Vector2(-1, 0) * Time.deltaTime * 100;      // 讓傷害量(紅色血條)逐漸追上當前血量
        }
        if (HurtBar.sizeDelta.x > HpBar.sizeDelta.x)
        {
            HurtBar.sizeDelta += new Vector2(-1, 0) * Time.deltaTime * 50;      // 讓傷害量(紅色血條)逐漸追上當前血量
        }
        if (MPDown.sizeDelta.x > MpBar.sizeDelta.x)
        {
            MPDown.sizeDelta += new Vector2(-1, 0) * Time.deltaTime * 50;
        }
    }

    // Control HP Value //
    // boss damaging //
    IEnumerator BossDamaging()
    {
        int wait = 2;
        while(wait > 0)
        {
            yield return new WaitForSeconds(0.2f);
            wait--;
        }
        actorImg[ActorAbility.TypeIndex].color = new Color(255, 255, 255, 255);
        bossImg.color = new Color(255, 255, 255, 255);
    }
    public Image bossImg;
    public Image[] actorImg = new Image[3];

    public void Boss_HP_Damage(int damage)
    {
        if (damage > NowBossHp) damage = NowBossHp;
        if (NowBossHp - damage >= 0)
        {
            PlayAttack();
            bossImg.color = new Color(255, 0, 0, 255);
            actorImg[ActorAbility.TypeIndex].color = new Color(255, 255, 255, 0);
            StartCoroutine(BossDamaging());
            NowBossHp -= damage;
            BossHPBarWidth = BossHpBarWidth_temp * ((float)NowBossHp / (float)BossMaxHp);
            Debug.Log(NowBossHp);
            BossHpBar.sizeDelta = new Vector2(BossHPBarWidth, BossHpBar.sizeDelta.y);
            ShowBossHp.text = NowBossHp + " / " + BossMaxHp;
        }
        if (NowBossHp <= 0) BossDead = true;
    }

    public AudioSource bossAttackSE;  
    // boss attack//
    public void HP_Damage(int damage)
    {
        if (damage > NowActorHp) damage = NowActorHp;
        if (NowActorHp - damage >= ActorMaxHp) damage = (ActorMaxHp - NowActorHp) * -1;
        if (SetBroad.BossIsAttack)
        {
            bossAttackSE.Play();
            SetBroad.BossIsAttack = false;
            actorImg[ActorAbility.TypeIndex].color = new Color(255, 0, 0, 255);
            bossImg.color = new Color(255, 255, 255, 0);
            StartCoroutine(BossDamaging());
        }
        NowActorHp -= damage;
        HPBarWidth = HpBarWidth_temp * ((float)NowActorHp / (float)ActorMaxHp);
        Debug.Log(HPBarWidth);
        HpBar.sizeDelta = new Vector2(HPBarWidth, HpBar.sizeDelta.y);
        ShowActorHp.text = NowActorHp + " / " + ActorAbility.ActorHp;
        ActorAbility.ActorHp = NowActorHp;

        if (NowActorHp <= 0) ActorDead = true;
    } 
}
