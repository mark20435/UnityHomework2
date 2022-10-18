using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    public Transform lookPoint;
    public Transform followTarget;
    public float lookHeight = 1.0f;
    public float followHeight = 1.0f;
    public float lookSmoothTime = 0.1f;
    public float followDistance = 5.0f;
    public float cameraSensitivity = 1.0f;
    private float horizontalRotateDegree = 0.0f;
    private float verticalRotateDegree = 0.0f;
    private Vector3 followPosition = Vector3.zero;
    private Vector3 currentVector = Vector3.zero;
    private Vector3 refVel;
    // Start is called before the first frame update
    void Start()
    {
        currentVector = followTarget.forward;
        refVel = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        float mX = Input.GetAxis("Mouse X");
        float mY = Input.GetAxis("Mouse Y");
        horizontalRotateDegree = mX * cameraSensitivity;
        verticalRotateDegree += mY;

        if (verticalRotateDegree > 20.0f)
        {
            verticalRotateDegree = 20.0f;
        } else if (verticalRotateDegree < -30.0f)
        {
            verticalRotateDegree = -30.0f;
        }
    }

    private void LateUpdate()
    {
        // calculate follow position
        Vector3 horizontalVec = currentVector;
        horizontalVec.y = 0.0f;
        Vector3 rotatedHVec = Quaternion.AngleAxis(horizontalRotateDegree, Vector3.up) * horizontalVec;
        rotatedHVec.Normalize();
        Vector3 axis = Vector3.Cross(Vector3.up, rotatedHVec);
        Vector3 finalVec = Quaternion.AngleAxis(-verticalRotateDegree, axis) * rotatedHVec;
        followPosition = followTarget.position - finalVec * followDistance + followHeight * Vector3.up;
        
        // lerp to follow position
        transform.position = Vector3.Lerp(transform.position, followPosition, lookSmoothTime);
        //transform.position = Vector3.SmoothDamp(transform.position, followPosition, ref refVel, lookSmoothTime);

        // reset horizontal rotate degree
        horizontalRotateDegree = 0.0f;

        // lerp look position
        Vector3 headUpPosition = followTarget.position + lookHeight * Vector3.up;
        lookPoint.position = Vector3.Lerp(lookPoint.position, headUpPosition, lookSmoothTime);
        //lookPoint.position = Vector3.SmoothDamp(lookPoint.position, headUpPosition, ref refVel, lookSmoothTime);

        Vector3 lookVec = lookPoint.position - this.transform.position;
        this.transform.forward = lookVec;

        currentVector = transform.forward;
    }
}
