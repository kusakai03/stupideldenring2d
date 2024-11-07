using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public static CameraFollow Instance { get; private set;}
    public float interpVelocity;
	private GameObject target;
	public Vector3 offset;
	Vector3 targetPos;
	private Camera cam;
	private float camsize = 6;
	private float newCamsize;
	// Use this for initialization
	private void Awake(){
		if (Instance == null)
		Instance = this;
		else Destroy(gameObject);
	}
	void Start () {
		cam = GetComponent<Camera>();
		targetPos = transform.position;
		newCamsize = camsize;
	}
	public void SetCamSize(float camsize){
		newCamsize = camsize;
	}
	// Update is called once per frame
	void FixedUpdate () {
		if (target)
		{
			Vector3 posNoZ = transform.position;
			posNoZ.z = target.transform.position.z;
			Vector3 targetDirection = (target.transform.position - posNoZ);
			interpVelocity = targetDirection.magnitude * 5f;
			targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime); 
			transform.position = Vector3.Lerp( transform.position, targetPos + offset, 0.4f);
			if (cam.orthographic)
            {
                float distanceToTarget = targetDirection.magnitude;
                cam.orthographicSize = camsize;
                float steps = (camsize / 6);
                offset.y = steps * 0.2f;
            }
		}
		if (camsize < newCamsize) camsize += 0.1f;
		if (camsize > newCamsize) camsize -= 0.1f;
	}
	public void SetTarget(GameObject newTarget){
		target = newTarget;
	}
}
