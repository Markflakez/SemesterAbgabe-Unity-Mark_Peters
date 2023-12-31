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


    //Intialisiert den Slider
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }


    //Wenn das GameObject aktiviert wird, wird der Listener für die Slider-Value geaddet
    private void OnEnable()
    {
        slider.onValueChanged.AddListener(delegate { UpdateDamageAmount(); });
    }

    //Wenn das GameObject deaktiviert wird, wird der Listener für die Slider-Value entfernt
    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(delegate { UpdateDamageAmount(); });
    }

    //Aktualisiert den DamageAmount-Text basierend auf der Slider-Value
    private void UpdateDamageAmount()
    {
        damageAmount = (int)slider.value;
        damageAmountText.text = damageAmount.ToString();
    }

    //Führt den Schaden aus, wenn der Button gedrückt wird
    //Wenn der Schaden größer als 0 ist, wird der Schaden ausgeführt und der Hurt-Sound abgespielt damit der Spieler weiß, dass er Schaden genommen hat
    //Wenn die Health 0 ist, wird der Button deaktiviert
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
