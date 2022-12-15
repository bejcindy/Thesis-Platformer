using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeMaster : MonoBehaviour
{
    public static bool goBack;

    private LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    public float ropeSegLen = 0.5f;
    public int segmentLength;
    public float lineWidth = 0.5f;

    int originalLength;
    float timer;
    bool reset;

    // Use this for initialization
    void Start()
    {
        originalLength = segmentLength;
        this.lineRenderer = this.GetComponent<LineRenderer>();
        //Vector3 ropeStartPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 ropeStartPoint = transform.position;
        
        for (int i = 0; i < segmentLength; i++)
        {
            this.ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= ropeSegLen;
        }
        //GenerateMeshCollider();
    }

    // Update is called once per frame
    void Update()
    {
        this.DrawRope();

        if (goBack)
        {
            if (!reset)
            {
                segmentLength = 3;
                reset = true;
            }
            else
            {
                timer += Time.deltaTime;
                if (timer > .1f)
                {
                    //Debug.Log(segmentLength);
                    segmentLength++;
                    timer = 0;
                }
                if (segmentLength == originalLength)
                {
                    timer = 0;
                    reset = false;
                    goBack = false;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        this.Simulate();
    }




    private void Simulate()
    {
        // SIMULATION
        Vector2 forceGravity = new Vector2(0f, -1.5f);

        for (int i = 1; i < this.segmentLength; i++)
        {
            RopeSegment firstSegment = this.ropeSegments[i];
            Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
            this.ropeSegments[i] = firstSegment;
        }

        //CONSTRAINTS
        for (int i = 0; i < 50; i++)
        {
            this.ApplyConstraint();
        }
    }

    private void ApplyConstraint()
    {
        //Constrant to Mouse
        RopeSegment firstSegment = this.ropeSegments[0];
        //firstSegment.posNow = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        firstSegment.posNow = transform.position;
        this.ropeSegments[0] = firstSegment;
        RopeSegment lastSegment = this.ropeSegments[ropeSegments.Count-1];
        lastSegment.posNow = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.ropeSegments[ropeSegments.Count - 1] = lastSegment;

        for (int i = 0; i < this.segmentLength - 1; i++)
        {
            RopeSegment firstSeg = this.ropeSegments[i];
            RopeSegment secondSeg = this.ropeSegments[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - this.ropeSegLen);
            Vector2 changeDir = Vector2.zero;

            if (dist > ropeSegLen)
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            }
            else if (dist < ropeSegLen)
            {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if (i != 0)
            {
                firstSeg.posNow -= changeAmount * 0.5f;
                this.ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                this.ropeSegments[i + 1] = secondSeg;
            }
            else
            {
                secondSeg.posNow += changeAmount;
                this.ropeSegments[i + 1] = secondSeg;
            }
        }
    }

    private void DrawRope()
    {
        float lineWidth = this.lineWidth;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3[] ropePositions = new Vector3[this.segmentLength];
        for (int i = 0; i < this.segmentLength; i++)
        {
            ropePositions[i] = this.ropeSegments[i].posNow;
        }

        lineRenderer.positionCount = ropePositions.Length;
       
        lineRenderer.SetPositions(ropePositions);
    }

    public Vector3[] GetPositions()
    {
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);
        return positions;
    }

    public float GetWidth()
    {
        return lineRenderer.startWidth;
    }

    public struct RopeSegment
    {
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegment(Vector2 pos)
        {
            this.posNow = pos;
            this.posOld = pos;
        }
    }
}