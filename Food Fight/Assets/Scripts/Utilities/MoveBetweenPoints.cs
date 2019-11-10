using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenPoints : MonoBehaviour
{
    [SerializeField]
    private int CurrentTargetIndex;
    public Transform[] targets;
    public float MoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        CurrentTargetIndex = 0;
        if (MoveSpeed == 0) { MoveSpeed = 6.0f; }
    }

    // Update is called once per frame
    void Update()
    {
        if ((targets[CurrentTargetIndex].position - this.gameObject.transform.position).magnitude < 0.1f)
        {
            SetNewTarget();
        }
    }

    public void FixedUpdate()
    {
        MoveObject();
    }

    void SetNewTarget()
    {
        if(CurrentTargetIndex + 1 < targets.Length)
        {
            CurrentTargetIndex++;
        }
        else
        {
            CurrentTargetIndex = 0;
        }
    }

    void MoveObject()
    {
        
        this.gameObject.transform.position = Vector3.Lerp( this.gameObject.transform.position,targets[CurrentTargetIndex].position, MoveSpeed * Time.fixedDeltaTime);
        //this.gameObject.transform.position += Vector3.Normalize(targets[CurrentTargetIndex].position - this.gameObject.transform.position) * MoveSpeed * Time.fixedDeltaTime;
    }
}
