using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SetBroad : MonoBehaviour
{
    public Sprite[] img = new Sprite[4];    // 放4張圖片
    public Image _img;  // 生成放置要顯示的圖片容器
    Image[,] BoardArray = new Image[5,5];   // 顯示圖片
    public GameObject panal;    
    public Text ShowTimer;
    int[,] temp = new int[5, 5];    // 用來判定索引位置value 的 array
    int stepCount = 0;  // 計算移動了幾次(按下任何方向鍵都算一次)
    public Text ShowFrightInfo;
    //public Text showStep;
    int Atk_Count = 0, Health_Count = 0, Shield_Count = 0;
    System.Random r = new System.Random();

    public static bool isDead = false;
    void Start()
    {
        if(ControlHP_MP.BossDead || ControlHP_MP.ActorDead)
        {
            isDead = true;
            ShowFrightInfo.text = "";
            ShowTimer.text = "按下任意方向鍵開始";
        }
        float x = -2f, y = 1.5f;

        for (int i = 0; i < 5; i++)
        {
            x = -2f;
            for (int j = 0; j < 5; j++)
            {
                int num = r.Next(0, 3);
                BoardArray[i, j] = Instantiate(_img);
                BoardArray[i, j].transform.SetParent(panal.transform, false);
                BoardArray[i, j].sprite = img[num];
                BoardArray[i, j].transform.position = new Vector2(x, y);
                temp[i, j] = num;
                x += 1f;
            }
            y -= 1f;
        }
    }

    int countDownTime = 4;  // move time = 4 seconds
    public static int damage = 0;
    public static int health = 0;
    public static int shield = 0;
    public static bool isTimesUp = false;   // is time's up
    public static bool BossIsAttack = false;    // boss attack
    bool countDownStart = false;    // control the countdown timer ON/OFF
    int waitTime = 0;       // wait [X] seconds 

    // Play cler up se //
    public AudioSource ClearSE;
    bool isPlayingSE = false;
    void PlayClearSE()
    {
        ClearSE.Play();
    }
    void Update()
    {
        //showStep.text = stepCount.ToString() + " 步";
        // if time's up yet and wait time is 0
        if (!isTimesUp && waitTime == 0 && !ControlHP_MP.BossDead && !ControlHP_MP.ActorDead)
        {
            isPlayingSE = false;
            if (!isNotSame())
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    MoveLeft();
                    stepCount++;
                    if (!countDownStart)
                    {
                        StartCoroutine(CountDown());
                        countDownStart = true;
                    }

                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    MoveUp();
                    stepCount++;
                    if (!countDownStart)
                    {
                        StartCoroutine(CountDown());
                        countDownStart = true;
                    }
                }

                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    MoveRight();
                    stepCount++;
                    if (!countDownStart)
                    {
                        StartCoroutine(CountDown());
                        countDownStart = true;
                    }
                }

                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    MoveDown();
                    stepCount++;
                    if (!countDownStart)
                    {
                        StartCoroutine(CountDown());
                        countDownStart = true;
                    }
                }
                SetImage();
            }

            
        }
    }

    IEnumerator CountDown()
    {
        countDownTime = 5;
        while(countDownTime > 0)
        {
            ShowTimer.text = "剩餘 " + countDownTime.ToString() + "秒";
            yield return new WaitForSeconds(1f);
            countDownTime--;
        }
        countDownTime = 0;
        damage = (int)( ((float)ActorAbility.ActorAttack * (float)Atk_Count * 0.05f) * ( 1.0f / ((float)stepCount * 0.05f)));
        health = (int)((float)Health_Count * -1.5f * (1.0f / ((float)stepCount * 0.05f)));
        //Debug.Log(damage);
        isTimesUp = true;

        stepCount = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                temp[i, j] = -1;
                BoardArray[i, j].sprite = img[3];
            }
        }

        waitTime = 5;   // wait 5 seconds

        while (waitTime > 0)
        {
            ShowFrightInfo.text = "";
            ShowTimer.text = "你造成了 " + damage.ToString() + " 點傷害\n及回復了" + (health * (-1)).ToString() + "點生命。";
            ShowFrightInfo.text = ShowTimer.text +"[ " + System.DateTime.Now.ToString("mm:ss") + " ]";
            ShowTimer.text += "請等待 " + waitTime.ToString() + "秒";
            yield return new WaitForSeconds(1f);
            waitTime--;
        }
        damage = 0;
        countDownStart = false; // reset to start timer
        damage = 70 + r.Next(30, 50);
        damage = (int)(damage - (damage * ((float)Shield_Count * 0.001f)));
        if (!ControlHP_MP.BossDead)
        {
            BossIsAttack = true;
            ShowTimer.text = "\n敵人對你造成了" + damage.ToString() + " 點傷害";
            ShowFrightInfo.text = ShowTimer.text + "[ " + System.DateTime.Now.ToString("mm:ss") + " ]";
            ShowTimer.text += "\n按下任意方向鍵開始";
        }
        // initialize abilities count//
        Atk_Count = 0;
        Health_Count = 0;
        Shield_Count = 0;
    }
    // set new image //
    void SetImage()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (temp[i, j] == -1)
                {
                    int num = r.Next(0, 3);
                    BoardArray[i, j].sprite = img[num];
                    temp[i, j] = num;
                }
            }
        }
    }
    
    // move left //
    void MoveLeft()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 1; j < 5; j++)
            {
                if (temp[i, j - 1] == temp[i, j])
                {
                    if (!isPlayingSE)
                    {
                        ClearSE.Play();
                        isPlayingSE = true;
                    }
                        
                    switch (temp[i, j])
                    {
                        case 0:
                            Atk_Count += 2;
                            break;
                        case 1:
                            Health_Count += 2;
                            break;
                        case 2:
                            Shield_Count += 2;
                            break;
                    }
                    BoardArray[i, j - 1].sprite = null;
                    BoardArray[i, j].sprite = null;
                    temp[i, j - 1] = -1;
                    temp[i, j] = -1;

                }
            }
        }
        for(int i = 0; i < 5; i++)
        {
            for(int j = 1; j < 5; j++)
            {
                int c = 1, a = 0;
                if (temp[i, j] != -1)
                {
                    while(temp[i, j - c] == -1)
                    {
                        temp[i, j - c] = temp[i, j - a];
                        temp[i, j - a] = -1;
                        BoardArray[i, j - c].sprite = BoardArray[i, j - a].sprite;
                        BoardArray[i, j - a].sprite = null;
                        c++; a++;
                        if (c > j) break;
                    }
                }
            }
        }
       //Debug.Log(Atk + "," + hp + "," + mp);
    }

    // move up //
    void MoveUp()
    {
        for(int j = 0; j < 5; j++)
        {
            for(int i = 1; i < 5; i++)
            {
                if(temp[i - 1, j] == temp[i, j])
                {
                    if (!isPlayingSE)
                    {
                        ClearSE.Play();
                        isPlayingSE = true;
                    }
                    switch (temp[i, j])
                    {
                        case 0:
                            Atk_Count += 2;
                            break;
                        case 1:
                            Health_Count += 2;
                            break;
                        case 2:
                            Shield_Count += 2;
                            break;
                    }
                    BoardArray[i - 1, j].sprite = null;
                    BoardArray[i, j].sprite = null;
                    temp[i - 1, j] = -1;
                    temp[i, j] = -1;
                }
            }
        }
        for (int j = 0; j < 5; j++)
        {
            for (int i = 1; i < 5; i++)
            {
                int c = 1, a = 0;
                if(temp[i, j] != -1)
                {
                    while(temp[i - c, j] == -1)
                    {
                        temp[i - c, j] = temp[i - a, j];
                        temp[i - a, j] = -1;
                        BoardArray[i - c, j].sprite = BoardArray[i - a, j].sprite;
                        BoardArray[i - a, j].sprite = null;
                        c++; a++;
                        if (c > i) break;
                    }
                }
            }

        }
        //Debug.Log(Atk + "," + hp + "," + mp);
    }

    // move right //
    void MoveRight()
    {
        for(int i = 0; i < 5; i++)
        {
            for(int j = 3; j >= 0; j--)
            {
                if(temp[i, j + 1] == temp[i, j])
                {
                    if (!isPlayingSE)
                    {
                        ClearSE.Play();
                        isPlayingSE = true;
                    }
                    switch (temp[i, j + 1])
                    {
                        case 0:
                            Atk_Count += 2;
                            break;
                        case 1:
                            Health_Count += 2;
                            break;
                        case 2:
                            Shield_Count += 2;
                            break;
                    }
                    BoardArray[i, j + 1].sprite = null;
                    BoardArray[i, j].sprite = null;
                    temp[i, j + 1] = -1;
                    temp[i, j] = -1;

                }
            }
        }
        for (int i = 0; i < 5; i++)
        {
            for (int j = 3; j >= 0; j--)
            {
                int c = 1, a = 0;
                if(temp[i, j] != -1)
                {
                    while(temp[i, j + c] == -1)
                    {
                        temp[i, j + c] = temp[i, j + a];
                        temp[i, j + a] = -1;
                        BoardArray[i, j + c].sprite = BoardArray[i, j + a].sprite;
                        BoardArray[i, j + a].sprite = null;
                        c++;a++;
                        if (c + j > 4) break;
                    }
                }
            }
        }
        //Debug.Log(Atk + "," + hp + "," + mp);
    }

    // move down //
    void MoveDown()
    {
        for (int j = 0; j < 5; j++)
        {
            for (int i = 3; i >= 0; i--)
            {
                if(temp[i + 1, j] == temp[i, j])
                {
                    if (!isPlayingSE)
                    {
                        ClearSE.Play();
                        isPlayingSE = true;
                    }
                    switch (temp[i + 1, j])
                    {
                        case 0:
                            Atk_Count += 2;
                            break;
                        case 1:
                            Health_Count += 2;
                            break;
                        case 2:
                            Shield_Count += 2;
                            break;
                    }
                    BoardArray[i + 1, j].sprite = null;
                    BoardArray[i, j].sprite = null;
                    temp[i + 1, j] = -1;
                    temp[i, j] = -1;
                }

            }
        }
        for(int j =0; j < 5; j++)
        {
            for(int i = 3; i >= 0; i--)
            {
                int c = 1, a = 0;
                if(temp[i, j] != -1)
                {
                    while(temp[i + c, j] == -1)
                    {
                        temp[i + c, j] = temp[i + a, j];
                        temp[i + a, j] = -1;
                        BoardArray[i + c, j].sprite = BoardArray[i + a, j].sprite;
                        BoardArray[i + a, j].sprite = null;
                        c++; a++;
                        if (i + c > 4) break;
                    }
                }
            }
        }
        //Debug.Log(Atk + "," + hp + "," + mp);

    }

    // lose board //
    bool isNotSame()
    {
        // 向右判定 //
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 4;  j++)
            {
                if (temp[i, j] == temp[i, j + 1])
                    return false;
            }
        }

        // 向下判定 //
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                if (temp[i, j] == temp[i + 1, j])
                    return false;   
            }
        }
        return true;
    }

    
}
