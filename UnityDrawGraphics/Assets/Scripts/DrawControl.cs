using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DrawControl : GraphicsBehaviour
{
    // Use this for initialization
    private List<List<Vector2>> _inItPoints = new List<List<Vector2>>();//初始点
    private List<List<Vector2>> _newPoints = new List<List<Vector2>>();//新点
    private List<Vector2> _linePoints = new List<Vector2>();//折线
    private Vector2 _downVector2;//按下点
    private Vector2 _moveCurrentVector2;//当前移动点
    private Vector2 _moveLastVector2;//上一个移动点
    private float _K;//折线斜率
    private float _b;//折线b值
    private float _angle;//移动点与按下点角度
    private float _symmetryLength= 1000;//随便值用于画对折线
    void Start()
    {
        _inItPoints.Add(new List<Vector2>());
        _inItPoints[0].Add(MathTool.ToVector2(new Vector2(Screen.width / 4, Screen.height / 4)));
        _inItPoints[0].Add(MathTool.ToVector2(new Vector2(Screen.width * 3 / 4, Screen.height / 4)));
        _inItPoints[0].Add(MathTool.ToVector2(new Vector2(Screen.width * 3/4, Screen.height * 3 / 4)));
        _inItPoints[0].Add(MathTool.ToVector2(new Vector2(Screen.width/4, Screen.height * 3 / 4)));


        //_inItPoints.Add(new List<Vector2>());
        //_inItPoints[1].Add(MathTool.ToVector2(new Vector2(100 + Screen.width / 4, Screen.height / 4)));
        //_inItPoints[1].Add(MathTool.ToVector2(new Vector2(100 + Screen.width * 3 / 4, Screen.height / 4)));
        //_inItPoints[1].Add(MathTool.ToVector2(new Vector2(100 + Screen.width * 3 / 4, Screen.height * 3 / 4)));
        //_inItPoints[1].Add(MathTool.ToVector2(new Vector2(100 + Screen.width / 4, Screen.height * 3 / 4)));

        //_inItPoints.Add(new List<Vector2>());
        //_inItPoints[2].Add(MathTool.ToVector2(new Vector2(150 + Screen.width / 4, Screen.height / 4)));
        //_inItPoints[2].Add(MathTool.ToVector2(new Vector2(150 + Screen.width * 3 / 4, Screen.height / 4)));
        //_inItPoints[2].Add(MathTool.ToVector2(new Vector2(150 + Screen.width * 3 / 4, Screen.height * 3 / 4)));
        //_inItPoints[2].Add(MathTool.ToVector2(new Vector2(150 + Screen.width / 4, Screen.height * 3 / 4)));

        _newPoints.Add(_inItPoints[0]);
        //_newPoints.Add(_inItPoints[1]);
        //_newPoints.Add(_inItPoints[2]);


    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount >= 2) return;
        if (Input.GetMouseButtonDown(0))
        {
            _downVector2 = MathTool.ToVector2(Input.mousePosition);
            _moveLastVector2 = _downVector2;
        }
        else if (Input.GetMouseButton(0))
        {
            _moveCurrentVector2 = MathTool.ToVector2(Input.mousePosition);
            if (MathTool.GetDistance(_moveCurrentVector2, _moveLastVector2) <1) return;
            _moveLastVector2 = _moveCurrentVector2;
            _angle = MathTool.GetAngle2(_downVector2, _moveCurrentVector2);
            var radian = (_angle - 90) * Mathf.PI / 180;
            _K = Mathf.Tan(radian);
            _b = (_downVector2.y + _moveCurrentVector2.y) / 2 - (_K * (_moveCurrentVector2.x + _downVector2.x) / 2);
            //折线
            Vector2 p1 = new Vector2(_symmetryLength, _K * _symmetryLength + _b);
            Vector2 p2 = new Vector2(-_symmetryLength, -_K * _symmetryLength + _b);
            p1 = MathTool.ToVector2(p1);
            p2 = MathTool.ToVector2(p2);
            if (_angle == 0 || _angle == 180)
            {
                //折线垂直
                p1.x = (_downVector2.x + _moveCurrentVector2.x) / 2;
                p1.y = _symmetryLength;

                p2.x = (_downVector2.x + _moveCurrentVector2.x) / 2;
                p2.y = -_symmetryLength;
            }
            _linePoints.Clear();
            _linePoints.Add(p1);
            _linePoints.Add(p2);
            _newPoints.Clear();
            for (int i = _inItPoints.Count-1; i >=0 ; i--)
            {
                MathTool.CountDraw(_inItPoints[i],_angle,_downVector2,_moveCurrentVector2,_K,_b,_newPoints);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (_newPoints.Count != 0)
            {
                _inItPoints.Clear();
                _inItPoints.AddRange(_newPoints);
            }
        }
    }
    public override void OnRenderObject()
    {
        base.OnRenderObject();
        for (int j = 0; j < _newPoints.Count; j++)
        {
            List<Vector2> points = new List<Vector2>();
            for (int i = 0; i < _newPoints[j].Count; i++)
            {
                points.Add(_newPoints[j][i]);
            }
            DrawPolygon(points, new Color(1, 0.92f, 0.016f, 0.5f), Color.red, PointMode.ScreenPoint);
        }
        if (_linePoints.Count != 0)
        {
            List<Vector2> linePoints = new List<Vector2>();
            for (int i = 0; i < _linePoints.Count; i++)
            {
                linePoints.Add(_linePoints[i]);
            }
            //DrawLines(linePoints, Color.red, PointMode.ScreenPoint);
        }
    }
    void OnGUI()
    {
        if (GUILayout.Button("还原", GUILayout.Width(Screen.width / 8), GUILayout.Height(Screen.height / 8)))
        {
            _newPoints.Clear();
            _newPoints.Add(new List<Vector2>());
            _newPoints[0].Add(MathTool.ToVector2(new Vector2(Screen.width / 4, Screen.height / 4)));
            _newPoints[0].Add(MathTool.ToVector2(new Vector2(Screen.width * 3 / 4, Screen.height / 4)));
            _newPoints[0].Add(MathTool.ToVector2(new Vector2(Screen.width * 3 / 4, Screen.height * 3 / 4)));
            _newPoints[0].Add(MathTool.ToVector2(new Vector2(Screen.width / 4, Screen.height * 3 / 4)));
        }
    }
}
