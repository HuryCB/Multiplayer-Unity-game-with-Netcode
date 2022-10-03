using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Bullet : NetworkBehaviour
{
    // Start is called before the first frame update
    float horizontal;
    float vertical;
    public GameObject blood;
    Transform bulletPosition;
    Rigidbody2D rb;
    public float speed = 40f;
    public float lifeTime = 5f;

    private readonly NetworkVariable<Vector3> netPos = new(writePerm: NetworkVariableWritePermission.Owner);
    private readonly NetworkVariable<Quaternion> netRotation = new(writePerm: NetworkVariableWritePermission.Owner);


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletPosition = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
        }
        else
        {
            if(IsServer){
                Destroy(this.gameObject);
            }
            // DestroyServerRpc();
            // Destroy(t)
            return;
        }
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (IsOwner)
        {
            netPos.Value = transform.position;
            netRotation.Value = transform.rotation;
        }
        else
        {
            transform.position = netPos.Value;
            transform.rotation = netRotation.Value;
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // if (hitInfo.tag != "activateBox")
        // {

        // }

        if (hitInfo.tag == "enemy")
        {
            hitInfo.GetComponent<Enemy>().damage();
             if(IsServer){
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "activateBox")
        {

        }

        if (collision.gameObject.CompareTag("enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().damage();
            if(IsServer){
                Destroy(this.gameObject);
            }
        }
    }

    // [ServerRpc(RequireOwnership = false)]
    // void DestroyServerRpc()
    // {
    //     Destroy(this.gameObject);
    // }
}
