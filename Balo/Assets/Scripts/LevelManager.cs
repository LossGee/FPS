using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �÷��̾��� ������ ǥ���ϰ� �ʹ�.
// ���� ���� ��, ���� ���� Ƚ��(kill count, ����ġ)
// ���� �ı��� �� KillCount�� 1 ������Ű�� �ʹ�. 
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;    // Singleton
    private void Awake()
    {
        Instance = this;
    }

    public int maxCreateCount;
    public int createCount;            // ���� ������ Ƚ��
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
            // ���� killCount�� maxCreateCount�̻��̸�
            if (killCount >= maxCreateCount)
            {
                // 
                killCount++;
                // ���� killCount�� maxCreateCount �̻��̸� 
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
            // ������ 1 ������Ű�� �ʹ�. 
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
