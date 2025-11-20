using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    public int chargesToAdd = 1; // cuántas cargas da la batería

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Buscar el controlador de linterna dentro del jugador
            FlashlightController flashlight = other.GetComponentInChildren<FlashlightController>();

            if (flashlight != null)
            {
                flashlight.AddCharges(chargesToAdd);
            }

            // Destruir la batería
            Destroy(gameObject);
        }
    }
}
