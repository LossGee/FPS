using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  그 위치에 생성하고 싶다. 
// 일정시간마다 적공장에서 적을 만들어서 내위치에 배치하고 싶다. 
// 생성시간을 생성직후에 랜덤으로 정하고 싶다.
public class SpawnManager : MonoBehaviour
{
    float currentTime;
    float createTime = 1;
    public float min = 1;
    public float max = 3;
    int preRandomIndex = -1;        // 범위 밖의 숫자를 사용하여 표현
    public GameObject enemyFactory;
    public Transform[] spawnList;   // Start 부분에 GetCompoentsInChildren을 활용할 수도 있음. but 이때는 SpawnManager 자신의 Transform도 포함이 된다. 


    // Start is called before the first frame update
    void Start()
    {
        // 태어날 때 생성시간을 랜덤으로 정하고 싶다. 
        // 생성시간 = 랜덤(min, max)
        createTime = Random.Range(min, max);

    }

    // Update is called once per frame
    void Update()
    {
        // 만약 생성 횟수(creatCount) < 최대생성횟수 
        if (LevelManager.Instance.CanCreateEnemy())
        {
            // 1. 현재 시간이 흐르다가. 
            currentTime += Time.deltaTime;
            // 2. 만약 현재시간이 생성시간을 초과하면
            if (currentTime > createTime)
            {
                // 3. 적공장에서 적을 만들어서 
                GameObject enemy = Instantiate(enemyFactory);
                LevelManager.Instance.createCount++;
                //4. spawnList의 임의의 위치에 배치하고 싶다.
                int randomIndex = Random.Range(0, spawnList.Length);
                //만약 randomIndex가  preRandomIndex와 같다면
                //randomIndex를 다시 정하고 싶다. 
                if (randomIndex == preRandomIndex)
                {
                    //randomIndex를 다시 정하고 싶다. (강사님의 노하우, while문으로 처리했을 때 무한루프에 걸리는 것을 방지. 기획자가 spawn을 1개만 만들었을 때;;)
                    randomIndex = (randomIndex + 1) % spawnList.Length;

                }

                Vector3 pos = spawnList[randomIndex].position;
                enemy.transform.position = pos;

                // 5. 현재시간을 0로 초기화 해야한다.
                currentTime = 0;
                // 6. 생성시간을 생성직후에 랜덤으로 정하고 싶다.
                createTime = Random.Range(min, max);
                preRandomIndex = randomIndex;
            }
        }

    }
}
