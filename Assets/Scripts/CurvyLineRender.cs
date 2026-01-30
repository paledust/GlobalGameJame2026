using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(LineRenderer))]
public class CurvyLineRender : MonoBehaviour
{
    [SerializeField] private Transform upperLeg;
    [SerializeField] private Transform ankle;
    [SerializeField] private Transform foot;
    [SerializeField] private int seg = 10;

    private Spline spline;
    private LineRenderer legLine;
    private BezierKnot[] controlKnots;

    void Awake()
    {
        legLine = GetComponent<LineRenderer>();
        legLine.positionCount = seg + 1;

        controlKnots = new BezierKnot[3];
        controlKnots[0] = new BezierKnot(upperLeg.position);
        controlKnots[1] = new BezierKnot(ankle.position);
        controlKnots[2] = new BezierKnot(foot.position);

        spline = new Spline();
        for(int i=0; i<controlKnots.Length; i++)
        {
            spline.Add(controlKnots[i]);
        }
        spline.Closed = false;
    }

    void Update()
    {
        controlKnots[0].Position = upperLeg.position;
        controlKnots[1].Position = ankle.position;
        controlKnots[2].Position = foot.position;
        for(int i=0; i<controlKnots.Length; i++)
        {
            spline.SetKnot(i, controlKnots[i]);
        }
        for(int i=0; i<=seg; i++)
        {
            float t = i / (float)seg;
            Vector3 pos = spline.EvaluatePosition(t);
            legLine.SetPosition(i, pos);
        }
    }
}