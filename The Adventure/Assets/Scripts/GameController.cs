using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public Transform player;

    [System.Serializable]
    public class EnemyController
    {
        public string Name = "Enemy";

        public GameObject enemy;
        public Transform spawnLocation;
        public Transform triggerLocation;
    }

    public List<EnemyController> enemies;
    private bool[] casting;


    void Start()
    {
        casting = new bool[enemies.Count];

        for (int i = 0; i < casting.Length; i++)
        {
            casting[i] = true;
        }
    }

    void LateUpdate()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (player.transform.position.x > enemies[i].triggerLocation.position.x && casting[i])
            {
                Debug.Log("got it down");
                SpawnEnemy(enemies[i].enemy, enemies[i].spawnLocation, i);
                casting[i] = false;
            }
        }
    }

    void SpawnEnemy(GameObject enemy, Transform position, int enemynum)
    {
        GameObject child = Instantiate(enemy);
        child.name = "Enemy " + enemynum;
        child.transform.position = position.position;
        child.transform.rotation = Quaternion.Euler(0,-90,0);
        child.GetComponent<Character>().player = GameObject.Find("Player").GetComponent<Movement>();

        child.transform.parent = GameObject.Find("Enemies").transform;
    }
}
