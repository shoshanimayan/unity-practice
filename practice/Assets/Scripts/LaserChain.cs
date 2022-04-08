using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserChain : MonoBehaviour
{
    [SerializeField] private Transform[] _points;

    private LineRenderer _line;

    private void Start()
    {
        _line = GetComponent<LineRenderer>();
    }

    private Vector3[] GetPositions(Transform[] spots)
    {
        Vector3[] vec3 = new Vector3[spots.Length+1];
        var i = 0;
        foreach (Transform t in spots)
        {
            vec3[i] = t.position;
            i++;
                
        }
        vec3[i] = spots[0].position;
        return vec3;


    }
    private void DrawLasers()
    {
        _line.positionCount = _points.Length+1;
        var positions = GetPositions(_points);
        _line.SetPositions(positions);
        var pos0 = positions[0];
        bool trigger = false;
        for (int i = 1; i < positions.Length; i++)
        {
            /*if (Physics.Linecast( pos0, positions[i]))
            {
                trigger = true;
            }*/
            var direction = positions[i] - pos0;
            var distance = Vector3.Distance(pos0, positions[i]);
            RaycastHit hit;
            if(Physics.Raycast(pos0, direction, out hit, distance))
            {
                trigger = true;
            }


            pos0 = positions[i];
        }
        if (trigger)
        {
            _line.startColor = Color.red;
            _line.endColor = Color.red;

        }
        else
        {
            _line.startColor = Color.green;
            _line.endColor = Color.green;
        }
        
    }

    private void Update()
    {
        DrawLasers();
    }

}
