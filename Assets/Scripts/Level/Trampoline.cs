using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float _force;

    private void OnTriggerEnter(Collider other)
    {        
        if(other.gameObject.TryGetComponent(out PlayerTower playerTower))
        {
            playerTower.GetComponent<Rigidbody>().AddForce(Vector3.up * _force);            
        }
    }
}
