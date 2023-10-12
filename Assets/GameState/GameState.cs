using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class GameState : MonoBehaviour
{
    public Transform _playerTemplate;
    private Transform _player;
    private IController _currentController;
    public Transform Player { get => _player; }
    public IController CurrentController { get => _currentController; }


    // Start is called before the first frame update
    void Awake()
    {
        EntityHealth.OnAnyEntityDeath += GameOver;

        SpawnPlayer();
        SetCameraFollowPlayer();
        _currentController = _player.GetComponent<PlayerController>();
    }

    private void SpawnPlayer()
    {
        if (_player != null)
        {
            Destroy(_player);
        }
        _player = Instantiate(_playerTemplate);
    }

    private void SetCameraFollowPlayer()
    {
        ParentConstraint camConstraint;
        if(!Camera.main.TryGetComponent<ParentConstraint>(out camConstraint))
        {
            camConstraint = Camera.main.AddComponent<ParentConstraint>();
            camConstraint.AddSource(new ConstraintSource() { sourceTransform = _player, weight = 1 });
        }
        else 
        {
            camConstraint.SetSource(0, new ConstraintSource() { sourceTransform = _player, weight = 1 });
        }
        camConstraint.translationAxis = Axis.X | Axis.Y;
        camConstraint.constraintActive = true;
    }

    public void GameOver(object sender, HitData data)
    {
        if(data.DamageReceiver.transform.root.CompareTag("Crystal"))
        {
            Debug.Log("GameOver");
        }
    }

}
