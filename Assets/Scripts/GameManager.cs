using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameManager _player;
    private PlayerModel _playerModel;
    private PlayerPresenter _playerPresenter;
    private PlayerView _playerView;
    [SerializeField] private static SceneLoader _sceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        var playerObject = Instantiate(_player, Vector3.zero, Quaternion.identity);
        var playerView = playerObject.GetComponent<PlayerView>();

        _playerPresenter = new PlayerPresenter(_playerView, _playerModel);
    }


    public class PlayerPresenter
    {
        private PlayerView _playerView;
        private PlayerModel _playerModel;


        public PlayerPresenter(PlayerView playerView, PlayerModel playerModel)
        {
            _playerView = playerView;
            _playerModel = playerModel;
        }

        public void Enamble()
        {
            _playerModel.ChangeHealth += _playerView.ChangeHealth;
            _playerModel.Death += _playerView.Death;
        }
    }

    public class PlayerModel

    {
        public event Action Death;
        public event Action<float> ChangeHealth;
        private float _maxHP = 100;
        private float _currentHp;

        public PlayerModel()
        {
            _currentHp = _maxHP;
        }

        public void SetNewhealth(float damage)
        {
            _currentHp -= damage;
            if (_currentHp > 0)
            {
                ChangeHealth?.Invoke(_currentHp);
            }
            else
            {
                Death?.Invoke();
            }
        }
    }

    public class PlayerView
    {
        private float _currentHp;
        private string _loadSceneName;

        public void ChangeHealth(float hp)
        {
            float newHp = hp - 1;
        }


        public void Death()
        {
            _sceneLoader.LoadScene(_loadSceneName);
        }
    }
}