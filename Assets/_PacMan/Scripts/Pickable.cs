using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField]
    public PickableType _PickableType;
    
    public Action<Pickable> OnPicked;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(_PickableType + " Triggerd with Player");
            
            if(OnPicked != null)
            {
                OnPicked(this);
            }
        }
    }
}
