using UnityEngine;

public class BatteryVisuals : MonoBehaviour
{
    [Header("Rotación")]
    public float rotationSpeed = 60f;

    [Header("Flotación")]
    public float bobAmplitude = 0.2f;
    public float bobSpeed = 2f;

    [Header("Luz / Brillo")]
    public Light glowLight;
    public float minIntensity = 1f;
    public float maxIntensity = 3f;
    public float pulseSpeed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Rotar
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // Flotar arriba / abajo
        float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Pulso de luz
        if (glowLight != null)
        {
            float t = (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f; // 0..1
            glowLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
        }
    }
}
