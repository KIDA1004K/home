using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class Unit : MonoBehaviour
{

    public int shield;
    public int MaxHp;
    private int hp;
    public int str = 0;

    public TextMeshProUGUI UnitDamagedText;
    public TextMeshProUGUI shieldText;
    public TextMeshProUGUI UnitHpText;
    public UnityEngine.UI.Slider slider;
    public UnityEngine.UI.Image shieldImage;
    protected Animator animator;
    public InGameDeck inGameDeck;
    



    protected virtual void Start()
    {
        hp = MaxHp;
        slider.value = hp / (float)MaxHp;
        shield = 0;
        UnitHpText.text = hp.ToString() + "/" + MaxHp.ToString();
        animator = GetComponent<Animator>();
    }

    IEnumerator C_OnDamaged()
    {

        Color color = UnitDamagedText.color;
        float duration = 1.0f;
        float time = 0f;

        while (time < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, time / duration);
            UnitDamagedText.color = new Color(color.r, color.g, color.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        // 마지막 알파값 보정
        UnitDamagedText.color = new Color(color.r, color.g, color.b, 0f);
    }

    public virtual void GetPower(int value)
    {
        GameManager.playerStr += value;
        //foreach (GameObject cardObj in GameManager.CardList)
        //{
        //    if (cardObj.GetComponent<Card>().type == TypeCard.Attack)
        //    {
        //        cardObj.GetComponent<Card>().valueText += GameManager.playerStr;
        //        cardObj.GetComponent<Card>().ChangeText();
        //    }
        //}
        ////inGameDeck.AddP();
        GameManager.CardsRP();
    }



    public void TakeDamage(int dmg, int count, GameObject attacker)
    {
        int damage = (dmg + GameManager.playerStr) * count;
        animator.SetTrigger("Damaged");
        if (shield > 0)
        {
            if (shield > damage) // 방어막이 데미지보다 크거나 같으면
            {
                shield -= damage; // 방어막 차감
                damage = 0; // 남은 데미지는 0
            }
            else
            {
                shieldImage.gameObject.SetActive(false);
                shieldText.text = "";
                shieldText.gameObject.SetActive(false);
                damage -= shield; // 방어막이 데미지보다 작으면 남은 데미지
                shield = 0; // 방어막은 모두 소진
            }
        }

        // 방어막 이후 남은 데미지를 체력에 적용
        if (damage > 0)
        {
            hp -= damage; // 체력 차감
            slider.value = hp / (float)MaxHp; // 체력 슬라이더 업데이트
        }

        UnitHpText.text = (hp.ToString() + "/" + MaxHp.ToString());
        if (count > 1)
        {
            UnitDamagedText.text = $"{dmg + GameManager.playerStr} x {count}".ToString();

        }
        else
        {
            UnitDamagedText.text = damage.ToString();
        }
            

        if (hp <= 0)
        {
            OnDie();
        }
        else
        {
            StartCoroutine(C_OnDamaged());
        }
        
    }

        public void GetShield(int value)
    {


        shield += value;
        if (slider.value > 0)
        {
            shieldImage.gameObject.SetActive(true);
            shieldText.gameObject.SetActive(true);
        }
        shieldText.text = shield.ToString();

    }
    public void OnStartTurn()
    {
        shield = 0;
        shieldText.text = "";
    }
    public virtual void OnDie()
    {
        gameObject.SetActive(false);
        shieldImage.gameObject.SetActive(false);
        shieldText.gameObject.SetActive(false);
        UnitHpText.gameObject.SetActive(false);
        slider.gameObject.SetActive(false);
        shieldImage.gameObject.SetActive(false);
        UnitDamagedText.gameObject.SetActive(false);
    }
}
