using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = default;
    [SerializeField, Tooltip("各ステージの敵の数")] int[] _stageEnemyCount = default;
    [SerializeField, Tooltip("フェードをさせるオブジェクト")] GameObject _fadeObj = default;
    [SerializeField, Tooltip("プレイヤーのプレハブ")] GameObject _playerPrefab = default;
    PlayerHP _playerHP;
    Attackspace _attackspace;
    /// <summary>現在のステージ </summary>
    int _currentStage = 0;
    public PlayerHP PlayerHP { get => _playerHP; }
    public Attackspace Attackspace { get => _attackspace; }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
            SceneManager.sceneLoaded += GameSceneLoad;
            DontDestroyOnLoad(this);
        }
    }
    void Start()
    {
        
    }

    /// <summary>遷移した時にプレイヤーを生成する </summary>
    public void GameSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            var player = Instantiate(_playerPrefab, Vector2.zero, Quaternion.identity);
            _playerHP = player.GetComponent<PlayerHP>();
            _attackspace = player.GetComponent<Attackspace>();
        }
    }

    /// <summary>敵を倒した時に呼ばれる ステージ内に存在する敵の数を減らす</summary>
    public void ChangeEnemyCount()
    {
        _stageEnemyCount[_currentStage]--;      //敵の数を減らす

        if (_stageEnemyCount[_currentStage] <= 0) //ステージクリア
        {
            _currentStage++;
        }
    }

    /// <summary>フェードオブジェクトを作成する </summary>
    public void InstantiateFadeObj()
    {
        Instantiate(_fadeObj);
    }
}
