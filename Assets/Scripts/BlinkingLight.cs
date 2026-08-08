using UnityEngine;

public class BlinkingLight : MonoBehaviour {

    public Light light; // wskaż światło, które chcesz zacząć migąć
    public float blinkSpeed = 1.0f; // prędkość migania w sekundach

    void Start() {
        if(light == null) {
            light = GetComponent<Light>(); // jeśli nie podano światła, odczytaj go z komponentu na tym samym obiekcie
        }
    }

    void Update() {
        light.intensity = 0.5f + (Mathf.Sin(Time.time * blinkSpeed) * 0.5f); // zmienia intensywność światła w funkcji czasu i prędkości migania
    }
}
