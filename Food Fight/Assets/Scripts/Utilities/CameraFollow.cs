using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject followTarget;

    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    private float cameraZ = 0;
    public float cameraSmoothing;

    public float shakeDurationDefault = 0.1f;
    private float ShakeDuration;
    public float shakeMagnitude = 0.05f;

    private bool CamShake = false;
    private Transform Obstruction;

    private Camera camera;

    void Start()
    {
        cameraZ = transform.position.z;
        camera = GetComponent<Camera>();
        cameraSmoothing = 2f;

        ShakeDuration = shakeDurationDefault;
    }

    void Update()
    {
        if(followTarget == null)
        {
            followTarget = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void LateUpdate()
    {
        if (followTarget)
        {
            //CLOSE RANGE CAM
            Vector3 TargetPos = new Vector3(followTarget.transform.position.x, 8, followTarget.transform.position.z - 7.5f);
            
            //LONG DISTANCE CAM
            //Vector3 TargetPos = new Vector3(followTarget.transform.position.x, 19, followTarget.transform.position.z - 7.5f);
            //transform.position = Vector3.SmoothDamp(transform.position, TargetPos, ref velocity, cameraSmoothing);
            transform.position = Vector3.Slerp(transform.position, TargetPos, cameraSmoothing * Time.deltaTime);
            //transform.position = new Vector3(followTarget.transform.position.x, 19, followTarget.transform.position.z - 7.5f);
            //Vector3 delta = followTarget.transform.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, cameraZ));
            //Vector3 destination = transform.position + delta;
            //destination.z = cameraZ;
            //transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            ViewObstructed();
        }
    }

    private void ViewObstructed()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, followTarget.transform.position - transform.position, out hit, 4.5f))
        {
            if (Obstruction)
            {
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                Obstruction = null;
            }
            if (!hit.collider.CompareTag("Player"))
            {
                Obstruction = hit.transform;
                Obstruction.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                if(Vector3.Distance(Obstruction.position, transform.position) >= 3f && Vector3.Distance(transform.position, followTarget.transform.position) >= 1.5f)
                {
                    transform.Translate(Vector3.forward * 2f * Time.deltaTime);
                }

            }
            else
            {
                if (Obstruction)
                { 
                    Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                    Obstruction = null;
                }
                if (Vector3.Distance(transform.position, followTarget.transform.position) < 4.5f)
                {
                    transform.Translate(Vector3.back * 2f * Time.deltaTime);
                }
            }
        }
    }

    public void CameraShake()
    {
        if (!CamShake)
        {
            CamShake = true;
            StartCoroutine(ShakeCamera());
        }
    }

    IEnumerator ShakeCamera()
    {
        Debug.Log("IS SHAKKING!");
        while (CamShake)
        {
            if (ShakeDuration > 0)
            {
                camera.transform.localPosition = camera.transform.localPosition + Random.insideUnitSphere * shakeMagnitude;
                ShakeDuration -= Time.deltaTime;
            }
            else
            {
                CamShake = false;
                ShakeDuration = shakeDurationDefault;
            }
        }
        yield return null;
    }

}
