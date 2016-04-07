using UnityEngine;
using System.Collections;

public class DestroyTest : MonoBehaviour {

    void OnDestroy()
    {
        Debug.Log(tag + ": got detroyed");
    }

	// Use this for initialization
	void Start () {
        Debug.Log(tag + ": got made");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
