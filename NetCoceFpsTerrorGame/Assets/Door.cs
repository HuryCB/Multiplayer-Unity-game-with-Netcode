using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Door : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenDoor()
    {
        OpenDoorServerRpc();
        OpenDoorClientRpc();
         Destroy(this.transform.parent.gameObject);
    }

    [ServerRpc(RequireOwnership = false)]
    private void OpenDoorServerRpc()
    {
        Destroy(this.transform.parent.gameObject);
    }

    [ClientRpc]
    private void OpenDoorClientRpc()
    {
        Destroy(this.transform.parent.gameObject);
    }
}
