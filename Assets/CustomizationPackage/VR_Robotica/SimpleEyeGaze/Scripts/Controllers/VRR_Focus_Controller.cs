using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.VR_Robotica.AvatarComponents.Controllers
{
	/// <summary>
	/// This creates the Focus Point that the Avatar's Eyes will converge upon. It has one 
	/// controlling function that moves it from one position to another.
	/// </summary>
	public class VRR_Focus_Controller : MonoBehaviour
	{
		public GameObject	controller;
		public Vector3		DefaultPosition;

		private Vector3		_startPosition;
		private Vector3		_targetPosition;
		private float		_speed;
		private bool		_isReady;

		/// <summary>
		/// the _controller object will follow the Target's Transform 
		/// POSITION at this rate of SPEED. Controller moves in WORLD SPACE
		/// </summary>
		/// <param name="target"></param>
		/// <param name="speed"></param>
		public void moveTo(Vector3 target, float speed)
		{
			_startPosition = controller.transform.position;
			_targetPosition = target;
			_speed = speed;
		}

		public IEnumerator Create()
		{
			create();
			Start_Moving();
			yield return null;
		}

		public void GotoDefaultPosition()
		{
			moveTo(DefaultPosition, 5.0f);
		}

		private void create()
		{
			if (!_isReady)
			{
				controller = new GameObject();
				controller.name = "Focus Controller";
				controller.transform.parent = this.transform;
				// make sure it does not interfere with any ray casting
				// Layer[2] = Ignore Raycast
				controller.layer = 2;

				controller.AddComponent<Rigidbody>();
				controller.GetComponent<Rigidbody>().useGravity = false;
				controller.GetComponent<Rigidbody>().mass = 0.0f;

				//add collider
				controller.AddComponent<SphereCollider>();
				controller.GetComponent<SphereCollider>().radius = 0.01f;
				controller.GetComponent<SphereCollider>().isTrigger = true;

				/* Un Comment if you'd like to have geometry attached to the controller,
				 * which you may like to see how the character changes its focus
				  
				GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				sphere.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
				Destroy(sphere.gameObject.GetComponent<SphereCollider>());
				sphere.transform.parent = controller.transform;
				*/

				_isReady = true;
			}
			else
			{
				Debug.LogWarning("Only one focus controller object can be created.");
			}
		}

		#region COROUTINE - Moving
		public void Start_Moving()
		{
			Stop_Moving();
			container = moving();
			StartCoroutine(container);
		}

		public void Stop_Moving()
		{
			if(container != null)
			{
				StopCoroutine(container);
				container = null;
			}
		}

		private IEnumerator container;
		private IEnumerator moving()
		{
			while (true)
			{
				controller.transform.position = Vector3.Lerp(_startPosition, _targetPosition, Time.deltaTime * _speed);
				yield return null;
			}
		}
		#endregion
	}
}