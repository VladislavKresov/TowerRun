using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingChecker : MonoBehaviour
{
    [SerializeField] private PlayerTower _playerTower;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Human human))
        {
            Tower collisionTower = human.GetComponentInParent<Tower>();
            if (collisionTower != null)
            {
                _playerTower.CollectHumans(collisionTower);
            }
        }
    }
}
