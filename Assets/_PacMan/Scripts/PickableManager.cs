using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    public List<Pickable> _pickables;

    private void Start()
    {
        InitPickableList();
    }

    private void InitPickableList()
    {
        Pickable[] pickableObjects = FindObjectsOfType<Pickable>();
        
        for (int i = 0; i < pickableObjects.Length; i++)
        {
            _pickables.Add(pickableObjects[i]);
            pickableObjects[i].OnPicked += OnPickablePicked;
        }
        Debug.Log("Pickable List: "+_pickables.Count);
    }
    
    private void OnPickablePicked(Pickable pickable)

    {
        _pickables.Remove(pickable);
        Destroy(pickable.gameObject);
        
        Debug.Log("Pickable List: " + _pickables.Count);

        if (_pickables.Count <= 0)
        {
            Debug.Log("Win");
        }
        
        if (pickable._PickableType == PickableType.PowerUp)
        {
            _player?.PickPowerUp();
        }
    }
}
