using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private SceneController _sceneController;

    [SerializeField] private ScoreManager _scoreManager;
    public List<Pickable> _pickables;

    private void Start()
    {
        InitPickableList();
    }

    private void OnDestroy()
    {
        Pickable[] pickableObjects = FindObjectsOfType<Pickable>();
        
        for (int i = 0; i < pickableObjects.Length; i++)
        {
            _pickables.Add(pickableObjects[i]);
            pickableObjects[i].OnPicked -= OnPickablePicked;
        }
    }

    private void InitPickableList()
    {
        Pickable[] pickableObjects = FindObjectsOfType<Pickable>();
        
        for (int i = 0; i < pickableObjects.Length; i++)
        {
            _pickables.Add(pickableObjects[i]);
            pickableObjects[i].OnPicked += OnPickablePicked;
        }
        _scoreManager.SetMaxScore(_pickables.Count);
        Debug.Log("Pickable List: "+_pickables.Count);
    }
    
    private void OnPickablePicked(Pickable pickable)
    {
        Destroy(pickable.gameObject);

        _pickables.Remove(pickable);
        
        Debug.Log("Pickable List: " + _pickables.Count);

        if (_pickables.Count <= 0)
        {
            Debug.Log("Win");
            _sceneController.WinScene();
        }
        
        if (pickable._PickableType == PickableType.PowerUp)
        {
            _player?.PickPowerUp();
        }
        _scoreManager.AddScore(1);
        

    }
}
