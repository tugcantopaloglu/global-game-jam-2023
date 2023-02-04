using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBarBehavior : MonoBehaviour
{
    public Slider slider;
    public Color low;
    public Color high;
    public Vector3 offSet;

    public void setHealth(float health, float maxHealth)
    {
        slider.gameObject.SetActive(health < maxHealth);
        slider.value = health;
        slider.maxValue = maxHealth;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }
    
    // Update is called once per frame
    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.position + offSet);


    }
}
