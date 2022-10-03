using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            Debug.Log("enemy collided");
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.player = transform.parent.gameObject;
        }
    }
}
