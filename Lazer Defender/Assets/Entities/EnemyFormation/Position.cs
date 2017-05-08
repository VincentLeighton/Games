using UnityEngine;

public class Position : MonoBehaviour {
	void OnDrawGizmos(){
		Gizmos.DrawWireSphere(gameObject.transform.position, 20f);
	}
}
