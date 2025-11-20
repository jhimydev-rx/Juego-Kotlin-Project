using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform[] spawnPoints;   // BossSpawn(1..4)
    public Transform[] patrolPoints;  // BossPatrol(1..N)
    public Transform player;          // el Player de la escena

    void Start()
    {
        SpawnBoss();
    }

    void SpawnBoss()
    {
        if (bossPrefab == null || spawnPoints.Length == 0)
        {
            Debug.LogError("[BossSpawner] Falta prefab o spawnPoints");
            return;
        }

        int index = Random.Range(0, spawnPoints.Length);
        Transform point = spawnPoints[index];

        GameObject boss = Instantiate(bossPrefab, point.position, point.rotation);

        // Pasar puntos de patrulla al boss
        EnemyPatrol patrol = boss.GetComponent<EnemyPatrol>();
        if (patrol != null)
        {
            patrol.points = patrolPoints;
        }

        // Pasar puntos de teletransporte al boss
        EnemyFlashlightReaction reaction = boss.GetComponent<EnemyFlashlightReaction>();
        if (reaction != null)
        {
            reaction.teleportPoints = spawnPoints; // usamos los spawn como teleports
        }

        // 👉 Pasar el Player al EnemyAI para que pueda perseguir
        EnemyAI ai = boss.GetComponent<EnemyAI>();
        if (ai != null)
        {
            ai.player = player;
        }
    }
}
