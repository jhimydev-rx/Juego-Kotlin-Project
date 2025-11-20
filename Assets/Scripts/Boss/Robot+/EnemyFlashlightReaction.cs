using UnityEngine;

public class EnemyFlashlightReaction : MonoBehaviour
{
    [Header("Referencias (auto)")]
    public FlashlightController flashlight;
    public EnemyPatrol enemyPatrol;

    [Header("Teletransporte")]
    public Transform[] teleportPoints;
    public float teleportCooldown = 2f;

    float lastTeleportTime = -999f;

    void Awake()
    {
        if (flashlight == null)
            flashlight = FindObjectOfType<FlashlightController>();

        if (enemyPatrol == null)
            enemyPatrol = GetComponent<EnemyPatrol>();
    }

    void Update()
    {
        if (flashlight == null) return;
        if (teleportPoints == null || teleportPoints.Length == 0) return;

        // Solo reacciona si la linterna está encendida
        if (!flashlight.IsOn) return;

        if (Time.time - lastTeleportTime < teleportCooldown)
            return;

        TeleportBoss();
        lastTeleportTime = Time.time;
    }

    void TeleportBoss()
    {
        int index = Random.Range(0, teleportPoints.Length);
        Transform p = teleportPoints[index];

        if (enemyPatrol != null)
            enemyPatrol.canMove = false;

        transform.position = p.position;
        transform.rotation = p.rotation;

        if (enemyPatrol != null)
            enemyPatrol.canMove = true;

        Debug.Log("[EnemyFlashlightReaction] Teleport a " + p.name);
    }
}
