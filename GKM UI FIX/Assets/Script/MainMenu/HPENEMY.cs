using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPENEMY : MonoBehaviour
{
    [SerializeField] private Image _healthbarSprite;
    [SerializeField] private float _ReduceSpeed = 2;
    WeaponDamage _weaponDamage;
    private float _target = 1;
    private Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
        _weaponDamage = GetComponent<WeaponDamage>();
    }

    public void UpdateHealthBar(float maxHealth, float currentHelath)
    {
        _target = currentHelath / maxHealth;
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
        _healthbarSprite.fillAmount = Mathf.MoveTowards(_healthbarSprite.fillAmount, _target, _ReduceSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        // Reduce the current health by the damage amount and update the health bar
        float currentHealth = Mathf.Max(0, _target * _healthbarSprite.fillAmount - damage); // Ensure health doesn't go below zero
        UpdateHealthBar(_healthbarSprite.fillAmount, currentHealth);
    }
}
