using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCrisps : MonoBehaviour
{
    [SerializeField]
    private GameObject crisps;

    [SerializeField]
    private Vector3 position;

    public void Spawn()
    {
        Instantiate(crisps).transform.position=position;
    }
}
