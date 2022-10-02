using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerNetwork : NetworkBehaviour
{
    private readonly NetworkVariable<Vector3> netPos = new (writePerm: NetworkVariableWritePermission.Owner);
    private readonly NetworkVariable<Quaternion> netRotation = new (writePerm: NetworkVariableWritePermission.Owner);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if(IsOwner)   {
        netPos.Value = transform.position;
        netRotation.Value = transform.rotation;
     }else{
        transform.position = netPos.Value;
        transform.rotation = netRotation.Value;
     }
    }
}
