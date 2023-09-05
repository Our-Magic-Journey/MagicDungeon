using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TopDownCamera : MonoBehaviour {
    public Transform player;
    public float cameraLag = 0.1f;
    private Vector3 idealOffset;

    void Start() {
        idealOffset = transform.position - player.position;
    }

    void Update() {        
        this.transform.position = Vector3.Lerp(this.transform.position, player.position + idealOffset, cameraLag * Time.deltaTime); 
    }
}
