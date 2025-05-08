using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The maximum value for hit points.")]
    public float maxHitValue = 5f;

    [Tooltip("The recovery rate per second.")]
    public float recoveryRate = 0.5f;

    [Header("Runtime")]
    [Tooltip("The current hit value. Starts at maxHitValue and recovers over time.")]
    public float currentValue;

    private void Start()
    {
        currentValue = maxHitValue;
    }

    private void Update()
    {
        if (currentValue < maxHitValue)
        {
            currentValue += recoveryRate * Time.deltaTime;
            if (currentValue > maxHitValue)
            {
                currentValue = maxHitValue;
            }
        }
    }

    /// <param name="hitAmount">Amount to subtract (default is 1).</param>
    public void TakeHit(float hitAmount = 1f)
    {
        currentValue -= hitAmount;
        if (currentValue < 0f)
        {
            currentValue = 0f;
        }
    }
}
