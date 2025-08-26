using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Light lightToFlicker;
    public float minInterval = 0.05f; // minimum time between flickers
    public float maxInterval = 0.3f;  // maximum time between flickers
    public float flickerChance = 0.5f; // chance that light will toggle at each interval

    private void Start()
    {
        if (lightToFlicker == null)
            lightToFlicker = GetComponent<Light>();

        StartCoroutine(FlickerRoutine());
    }

    private System.Collections.IEnumerator FlickerRoutine()
    {
        while (true)
        {
            float interval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(interval);

            // Randomly toggle the light
            if (Random.value < flickerChance)
            {
                lightToFlicker.enabled = !lightToFlicker.enabled;
            }
        }
    }
}
