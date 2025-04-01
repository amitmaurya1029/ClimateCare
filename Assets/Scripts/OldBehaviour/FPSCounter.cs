using UnityEngine;
using TMPro;  // If using TextMeshPro

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI fpsText; // Assign this in the Inspector
    private float deltaTime = 0.0f;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f; // Smooth FPS calculation
        float fps = 1.0f / deltaTime;
        fpsText.text = "FPS: " + Mathf.Ceil(fps).ToString();
    }
    
}
