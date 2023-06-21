using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Armor: MonoBehaviour 
{    private float armor;
     private float lerpTimer;
     public float maxArmor = 50;
     public float chipSpeed = 5f;
     public Image ArmorBarFront;
     public Image ArmorBarBack;

    // Start is called before the first frame update
    void Start()
    {
        armor = maxArmor;
    }

    // Update is called once per frame
    void Update()
    {
        armor = Mathf.Clamp(armor, 0, maxArmor);//�������� ��� health
        UpdateArmorUI();
        if (Input.GetKeyUp(KeyCode.B))
        {
            TakeDamage(Random.Range(5, 20));
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            RestoreArmor(Random.Range(5, 20));
        }
    }
    public void UpdateArmorUI()// ������ � ����� ��
    {
        float fillFront = ArmorBarFront.fillAmount;
        float fillBack = ArmorBarBack.fillAmount;
        float hFraction = armor / maxArmor;
        if (fillBack > hFraction) // �������� �� � ���������� ����
        {
            ArmorBarFront.fillAmount = hFraction;
            ArmorBarBack.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            ArmorBarBack.fillAmount = Mathf.Lerp(fillBack, hFraction, percentComplete);
        }
        if (fillFront < hFraction) // ���������� �� � ���������� ����
        {
            ArmorBarBack.color = Color.green;
            ArmorBarBack.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            ArmorBarFront.fillAmount = Mathf.Lerp(fillFront, ArmorBarBack.fillAmount, percentComplete);
        }

    }
    public void RestoreArmor(float healAmount)// ��������� ��
    {
        armor += healAmount;
        lerpTimer = 0f;
    }
    public void TakeDamage(float damage)// �������� ��
    {
        armor -= damage;
        lerpTimer = 0f;
    }
}
