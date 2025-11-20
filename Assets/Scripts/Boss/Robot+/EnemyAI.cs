using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;          // se asigna desde el BossSpawner o en escena
    public Transform[] patrolPoints;  // puntos de patrulla

    [Header("Velocidades")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;

    [Header("Detección")]
    public float detectionRange = 15f;   // hasta dónde ve
    public float viewAngle = 60f;        // ángulo de visión
    public float loseSightTime = 3f;     // tiempo para dejar de perseguir si te pierde

    int currentPointIndex = 0;
    bool isChasing = false;
    float loseTimer = 0f;

    void Update()
    {
        if (player == null)
        {
            Patrol();
            return;
        }

        if (CanSeePlayer())
        {
            isChasing = true;
            loseTimer = 0f;
        }
        else if (isChasing)
        {
            loseTimer += Time.deltaTime;
            if (loseTimer >= loseSightTime)
            {
                isChasing = false;
            }
        }

        if (isChasing)
            ChasePlayer();
        else
            Patrol();
    }

    bool CanSeePlayer()
    {
        Vector3 toPlayer = player.position - transform.position;
        float distance = toPlayer.magnitude;

        if (distance > detectionRange) return false;

        float angle = Vector3.Angle(transform.forward, toPlayer);
        if (angle > viewAngle * 0.5f) return false;

        // Raycast opcional para paredes
        if (Physics.Raycast(transform.position + Vector3.up * 1f, toPlayer.normalized, out RaycastHit hit, detectionRange))
        {
            if (!hit.collider.CompareTag("Player"))
                return false;
        }

        return true;
    }

    void Patrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
            return;

        Transform target = patrolPoints[currentPointIndex];

        MoveTowards(target.position, patrolSpeed);

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            currentPointIndex++;
            if (currentPointIndex >= patrolPoints.Length)
                currentPointIndex = 0;
        }
    }

    void ChasePlayer()
    {
        MoveTowards(player.position, chaseSpeed);
    }

    void MoveTowards(Vector3 targetPos, float speed)
    {
        Vector3 dir = targetPos - transform.position;
        dir.y = 0f;

        if (dir.sqrMagnitude > 0.001f)
        {
            dir.Normalize();
            transform.position += dir * speed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}
