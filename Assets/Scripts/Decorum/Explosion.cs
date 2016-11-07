using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public GameObject Ship;
	public GameObject Shockwave;
	public GameObject FireBall;
	public GameObject BaseSmoke;

	public void PlayExplosion() {
		transform.position = Ship.transform.position;
		Shockwave.GetComponent<ParticleSystem> ().Emit (1);
		FireBall.GetComponent<ParticleSystem> ().Emit (20);
		BaseSmoke.GetComponent<ParticleSystem> ().Emit (50);
	}
}
