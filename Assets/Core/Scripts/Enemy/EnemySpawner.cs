using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyRabbit;
    [SerializeField] Transform SpawnLocation;

    float maxTime = 10f;
    float timer;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= maxTime)
        {
            timer -= maxTime;
            Spawn();
        }
    }

    void Spawn()
    {
        if (transform.childCount < 7)
        {
            GameObject go = Instantiate(enemyRabbit, transform);
            go.GetComponentInChildren<RabbitEnemyAI>().CreateEnemyStats();
        }
    }
}
