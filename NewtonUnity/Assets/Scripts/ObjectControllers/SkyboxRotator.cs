using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    public float rotationSpeed = 1.0f; // degrees per second

    void Update()
    {
        RenderSettings.skybox.SetFloat("Rotation", Time.time * rotationSpeed);
    }
}