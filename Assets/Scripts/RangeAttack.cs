﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    // Reference to arrow prefab (will be instantiated on attack)
    [SerializeField] private GameObject missileObjectPrefab;
    private Rigidbody missileBody;

    // movement variables
    private Movement movementScript;
    private float left = -1;
    private float right = 1;
    
    // target variables
    private Transform target;

    // Cooldown between attack
    public float defaultCooldown = 3;
    private float currentCooldown;

    void Start()
    {
        // Start cooldown at default value
        currentCooldown = defaultCooldown;

        // Initialize target (default will be player)
        target = GameObject.FindWithTag("Player").transform;

        // Initialize movement script
        movementScript = gameObject.GetComponent<Movement>();
    }

    void Update()
    {
        // Reduce cooldown every frame
        currentCooldown -= Time.deltaTime;
        
        // Applies cooldown
        if (currentCooldown <= 0)
        {
            rangedAttack();

            // After ranged attack, reset cooldown
            currentCooldown = defaultCooldown;
        }

        // If target is to the left, move left
        if (target.position.x < transform.position.x)
        {
            movementScript.move(left);
        }

        // If target is to the right, move right
        else
        {
            movementScript.move(right);
        }
    }

    void rangedAttack()
    {   
        // Instantiate the missile at the beginning of the attack
        missileBody = Instantiate(missileObjectPrefab, transform.position, transform.rotation).GetComponent<Rigidbody>();
        
        // Set missile velocity using the trajectory script
        missileBody.velocity = HitTargetAtTime(transform.position, target.position, new Vector3(0f, -9.81f, 0f), 1f);
        
        // Flip depending on where the missile is going
        if (missileBody.velocity.x < 0)
        {
            missileBody.GetComponent<SpriteRenderer>().flipX = true;
        }
    }


    // Trajectories thanks to: https://answers.unity.com/questions/1087568/3d-trajectory-prediction.html#answer-1087707
    public static Vector3 HitTargetAtTime(Vector3 startPosition, Vector3 targetPosition, Vector3 gravityBase, float timeToTarget)
    {
        Vector3 AtoB = targetPosition - startPosition;
        Vector3 horizontal = GetHorizontalVector(AtoB, gravityBase);
        float horizontalDistance = horizontal.magnitude;
        Vector3 vertical = GetVerticalVector(AtoB, gravityBase);
        float verticalDistance = vertical.magnitude * Mathf.Sign(Vector3.Dot(vertical, -gravityBase));
    
        float horizontalSpeed = horizontalDistance / timeToTarget;
        float verticalSpeed = (verticalDistance + ((0.5f * gravityBase.magnitude) * (timeToTarget * timeToTarget))) / timeToTarget;
    
        Vector3 launch = (horizontal.normalized * horizontalSpeed) - (gravityBase.normalized * verticalSpeed);
        return launch;
    }

    public static Vector3 GetHorizontalVector(Vector3 AtoB, Vector3 gravityBase)
    {
        Vector3 output;
        Vector3 perpendicular = Vector3.Cross(AtoB, gravityBase);
        perpendicular = Vector3.Cross(gravityBase, perpendicular);
        output = Vector3.Project(AtoB, perpendicular);
        return output;
    }
    
    public static Vector3 GetVerticalVector(Vector3 AtoB, Vector3 gravityBase)
    {
        Vector3 output;
        output = Vector3.Project(AtoB, gravityBase);
        return output;
    }
}