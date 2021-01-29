using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Human : MonoBehaviour
{
    [SerializeField] private Transform _fixationPoint;    
    public Transform FixationPoint => _fixationPoint;    

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Running(bool value)
    {
        _animator.SetBool("isRunning", value);
    }

    public void Texting(bool value)
    {
        _animator.SetBool("isTexting", value);
    }

}
