using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    // HpBar Slider�� �����ϱ� ���� Slider ��ü
    [SerializeField] private Slider _hpBar;
    [SerializeField] CDistanceEnemy _body;
    // ������Ʈ�� HP
    private int _hp;
    private Rigidbody2D rb;

    public int Hp
    {
        get => _hp;
        // HP�� HPController������ ���� �ϵ��� private���� ó��
        // Math.Clamp �Լ��� ����ؼ� hp�� 0���� �Ʒ��� �������� �ʰ� ��
        private set => _hp = Mathf.Clamp(value, 0, _hp);
    }

    private void Start()
    {
        // �÷��̾��� HP ���� �ʱ� ����
        _hp = 100;
        SetMaxHealth(_hp);
        rb = GetComponent<Rigidbody2D>();
        _body = GetComponent<CDistanceEnemy>();

    }

    public void SetMaxHealth(int health)
    {
        _hpBar.maxValue = health;
        _hpBar.value = health;
    }

    // �÷��̾ ������� ������ ����� ���� ���� �޾� HP�� �ݿ�
    public void GetDamage(int damage, Vector3 attackerPosition)
    {
        int getDamagedHp = Hp - damage;
        Hp = getDamagedHp;
        _hpBar.value = Hp;

        // �ǰݴ��� ���� ��� (�� -> �÷��̾��� �ݴ� �������� �з���)
        Vector2 knockbackDirection = (transform.position - attackerPosition).normalized;

        // ���� Rigidbody2D�� ���� ���� �з����� ��
        _body.ApplyKnockback(knockbackDirection);

        if (Hp <= 0)
        {
            Destroy(gameObject);
        }

    }
}
