using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Image healthBarFill;
    public HealthSystem healthSystem;
    public TextMeshProUGUI healthAmountText;

    #region OnEnable Kommentare
    //Wird aufgerufen, wenn das GameObject aktiviert wird.
    //Connected die Methode UpdateHealthBar als Observer für OnHealthChanged-Event.
    #endregion
    private void OnEnable()
    {
        healthSystem.OnHealthChanged += UpdateHealthBar;
    }

    #region OnDisable Kommentare
    //Wird aufgerufen, wenn das GameObject deaktiviert wird.
    //Disconnected die Methode UpdateHealthBar als Observer für OnHealthChanged-Event.
    #endregion
    private void OnDisable()
    {
        healthSystem.OnHealthChanged -= UpdateHealthBar;
    }

    #region UpdateHealthBar Kommentare
    //Aktualisiert die Health Bar und den Healh Bar text mit dem akutellen Health Wert.
    #endregion
    private void UpdateHealthBar(float health)
    {
        GetComponent<Slider>().value = health;
        healthAmountText.text = health.ToString();
    }
}
