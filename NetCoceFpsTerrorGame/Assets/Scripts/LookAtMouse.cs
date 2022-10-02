using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class LookAtMouse : NetworkBehaviour
{
     public override void OnNetworkSpawn()
    {
        // if(!IsOwner){
        //     Destroy(this);
        // }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsOwner){
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            return;
        }
    }

    
}
