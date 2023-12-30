using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TakeDamageSlider : MonoBehaviour
{
    private Slider slider;

    public TextMeshProUGUI damageAmountText;
    public HealthSystem healthSystem;
    public AudioSource hurtAudioSource;
    private int damageAmount = 0;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }


    private void OnEnable()
    {
        slider.onValueChanged.AddListener(delegate { UpdateDamageAmount(); });
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(delegate { UpdateDamageAmount(); });
    }

    private void UpdateDamageAmount()
    {
        damageAmount = (int)slider.value;
        damageAmountText.text = damageAmount.ToString();
    }

    public void PerformDamage(Button button)
    {
        if (damageAmount > 0)
        {
            healthSystem.DecreaseHealth(damageAmount);
            hurtAudioSource.Play();
        }
        if (healthSystem.currentHealth == 0)
        {
            button.interactable = false;
            return;
        }
    }
}
