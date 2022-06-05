using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[DisallowMultipleComponent]
public class Enemy : MonoBehaviour
{
    [SerializeField, Tooltip("�̗�")] int _hp = 1;
    [SerializeField, Tooltip("�ړ����x")] float _moveSpeed = 1f;
    [SerializeField, Tooltip("�U���Ԋu")] float _attackInterval = 1f;
    Rigidbody2D _rb2D => GetComponent<Rigidbody2D>();

    void Start()
    {
        var index = Random.Range(0, 8);
        Move((MoveDirection)index);
        StartCoroutine(Attack());
    }

    /// <summary>�_���[�W���󂯂�@�̗͂�0�ȉ��ɂȂ�����폜����� </summary>
    /// <param name="damage">�󂯂�_���[�W</param>
    public void GetDamage(int damage)
    {
        _hp -= damage;

        if (_hp <= 0)
        {
            Destroy(gameObject);
            GameManager.Instance.ChangeEnemyCount();
        }
    }

    /// <summary>��莞�Ԍo�߂�����v���C���[�Ƀ_���[�W��^���� </summary>
    /// <returns></returns>
    public IEnumerator Attack()
    {
        while (true)
        {
            //�_���[�W��^����
            yield return new WaitForSeconds(_attackInterval);
        }
    }

    /// <summary>�������ꂽ�Ƃ��ɔ�ԕ��������߂� </summary>
    /// <param name="direction">��ԕ���</param>
    private void Move(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.Up:
                _rb2D.AddForce(Vector2.up * _moveSpeed, ForceMode2D.Impulse);
                break;
            case MoveDirection.UpperRight:
                _rb2D.AddForce(new Vector2(1, 1).normalized * _moveSpeed, ForceMode2D.Impulse);
                break;
            case MoveDirection.Right:
                _rb2D.AddForce(new Vector2(1, 0).normalized * _moveSpeed, ForceMode2D.Impulse);
                break;
            case MoveDirection.BottomRight:
                _rb2D.AddForce(new Vector2(1, -1).normalized * _moveSpeed, ForceMode2D.Impulse);
                break;
            case MoveDirection.Down:
                _rb2D.AddForce(new Vector2(0, -1).normalized * _moveSpeed, ForceMode2D.Impulse);
                break;
            case MoveDirection.BottomLeft:
                _rb2D.AddForce(new Vector2(-1, -1).normalized * _moveSpeed, ForceMode2D.Impulse);
                break;
            case MoveDirection.Left:
                _rb2D.AddForce(new Vector2(-1, 0).normalized * _moveSpeed, ForceMode2D.Impulse);
                break;
            case MoveDirection.UpperLeft:
                _rb2D.AddForce(new Vector2(-1, 1).normalized * _moveSpeed, ForceMode2D.Impulse);
                break;

        }
    }
}

/// <summary>��ԕ��� </summary>
public enum MoveDirection
{
    Up = 0,             //��
    UpperRight = 1,     //�E��
    Right = 2,          //�E
    BottomRight = 3,    //�E��
    Down = 4,           //��
    BottomLeft = 5,     //?����
    Left = 6,           //?��
    UpperLeft = 7,      //?����
}