using UnityEngine;

public class BatterySpawner : MonoBehaviour
{
    public GameObject batteryPrefab;      // tu prefab de batería
    public Transform[] spawnPoints;       // puntos posibles
    public int batteriesPerGame = 3;      // cuántas baterías por partida

    void Start()
    {
        SpawnBatteries();
    }

    void SpawnBatteries()
    {
        if (batteryPrefab == null || spawnPoints.Length == 0) return;

        // Opcional: evitar spawns repetidos en el mismo punto
        // Hacemos una lista de índices disponibles
        System.Collections.Generic.List<int> available = new System.Collections.Generic.List<int>();
        for (int i = 0; i < spawnPoints.Length; i++)
            available.Add(i);

        int count = Mathf.Min(batteriesPerGame, available.Count);

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, available.Count);
            int spawnIndex = available[randomIndex];
            available.RemoveAt(randomIndex); // así no repetimos punto

            Transform point = spawnPoints[spawnIndex];
            Instantiate(batteryPrefab, point.position, point.rotation);
        }
    }
}
