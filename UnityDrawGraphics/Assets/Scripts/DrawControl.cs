using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DrawControl : GraphicsBehaviour
{

	// Use this for initialization
    private List<List<Vector2>> _inItPoints = new List<List<Vector2>>();
	void Start () 
    {
       _inItPoints.Add(new List<Vector2>());
       _inItPoints[0].Add(new Vector2(100, 100));
       _inItPoints[0].Add(new Vector2(500, 100));
       _inItPoints[0].Add(new Vector2(500, 500));
       _inItPoints[0].Add(new Vector2(100, 500));
       Debug.Log(_inItPoints[0][1]);
	}
	
	// Update is called once per frame
	void Update ()
    {

	
	}
    public override void OnRenderObject()
    {
        base.OnRenderObject();
        DrawPolygon(_inItPoints[0], new Color(1, 0.92f, 0.016f, 0.5f), Color.red, PointMode.ScreenPoint);

    }
}
