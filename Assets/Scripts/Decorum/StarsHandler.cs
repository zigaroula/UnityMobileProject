using UnityEngine;
using System.Collections;

public class StarsHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate () {
		float speed = GameManager.manager.GetGameSpeed ();
		// Update particle velocities, assumes constant velocity
		int numParticles = gameObject.GetComponent<ParticleSystem> ().particleCount;
		ParticleSystem.Particle[] particles = new ParticleSystem.Particle[numParticles];
		gameObject.GetComponent<ParticleSystem> ().GetParticles(particles);
		for (int i = 0; i < particles.Length; i++) {
			particles[i].velocity = new Vector3(0.0f, 0.0f, 10.0f * speed);
			if (particles [i].velocity.z == 0.0f && particles [i].lifetime <= 1.0f) {
				particles [i].lifetime += 5;
			}
		}
		gameObject.GetComponent<ParticleSystem> ().SetParticles(particles, numParticles);
		ParticleSystem.EmissionModule em = gameObject.GetComponent<ParticleSystem> ().emission;
		em.rate = 10.0f * speed;
	}
}
