using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private Vector2 _scaleRangeY;

    private int _height;

    private void Start()
    {
        _height = (int) Random.Range(_scaleRangeY.x, _scaleRangeY.y);
        Vector3 newScale = transform.localScale;
        newScale.y *= _height;
        transform.localScale = newScale;
        
        Vector3 newPosition = transform.position;
        newPosition.y += (transform.localScale / 2).y;

        transform.position = newPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerTower playerTower))
        {
            playerTower.DestroyHumans(_height);
            Destroy(gameObject);
        }
    }
}
