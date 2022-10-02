using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Player : NetworkBehaviour
{
    Rigidbody2D rb;

    AudioSource stepSound;
    float horizontal;
    float vertical;

    public float walkSpeed = 2.0f;
    public float runSpeed = 3.0f;
    public float speed = 0;
    bool idle = true;

    public override void OnNetworkSpawn()
    {
        // if(!IsOwner){
        //     Destroy(this);
        // }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!IsOwner){
            return;
        }
        rb = GetComponent<Rigidbody2D>();
        stepSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsOwner){
            return;
        }
        move();
       
        checkRun();
    }

    private void move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (rb.velocity.x != 0 || rb.velocity.y != 0) 
        {
            idle = false;
        }
        else
        {
            idle = true;
        }

        if (idle)
        {
            stepSound.Stop();
        }
        else
        {
            if (!stepSound.isPlaying)
            {
                stepSound.Play();
            }
        }
    }

    private void checkRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
            stepSound.pitch = runSpeed / walkSpeed;
        }
        else
        {
            speed = walkSpeed;
            stepSound.pitch = walkSpeed / walkSpeed;
        }
    }
    private void FixedUpdate()
    {
        if(!IsOwner){
            return;
        }
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(!IsOwner){
            return;
        }
        if (col.tag == "door")
        {
            if (Input.GetKey(KeyCode.E)){
                Debug.Log("Oi");
                // col.transform.parent.gameObject.GetComponent<NetworkObject>().Despawn();
                // Destroy(col.transform.parent.gameObject);
                GameObject door = col.transform.gameObject;
                door.GetComponent<Door>().OpenDoor();
                
            }
        }
    }

    [ClientRpc]
    private void OpenDoorClientRpc(){

    }
}
