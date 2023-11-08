using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]  GameObject deathFX;
    [SerializeField]  GameObject hitVfx;
    [SerializeField]  int hitPoint = 3;
    [SerializeField]  int scorePerHit = 5;
    private ScoreBoard scoreBoard;
     GameObject parentGameObject;



     void Start()
     {
         scoreBoard = FindObjectOfType<ScoreBoard>();
         parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
         AddRigidBody();
     }

      void AddRigidBody()
     {
         Rigidbody rb = gameObject.AddComponent<Rigidbody>();
         rb.useGravity = false;
     }

     void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoint < 1)
        {
              KillEnemy();
        }
      
   }

    void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVfx, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        hitPoint --;
        scoreBoard.IncreaseScore(scorePerHit);  
    }

    void KillEnemy()
    {
        GameObject vfx =  Instantiate(deathFX, transform.position,Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }
}
