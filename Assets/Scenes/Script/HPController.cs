using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    // HpBar Slider를 연동하기 위한 Slider 객체
    [SerializeField] private Slider _hpBar;
    [SerializeField] CDistanceEnemy _body;
    // 오브젝트의 HP
    private int _hp;
    private Rigidbody2D rb;

    public int Hp
    {
        get => _hp;
        // HP는 HPController에서만 수정 하도록 private으로 처리
        // Math.Clamp 함수를 사용해서 hp가 0보다 아래로 떨어지지 않게 함
        private set => _hp = Mathf.Clamp(value, 0, _hp);
    }

    private void Start()
    {
        // 플레이어의 HP 값을 초기 세팅
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

    // 플레이어가 대미지를 받으면 대미지 값을 전달 받아 HP에 반영
    public void GetDamage(int damage, Vector3 attackerPosition)
    {
        int getDamagedHp = Hp - damage;
        Hp = getDamagedHp;
        _hpBar.value = Hp;

        // 피격당한 방향 계산 (적 -> 플레이어의 반대 방향으로 밀려남)
        Vector2 knockbackDirection = (transform.position - attackerPosition).normalized;

        // 적의 Rigidbody2D에 힘을 가해 밀려나게 함
        _body.ApplyKnockback(knockbackDirection);

        if (Hp <= 0)
        {
            Destroy(gameObject);
        }

    }
}
