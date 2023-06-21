using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;
    public float maxHealth = 100;
    public float chipSpeed = 5f;
    public Image HealthBarFront;
    public Image HealthBarBack;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);//�������� ��� health
        UpdateHealthUI();
        if (Input.GetKeyUp(KeyCode.F))
        {
            TakeDamage(Random.Range(5, 20));
        }
        if (Input.GetKeyUp(KeyCode.H))
        {
            RestoreHealth(Random.Range(5, 20));
        }
    }

    public void UpdateHealthUI()// ������ � ����� ��
    {
        float fillFront = HealthBarFront.fillAmount;
        float fillBack = HealthBarBack.fillAmount;
        float hFraction = health / maxHealth;
        if (fillBack > hFraction) // �������� �� � ���������� ����
        {
            HealthBarFront.fillAmount = hFraction;
            HealthBarBack.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            HealthBarBack.fillAmount = Mathf.Lerp(fillBack, hFraction, percentComplete);
        }
        if (fillFront < hFraction) // ���������� �� � ���������� ����
        {
            HealthBarBack.color = Color.green;
            HealthBarBack.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            HealthBarFront.fillAmount =  Mathf.Lerp(fillFront, HealthBarBack.fillAmount, percentComplete);
        }

    }
    public void RestoreHealth(float healAmount)// ��������� ��
    {
        health += healAmount;
        lerpTimer = 0f;
    }
        public void TakeDamage(float damage)// �������� ��
    {
        health -= damage;
        lerpTimer = 0f;
    }
}
