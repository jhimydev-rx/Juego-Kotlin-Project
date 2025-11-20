using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] points;       // puntos de patrulla
    public float speed = 3f;         // velocidad de movimiento
    public float turnSpeed = 5f;     // qué tan rápido gira
    public float reachDistance = 1f; // distancia para "llegar" a un punto
    public bool canMove = true;

    int currentPointIndex = 0;

    void Update()
    {
        if (!canMove) return;
        if (points == null || points.Length == 0) return;

        Transform targetPoint = points[currentPointIndex];

        // dirección, ignorando Y (altura)
        Vector3 dir = targetPoint.position - transform.position;
        dir.y = 0;

        if (dir.sqrMagnitude > 0.01f)
        {
            // mover
            Vector3 move = dir.normalized * speed * Time.deltaTime;
            transform.position += move;

            // girar suave hacia el punto
            Quaternion targetRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);
        }

        // ¿está lo bastante cerca como para cambiar de punto?
        float dist = Vector3.Distance(
            new Vector3(transform.position.x, 0, transform.position.z),
            new Vector3(targetPoint.position.x, 0, targetPoint.position.z)
        );

        if (dist < reachDistance)
        {
            currentPointIndex++;
            if (currentPointIndex >= points.Length)
                currentPointIndex = 0;
        }
    }
}
