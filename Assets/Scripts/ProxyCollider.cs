using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class ProxyCollider : MonoBehaviour {
    /* Unable to do 'public IProxyColliderListener' because Unity doesn't want to serialize my interface.
     * Because of this i can't make this class universal :'( 
     */
    public IProxyColliderListener listener;
    
    void OnCollisionEnter(Collision col) {
        listener.OnProxyCollisionEnter(col);
    }
    void OnCollisionStay(Collision col) {
        listener.OnProxyCollisionStay(col);

    }
    void OnCollisionExit(Collision col) {
        listener.OnProxyCollisionExit(col);

    }
}
