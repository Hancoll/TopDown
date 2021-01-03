using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AttackMove : MonoBehaviour
{
    private new Rigidbody2D rigidbody;

    private void Awake() => rigidbody = GetComponent<Rigidbody2D>();

    public void Move(Vector2 moveDirection, float velocity)
    {
        Vector2 velocityVector = moveDirection * velocity;
        rigidbody.MovePosition((Vector2)transform.position + velocityVector * Time.deltaTime);
    }
}
