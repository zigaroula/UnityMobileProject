using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundMove : MonoBehaviour {

	public Material NebulaA;
	public Material NebulaAB;
	public Material NebulaABC;
	public Material NebulaB;
	public Material NebulaBC;
	public Material NebulaBCD;
	public Material NebulaC;
	public Material NebulaCD;
	public Material NebulaCDE;
	public Material NebulaD;
	public Material NebulaDE;
	public Material NebulaDEF;
	public Material NebulaE;
	public Material NebulaEF;
	public Material NebulaF;

	private List<Material> nebulas;

	void Start() {
		nebulas = new List<Material> ();
		nebulas.Add (NebulaA);
		nebulas.Add (NebulaAB);
		nebulas.Add (NebulaABC);
		nebulas.Add (NebulaB);
		nebulas.Add (NebulaBC);
		nebulas.Add (NebulaBCD);
		nebulas.Add (NebulaC);
		nebulas.Add (NebulaCD);
		nebulas.Add (NebulaCDE);
		nebulas.Add (NebulaD);
		nebulas.Add (NebulaDE);
		nebulas.Add (NebulaDEF);
		nebulas.Add (NebulaE);
		nebulas.Add (NebulaEF);
		nebulas.Add (NebulaF);
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.manager.GetCurrentGameState () != GameManager.GameState.Pause) {
			float speed = GameManager.manager.GetGameSpeed();
			transform.Translate (0, 0, -0.2f * speed);
			if (transform.position.y <= -60) {
				transform.position = new Vector3 (Random.Range(-15, 15), 60, transform.position.z);
				int matNumber = Random.Range (0, nebulas.Count);
				gameObject.GetComponent<MeshRenderer> ().material = nebulas [matNumber];
			}
		}
	}
}
