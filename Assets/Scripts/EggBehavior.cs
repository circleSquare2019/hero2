﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBehavior : MonoBehaviour
{
    private static EggSpawnSystem sEggSystem = null;
    public static void InitializeEggSystem(EggSpawnSystem e) { sEggSystem = e; }

    private const float kEggSpeed = 40f;

    void Start()
    {
    }


    void Update()
    {
        transform.position += transform.up * (kEggSpeed * Time.smoothDeltaTime);

        bool outside = GameManager.sTheGlobalBehavior.CollideWorldBound(GetComponent<Renderer>().bounds) == CameraSupport.WorldBoundStatus.Outside;
        if (outside)
        {
            DestroyThisEgg("Self");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Hero") 
            DestroyThisEgg(collision.gameObject.name);
    }

    private void DestroyThisEgg(string name)
    {
        if (gameObject.activeSelf)
        {
            sEggSystem.DecEggCount();
            gameObject.SetActive(false); 
            Destroy(gameObject);
        } else
        {
            Debug.Log("Calling Egg Destroy on a destroyed egg: " + name);
        }
    }
}