using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Gun : NetworkBehaviour
{
    AudioSource shotSound;
    Transform shotPoint;
    public GameObject bulletPrefab;
    public float reloadTime = 0.5f;
    public float lastTimeShot = 0f;

    //public float bulletForce = 20f;
    //public float bulletSpeed = 10f;
    // public Rigidbody bullet;
    // Start is called before the first frame update

    public override void OnNetworkSpawn()
    {
        // if(!IsOwner){
        //     Destroy(this);
        // }
    }

    void Start()
    {
        if (!IsOwner)
        {
            return;
        }
        shotSound = GetComponent<AudioSource>();
        shotPoint = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        if (lastTimeShot < reloadTime)
        {
            lastTimeShot += Time.deltaTime;
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                shotSound.Play();
                // if (IsServer)
                // {
                //     Debug.Log("is server trying to fire");
                //     fire();
                // }
                // else
                // {
                Debug.Log("else trying to fire");
                fireServerRpc(shotPoint.position, shotPoint.rotation);
                lastTimeShot = 0;
                // }
            }
        }


    }

    [ServerRpc(RequireOwnership = false)]
    private void fireServerRpc(Vector3 position, Quaternion rotation)
    {
        // Debug.Log("serverRpc");
        // fire();

        Debug.Log("actually trying to fire");
        // if (!IsOwner)
        // {
        //     return;
        // }
        Debug.Log("actually firing");
        // Debug.Log(shotSound);
        Debug.Log(bulletPrefab);
        // Debug.Log(shotPoint);
        // Debug.Log(shotPoint.position);
        // Debug.Log(shotPoint.rotation);

        GameObject go = Instantiate(bulletPrefab, position, rotation);
        // GameObject go = Instantiate(bulletPrefab);
        go.GetComponent<NetworkObject>().Spawn();

    }

    private void fire()
    {
        Debug.Log("actually trying to fire");
        // if (!IsOwner)
        // {
        //     return;
        // }
        Debug.Log("actually firing");
        // Debug.Log(shotSound);
        Debug.Log(bulletPrefab);
        // Debug.Log(shotPoint);
        // Debug.Log(shotPoint.position);
        // Debug.Log(shotPoint.rotation);

        // GameObject go = Instantiate(bulletPrefab, shotPoint.position, shotPoint.rotation);
        GameObject go = Instantiate(bulletPrefab);
        go.GetComponent<NetworkObject>().Spawn();
        lastTimeShot = 0;

    }
}
