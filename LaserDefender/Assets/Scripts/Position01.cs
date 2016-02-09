using UnityEngine;
using System.Collections;

public class Position01 : MonoBehaviour {

	// Use this for initialization
    void OnDrawGizmos(){
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
