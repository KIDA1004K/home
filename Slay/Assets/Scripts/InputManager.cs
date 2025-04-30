using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    // ���콺 ���� Ŭ�� �� ī�� ��ġ �߾����� + Ŀ�� + �����̽��� ������ ī�� ��
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Camera _camera;
    bool _isCardClick = false;
    RaycastHit2D hit;
    Vector2 OrignPosition;
    public GameManager gameManager;
    
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isCardClick == false)
        {
            OnMouseLeftDonw();
        }
        if (Input.GetMouseButtonDown(1) && _isCardClick)
        {
            OnMouseRightDonw();
        }
        if (Input.GetKeyDown(KeyCode.Space) && _isCardClick)
        {
            Debug.Log("�����̽��� ����");
            Card card = hit.transform.GetComponent<Card>();
            if (card != null)
            {
                _isCardClick = false;
                Debug.Log("����");
                if (gameManager.attack(card) == false)
                {
                    OnMouseRightDonw();
                }
                GameManager.isClick = false;
            }
        }
    }

   
    public void OnMouseLeftDonw()
    {

        Vector2 ray = _camera.ScreenToWorldPoint(Input.mousePosition);
        hit = Physics2D.Raycast(ray, Vector2.zero);
        if (hit == true && hit.collider.CompareTag("Card"))
        {
            _isCardClick = true;
            GameManager.isClick = true;
            OrignPosition = hit.transform.position;
            Debug.Log(OrignPosition);
            hit.transform.position = Vector2.up;
            hit.transform.localScale = Vector2.one * 1.5f;
            Debug.Log(OrignPosition);

        }
    }

    public void OnMouseRightDonw()
    {
        Debug.Log(OrignPosition);
        _isCardClick = false;
        hit.transform.position = OrignPosition;
        hit.transform.localScale = new(1.5f,1.5f);
    }
}
