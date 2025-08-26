using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockdownButton : MonoBehaviour
{
    public GameObject redButton;
    public Animator blastDoorAnim1;
    public Animator blastDoorAnim2;
    public Animator blastDoorAnim3;
    public Animator blastDoorAnim4;

    public PlayerRaycast playerRaycast;
    public bool lookingAt = false;
    public GameObject buttonTip;
    public bool called = false;

    // Camera shake vars
    public Camera mainCamera;      // drag your camera here in Inspector
    public float shakeDuration = 2f;
    public float shakeMagnitude = 0.2f;
    public float minShakeInterval = 5f;
    public float maxShakeInterval = 10f;
    public bool hazards = true;
    public GameObject buttonLight;
    public GameObject compScreen;
    public GameObject hazardCanvas;
    public bool flashing = true;


    private Vector3 originalCamPos;

    private void Start()
    {
        hazardCanvas.SetActive(false);

        if (mainCamera != null)
            originalCamPos = mainCamera.transform.localPosition;

        StartCoroutine(CameraShakeLoop());
    }

    // Update is called once per frame
    void Update()
    {
        if (!hazards && !called && playerRaycast.isHubButtonObj)
        {
            buttonTip.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                blastDoorAnim1.SetBool("buttonPressed", true);
                blastDoorAnim2.SetBool("buttonPressed", true);
                blastDoorAnim3.SetBool("buttonPressed", true);
                blastDoorAnim4.SetBool("buttonPressed", true);
                redButton.SetActive(false);
                Destroy(buttonTip);
                Destroy(buttonLight);
                flashing = false;
                called = true;
            }
        }
        else
        {
            if (!called)
            {
                buttonTip.SetActive(false);
            }
        }
    }

    private IEnumerator CameraShakeLoop()
    {
        while (!called)
        {
            float randomInterval = Random.Range(minShakeInterval, maxShakeInterval);
            yield return new WaitForSeconds(randomInterval);

            if (!called)
                yield return StartCoroutine(ShakeCamera());
        }
    }

    private IEnumerator ShakeCamera()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            if (mainCamera != null)
            {
                Vector3 randomOffset = Random.insideUnitSphere * shakeMagnitude;
                randomOffset.z = 0; // stop z-axis movement
                mainCamera.transform.localPosition = originalCamPos + randomOffset;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        if (mainCamera != null)
            mainCamera.transform.localPosition = originalCamPos;
    }
}
