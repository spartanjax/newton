using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour
{
    public float waitShake;
    public lockdownTrigger lockdownTrigger;

    private bool canShake = false;

    private void Update()
    {
        if(lockdownTrigger != null && lockdownTrigger.tutorialDone == false && canShake)
        {
            float randomShakeTime = Random.Range(1, 4);
            float randomMag = Random.Range(0.1f, 0.2f);
            waitShake = Random.Range(10, 30);
            canShake = false;
            StartCoroutine(Shake(randomShakeTime, randomMag));
        }
    }
    
        public IEnumerator Shake(float duration, float magnitude)
    {
        yield return new WaitForSeconds(waitShake);
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-0.5f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, originalPos.y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
        canShake = true;
    }
}
