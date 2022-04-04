using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 플레이어의 레벨을 표현하고 싶다.
// 적의 생성 수, 적을 죽인 횟수(kill count, 경험치)
// 적이 파괴될 때 KillCount를 1 증가시키고 싶다. 
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;    // Singleton
    private void Awake()
    {
        Instance = this;
    }

    public int maxCreateCount;
    public int createCount;            // 적을 생성한 횟수
    int level;
    public Text textLevel;
    public int Level
    {
        get { return level; }
        set
        {
            level = value;
            maxCreateCount = level;
            textLevel.text = "Lv" + level;
            sliderKillCount.maxValue = level;
            sliderKillCount.value = killCount;
            createCount = 0;
        }
    }

    int killCount;
    public Slider sliderKillCount;
    public Text textKillCountPer;
    public int KillCount
    {
        get { return killCount; }
        set
        {
            killCount = value;
            // 만약 killCount가 maxCreateCount이상이면
            if (killCount >= maxCreateCount)
            {
                // 
                killCount++;
                // 만약 killCount가 maxCreateCount 이상이면 
                textKillCountPer.text = ((float)killCount / maxCreateCount * 100f) + " %";
                StartCoroutine("IELevelUp");
            }
        }
    }

    IEnumerator IELevelUp()
    {

        while (killCount >= maxCreateCount)
        {
            killCount -= maxCreateCount;
            // 레벨을 1 증가시키고 싶다. 
            Level++;
            yield return new WaitForSeconds(0.1f);
            textKillCountPer.text = ((float)killCount / maxCreateCount * 100f) + " %";
        }
        sliderKillCount.value = killCount;
    }

    public bool CanCreateEnemy()
    {
        return createCount < maxCreateCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        killCount = 0;
        maxCreateCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        print("killCount: " + killCount);
        print("maxKillCount: " + maxCreateCount);
        print("");
    }
}
