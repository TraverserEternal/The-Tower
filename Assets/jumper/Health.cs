using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Health : MonoBehaviour
{
    public float health { get; private set; }
    [SerializeField] float maxHealth = 3;
    [SerializeField] TextMeshPro healthText;

    private void Start()
    {
        SetHealth(maxHealth);
    }
    public void SetHealth(float value)
    {
        healthText.text = $"hp: {value.ToString()}";
        health = value;
        if (health <= 0) Destroy(gameObject);
    }
    public void Damage(float damage)
    {
        SetHealth(health - damage);
    }
}
