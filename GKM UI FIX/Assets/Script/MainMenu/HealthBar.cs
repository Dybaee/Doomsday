using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthbarSprite;
    [SerializeField] private float _ReduceSpeed = 2;
    private float _target = 1;

    public void UpdateHealthBar(float maxHealth, float currentHelath)
    {
        _target = currentHelath / maxHealth;
    }

    void Update()
    {
        _healthbarSprite.fillAmount = Mathf.MoveTowards(_healthbarSprite.fillAmount, _target, _ReduceSpeed * Time.deltaTime);
    }
}
