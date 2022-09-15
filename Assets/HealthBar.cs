using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Health health;
    Image bar;
    // Start is called before the first frame update
    void Start()
    {
        bar = transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health)
        {
            bar.fillAmount = health.health/health.maxHealth;
        }
    }
}
