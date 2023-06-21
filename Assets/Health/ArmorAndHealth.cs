using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorHealth : MonoBehaviour
{
    private float armor;
    private float lerpTimer;
    public float maxArmor = 50;
    public float chipSpeed = 5f;
    public Image ArmorBarFront;
    public Image ArmorBarBack;

    private float health;
    public float maxHealth = 100;
    public Image HealthBarFront;
    public Image HealthBarBack;

    // Start is called before the first frame update
    void Start()
    {
        armor = maxArmor;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        armor = Mathf.Clamp(armor, 0, maxArmor);//�������� ��� armor
        health = Mathf.Clamp(health, 0, maxHealth);//�������� ��� health

        UpdateArmorUI();
        UpdateHealthUI();

        if (Input.GetKeyUp(KeyCode.F))
        {
            TakeDamage(Random.Range(5, 20));
        }
        if (Input.GetKeyUp(KeyCode.H))
        {
            RestoreHealth(Random.Range(5, 20));
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            TakeArmorDamage(Random.Range(5, 20));
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            RestoreArmor(Random.Range(5, 20));
        }
    }

    public void UpdateArmorUI()// ������ � ����� �����
    {
        float fillFront = ArmorBarFront.fillAmount;
        float fillBack = ArmorBarBack.fillAmount;
        float aFraction = armor / maxArmor;
        if (fillBack > aFraction) // �������� ����� � ���������� ����
        {
            ArmorBarFront.fillAmount = aFraction;
            ArmorBarBack.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            ArmorBarBack.fillAmount = Mathf.Lerp(fillBack, aFraction, percentComplete);
        }
        if (fillFront < aFraction) // ���������� ����� � ���������� ����
        {
            ArmorBarBack.color = Color.green;
            ArmorBarBack.fillAmount = aFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            ArmorBarFront.fillAmount = Mathf.Lerp(fillFront, ArmorBarBack.fillAmount, percentComplete);
        }
    }

    public void UpdateHealthUI()// ������ � ����� ��������
    {
        float fillFront = HealthBarFront.fillAmount;
        float fillBack = HealthBarBack.fillAmount;
        float hFraction = health / maxHealth;
        if (fillBack > hFraction) // �������� �������� � ���������� ����
        {
            HealthBarFront.fillAmount = hFraction;
            HealthBarBack.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            HealthBarBack.fillAmount = Mathf.Lerp(fillBack, hFraction, percentComplete);
        }
        if (fillFront < hFraction) // ���������� �������� � ���������� ����
        {
            HealthBarBack.color = Color.green;
            HealthBarBack.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            HealthBarFront.fillAmount = Mathf.Lerp(fillFront, HealthBarBack.fillAmount, percentComplete);
        }
    }

    public void RestoreArmor(float healAmount)// ��������� �����
    {
        armor += healAmount;
        lerpTimer = 0f;
    }

    public void TakeArmorDamage(float damage)// �������� �����
    {
        armor -= damage;
        lerpTimer = 0f;
    }

    public void RestoreHealth(float healAmount)// ��������� ��������
    {
        if (armor > 0) // ���� � ��������� ���� �����
        {
            // ����������� �������� � ������ �����
            float healthToAdd = Mathf.Min(healAmount, armor);
            armor -= healthToAdd;
            healAmount -= healthToAdd;
        }

        health += healAmount;
        lerpTimer = 0f;
    }

    public void TakeDamage(float damage)// �������� ��������
    {
        if (armor > 0) // ���� � ��������� ���� �����
        {
            // ��������� ���������� ���� � ������ �����
            float damageToTake = Mathf.Min(damage, armor);
            armor -= damageToTake;
            damage -= damageToTake;
        }

        health -= damage;
        lerpTimer = 0f;
    }
}
