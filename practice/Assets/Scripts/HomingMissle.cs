using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class HomingMissle : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 1;
    private void FaceTarget()
    {
        // Vector3 that points from plane to camera
        Vector3 relativePos = _target.position - transform.position;
        // Quaternion that faces that direction
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        // Current rotation
        Quaternion current = transform.rotation;
        // Interpolate from current rotation to new rotation
        transform.rotation = Quaternion.Slerp(current, rotation, Time.deltaTime * _speed);

    }

    private void Start()
    {
        //StartCoroutine(SendHoming());
       // HomingSend();
    }

    private IEnumerator SendHoming()
    {
        while (Vector3.Distance(transform.position, _target.position) > 0.3f)
        {
            transform.position += (_target.position - transform.position).normalized * _speed * Time.deltaTime;
            transform.LookAt(_target.transform);
            yield return null;
        }
        yield return null;

    }

    private async Task HomingSend()
    {
        while (Vector3.Distance(transform.position, _target.position) > 0.3f)
        {
            transform.position += (_target.position - transform.position) * _speed * Time.deltaTime;
            transform.LookAt(_target.transform);
            await Task.Yield(); 
        }
        await Task.Yield();
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position, _target.position) > .03f)
        {
            transform.position += (_target.position - transform.position) * _speed * Time.deltaTime;
        }

    }
}
