using System.Collections;
using UnityEngine;

public class FlashingLight : MonoBehaviour
{
    public Light alarmLight;
    public float minIntensity = 0.5f;
    public float maxIntensity = 5f;
    public float pulseSpeed = 1f;
    public UnlockdownButton unlockdownButton;

    public GameObject redObject;


    private bool isTransitioning = false;

    private void Update()
    {
        if (unlockdownButton.flashing && alarmLight != null)
        {
            float t = (Mathf.Sin(Time.time * pulseSpeed * Mathf.PI * 2) + 1f) / 2f;
            alarmLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
            alarmLight.color = Color.red;
        }
        else if (!unlockdownButton.flashing && !isTransitioning && alarmLight != null)
        {
            StartCoroutine(LightsNormal());
        }
    }

    private IEnumerator LightsNormal()
    {
        isTransitioning = true;
        redObject.SetActive(false);

        //Fade out current flashing
        float fadeDuration = 1f;
        float startIntensity = alarmLight.intensity;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            alarmLight.intensity = Mathf.Lerp(startIntensity, 0f, time / fadeDuration);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        maxIntensity = 1f;
        alarmLight.range = 30f;

        // 3. Quick white flicker
        for (int i = 0; i < 2; i++)
        {
            alarmLight.color = Color.white;
            alarmLight.intensity = maxIntensity;
            yield return new WaitForSeconds(0.1f);

            alarmLight.intensity = 0f;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(2f);

        //fade into steady white light
        time = 0f;
        fadeDuration = 2f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            alarmLight.intensity = Mathf.Lerp(0f, maxIntensity, time / fadeDuration);
            alarmLight.color = Color.white;
            yield return null;
        }

        alarmLight.intensity = maxIntensity;
        alarmLight.color = Color.white;
        //isTransitioning = false;
    }
}
