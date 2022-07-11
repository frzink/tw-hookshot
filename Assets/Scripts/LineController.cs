using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private Transform source;
    private Transform target;


    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (source != null && target != null)
        {
            lineRenderer.SetPosition(0, source.position);
            lineRenderer.SetPosition(1, target.position);
        }
    }


    public void setLineSource(Transform lineSource)
    {
        source = lineSource;
    }

    public void setLineTarget(Transform lineTarget)
    {
        target = lineTarget;
    }

}
