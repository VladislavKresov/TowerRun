using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTower : MonoBehaviour
{
    [SerializeField] private Human _startHuman;
    [SerializeField] private Transform _distanceChecker;
    [SerializeField] private float _fixationMaxDistance;
    [SerializeField] private BoxCollider _checkCollider;

    private List<Human> _humans;

    public event UnityAction<int> HumanAdded;

    private void Start()
    {
        _humans = new List<Human>();
        _humans.Add(Instantiate(_startHuman, transform.position, Quaternion.identity, transform));
        _humans[0].Running(true);
        HumanAdded?.Invoke(_humans.Count);
    }    

    public void CollectHumans(Tower collisionTower)
    {
        _humans[0].Running(false);
        List<Human> collectedHumans = collisionTower.CollectHuman(_distanceChecker, _fixationMaxDistance);

        if (collectedHumans != null)
        {
            _humans.InsertRange(0, collectedHumans);
            Build();
            HumanAdded?.Invoke(_humans.Count);
        }
        collisionTower.Break();
        _humans[0].Running(true);
    }

    private void Build()
    {
        for (int i = 0; i < _humans.Count; i++)
        {
            Human currentHuman = _humans[i];
            currentHuman.transform.parent = transform;
            Vector3 currentPosition = transform.position;
            currentPosition.y += i * currentHuman.FixationPoint.localPosition.y;
            currentHuman.transform.position = currentPosition;
            currentHuman.transform.localRotation = Quaternion.identity;
            if (i < _humans.Count - 1)
                currentHuman.Texting(Random.Range(0, 2) == 1);
        }
    }

    public void DestroyHumans(int count)
    {
        if(_humans.Count < count)        
            count = _humans.Count;

        for (int i = 0; i < count; i++)
        {
            _humans[i].transform.parent = null;
            _humans[i].Running(false);
            _humans[i].Texting(false);
            Rigidbody rigidbody = _humans[i].GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
            rigidbody.freezeRotation = false;            
            Vector3 randomOffset = new Vector3(Random.Range(-1, 1), Random.Range(0, 2), Random.Range(-1, 0));
            rigidbody.AddExplosionForce(10, _humans[i].transform.position + randomOffset, 10, -1f, ForceMode.Impulse);           
        }
        _humans.RemoveRange(0, count);


        Build();
    }
}
