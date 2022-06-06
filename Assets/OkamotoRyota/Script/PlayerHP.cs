using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public int HP;//playersHP
    public int HPmax = 3;//PlayerMAXHP = 3
    [Tooltip("Slider"), SerializeField] Slider _hpSlider = default;
    [Tooltip("InfoText に表示する Text"), SerializeField] Text _uiText;
    public Text UiText { get => _uiText; set => _uiText = value; }

    bool _dieFlag = false;

    void Start()
    {
        HP = HPmax;//最初にplayerのHPをmaxの状態にする
        _hpSlider.maxValue = HPmax;
        _hpSlider.value = HP;
    }

    public void OnDamage(int _damage)//ダメージを受けた場合に実行させる処理
    {
        HP -= _damage;
        UiText.text = $"{_damage}ダメージ受けた！".ToString();
        _hpSlider.value -= _damage;
        if(HP <= 0 && !_dieFlag)
        {
            HP = 0;//もしHPがダメージを受け、HPが0以下になった場合0にする
            GameManager.Instance.GameOver();
            _dieFlag = true;
        }
    }

    public void Heal(int healpoints)
    {
        Debug.Log(HP);
        if(HP < HPmax)
        {
            HP += healpoints;
            _hpSlider.value += healpoints;
            UiText.text = $"{healpoints}回復した";
        }
        else if(HP >= HPmax)
        {
            UiText.text = "HPは既に最大です";
            return;
        }
    }
}
