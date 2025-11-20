using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light flashlight;
    public int maxCharges = 3;
    public float onTime = 2f;

    int currentCharges;
    bool isOn = false;
    float timer = 0f;

    void Start()
    {
        currentCharges = maxCharges;
        if (flashlight != null)
            flashlight.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isOn && currentCharges > 0)
        {
            TurnOn();
        }

        if (isOn)
        {
            timer += Time.deltaTime;
            if (timer >= onTime)
            {
                TurnOff();
            }
        }
    }

    void TurnOn()
    {
        isOn = true;
        timer = 0f;
        currentCharges--;

        if (flashlight != null)
            flashlight.enabled = true;
    }

    void TurnOff()
    {
        isOn = false;
        if (flashlight != null)
            flashlight.enabled = false;
    }

    public void AddCharges(int amount)
    {
        currentCharges = Mathf.Clamp(currentCharges + amount, 0, maxCharges);
    }

    public int GetCharges()
    {
        return currentCharges;
    }

    // 👉 ESTA ES LA NUEVA PROPIEDAD 👈
    public bool IsOn
    {
        get { return isOn; }
    }
}
