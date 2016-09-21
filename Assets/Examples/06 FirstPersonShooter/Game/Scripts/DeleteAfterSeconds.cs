using UnityEngine;
using System.Collections;

namespace SFS2XExamples.FirstPersonShooter {
	public class DeleteAfterSeconds : MonoBehaviour {
	
		public float seconds = 1.0f;
		
		void Start () {
			Destroy (gameObject, seconds);
		}
		
	}
}