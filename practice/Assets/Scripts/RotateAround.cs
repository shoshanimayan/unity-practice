using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{

    [SerializeField] private Transform _transformToRotateAround;
    [SerializeField] float _speed;
    // Start is called before the first frame update


    private void FaceTargetAndRotate()
    {
        // Vector3 that points from plane to camera
        Vector3 relativePos = _transformToRotateAround.position - transform.position;
        // Quaternion that faces that direction
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        // Current rotation
        Quaternion current = transform.rotation;
        // Interpolate from current rotation to new rotation
        transform.rotation = Quaternion.Slerp(current, rotation, Time.deltaTime * _speed);
        transform.Translate(new Vector3(.1f, 0f, 0) * Time.deltaTime * _speed);

    }

    private void RotateAroundQuat( Vector3 pivotPoint, Quaternion rot)
    {
        transform.position = rot * (transform.position - pivotPoint) + pivotPoint;
        transform.rotation = rot * transform.rotation;
    }

    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        return RotatePointAroundPivot(point, pivot, Quaternion.Euler(angles));
    }

    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
    {
        return rotation * (point - pivot) + pivot;
    }

    private void SimpleRotateAround()
    {
        transform.RotateAround(_transformToRotateAround.position, transform.up, Time.deltaTime * _speed);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotatePointAroundPivot(  transform.position, _transformToRotateAround.position, new Vector3(90, 90, 0)*Time.deltaTime));
            
    }

}
