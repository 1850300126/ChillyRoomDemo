using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;    

    public GameObject enemyPrafab;

    public List<Transform> enemyMovePoints = new List<Transform>();

    public List<Transform> enemyCreatePoints = new List<Transform>();

    public int createCount = 10;

    public float createInterval = 5f;



    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(nameof(CreateInterval), createCount);
    }


    private IEnumerator CreateInterval()
    {
        CreateEnemy(createCount);

        yield return new WaitForSeconds(createInterval);

        StartCoroutine(nameof(CreateInterval), createCount);
    }

    public void CreateEnemy(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject _enemyObj = Instantiate(enemyPrafab, this.transform);
            // 随机生成位置
            int index = Random.Range(0, 2);

            _enemyObj.transform.position = enemyCreatePoints[index].position;

            _enemyObj.transform.rotation = enemyMovePoints[index].rotation;

            _enemyObj.GetComponent<EnemyController>().Init();
        }
    }
}
