using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Fire : MonoBehaviour
{
    private Light2D light;

    private float timer;
    
    [SerializeField] private float power;
    [SerializeField] private float speed;

    void Start()
    {
        light = GetComponent<Light2D>();
    }

    void Update()
    {
        timer += Time.deltaTime * speed;
        
        light.intensity = power * Mathf.PerlinNoise(timer, 0f);
    }
}