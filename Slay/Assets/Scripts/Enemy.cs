using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Attack;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;
using Image = UnityEngine.UI.Image;
using System.Collections;
using static UnityEngine.Rendering.DebugUI;

public class Enemy : Unit
{
    public int level;
    public List<A1Class> a1class;
    public Player player;
    public GameManager gameManager;
    public Image enemyActImage;
    public TextMeshProUGUI enemyActText;
    A1Class currentA1;
    public GameObject panel;
    protected override void Start()
    {
        base.Start();

        a1class = new List<A1Class>();
        a1class.Add(new A1Class(12, Type.Attack, Special.None));
        a1class.Add(new A1Class(14, Type.Attack, Special.None));
        a1class.Add(new A1Class(16, Type.Attack, Special.None));
        currentA1 = a1class[Random.Range(0, a1class.Count)];
    }
    
    // �� ���� �̺�Ʈ �����س��� ������ �˸� ���� ������ �Լ�
    public void Attack()
    {

        // �ִϸ��̼� ����
        // �÷��̾� ä�� ���� or �� or �� ���� a1class�� ���� ������ �����Ұ���
        animator.SetTrigger(currentA1.type.ToString());
        player.TakeDamage(currentA1.value,1,gameObject);
        gameManager.OnTurnStart();
        GameManager.isClick = false;
        
    }

    public override void OnDie()
    {
        base.OnDie();
        enemyActImage.gameObject.SetActive(false);
        enemyActText.gameObject.SetActive(false);
        panel.SetActive(true);
    }

    public void Set()
    {
        currentA1 = a1class[Random.Range(0, a1class.Count)];
        enemyActText.text = currentA1.value.ToString();
    }

    //public void TakeDamage(int damage)
    //{
    //    animator.SetTrigger("Damaged");
    //    if (hpSlider == null)
    //        Debug.LogWarning("Slider�� ������� �ʾҾ��!");
        
    //    hp -= damage;
    //    hpSlider.value = hp / (float)maxHp;
    //    if (hp <= 0)
    //    {
    //        OnDie();
    //    }
    //    enemyHpText.text = (hp + "/" + maxHp).ToString();
    //    StartCoroutine(C_OnDamaged());
    //    enemyDamagedText.text = damage.ToString();
    //}

    

    
}

