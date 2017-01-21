using UnityEngine;
using System.Collections.Generic;

public class Demo : GraphicsBehaviour
{

    // Use this for initialization
    private List<Vector2> _points = new List<Vector2>();
    private List<List<Vector2>> _allPoints = new List<List<Vector2>>();
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("开始" + Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            //Debug.Log("移动" + Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("结束" + MathTool.GetAngle2(new Vector2(Screen.width/2,Screen.height/2),Input.mousePosition));

        }
#endif
#if !UNITY_EDITOR
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("开始" + Input.GetTouch(0).position);
        }
        else if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Debug.Log("移动" + Input.GetTouch(0).position);
        }
        else if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Debug.Log("结束" + Input.GetTouch(0).position);
        }
#endif
    }

    public override void OnRenderObject()
    {
        base.OnRenderObject();

        var points = new List<Vector2>();
        points.Add(new Vector2(0.1f, 0.2f));
        points.Add(new Vector2(0.5f, 0.25f));
        points.Add(new Vector2(0.8f, 0.95f));
        points.Add(new Vector2(0.7f, 0.88f));

        DrawPolygon(points, new Color(1, 0.92f, 0.016f, 0.5f), Color.red, PointMode.GLPoint);

        points = new List<Vector2>();
        points.Add(new Vector2(0.3f, 0.3f));
        points.Add(new Vector2(0.6f, 0.8f));
        points.Add(new Vector2(0.3f, 0.7f));
        points.Add(new Vector2(0.2f, 0.7f));
        DrawPolygon(points, new Color(1, 0.92f, 0.016f, 0.5f), Color.red, PointMode.GLPoint);

        points = new List<Vector2>();
        points.Add(new Vector2(100, 100));
        points.Add(new Vector2(200, 100));
        points.Add(new Vector2(200, 200));
        points.Add(new Vector2(100, 200));
        DrawPolygon(points, new Color(1, 0.92f, 0.016f, 0.5f), Color.red, PointMode.ScreenPoint);
    }
}