using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Statistics))]
[RequireComponent(typeof(Animator))]
public class Movement : MonoBehaviour {
    [Header("Rotation")]
    public float sensitivity = 2.0f;
    private bool allowRotation = true;
    private Vector3 movement;
    
    private Statistics statistics;
    private Rigidbody rb;
    private Animator animator;

    void Start() {
        this.statistics = this.GetComponent<Statistics>();
        this.rb = this.GetComponent<Rigidbody>();
        this.animator = this.GetComponent<Animator>();
    }

    void Update() {
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");   
        
        Rotation();

        animator.SetBool("moving", movement.z != 0 || movement.x != 0);
        animator.SetInteger("direction", -(int)movement.x);
    }

    void FixedUpdate() {
        Move();
    }

    /// <summary>
    /// Rotates the player towards the position of the mouse cursor.
    /// </summary>
    void Rotation() {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.y));

        Vector3 directionToMouse = worldMousePosition - transform.position;

        float angle = Mathf.Atan2(directionToMouse.x, directionToMouse.z) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    /// <summary>
    /// Moves the player on the map. 
    /// Uses transform.right and transform.forward properties to make sure the player moves according to its rotation.
    /// </summary>
    void Move() {
        rb.MovePosition(rb.position + statistics.speed * Time.fixedDeltaTime * movement);
    }
}
