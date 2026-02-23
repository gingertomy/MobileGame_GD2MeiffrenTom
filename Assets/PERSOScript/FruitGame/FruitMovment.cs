using System;
using UnityEngine;

public class FruitMovement : MonoBehaviour
{
    [SerializeField] private Transform[] _transforms;
   
    public Transform[] GetPath() 
    { 
        return _transforms; 
    }
    
}