using System.Collections.Generic;
using UnityEngine;

public class GraphicsBehaviour : MonoBehaviour
{
    public enum PointMode
    {
        GLPoint,
        ScreenPoint
    }

    private Material lineMaterial;

    void Awake()
    {
        // Unity has a built-in shader that is useful for drawing
        // simple colored things.
        var shader = Shader.Find("Hidden/Internal-Colored");
        lineMaterial = new Material(shader);
        lineMaterial.hideFlags = HideFlags.HideAndDontSave;
        // Turn on alpha blending
        lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        // Turn backface culling off
        lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
        // Turn off depth writes
        lineMaterial.SetInt("_ZWrite", 0);
    }

    private Vector2 ScreenToGLPoint(Vector2 point)
    {
        point.x /= Screen.width;
        point.y = Screen.height - point.y;
        point.y /= Screen.height;

        //point.x = Screen.width - point.x;
        //point.y = Screen.height- point.y;
        return point;
    }

    public void DrawLine(Vector2 start, Vector2 end, Color color, PointMode pointMode = PointMode.ScreenPoint)
    {
        if (pointMode == PointMode.ScreenPoint)
        {
            start = ScreenToGLPoint(start);
            end = ScreenToGLPoint(end);
        }

        GL.PushMatrix();

        GL.LoadOrtho();

        GL.Begin(GL.LINES);

        GL.Color(color);

        GL.Vertex(start);
        GL.Vertex(end);

        GL.End();

        GL.PopMatrix();
    }

    public void DrawLines(List<Vector2> points, Color color, PointMode pointMode = PointMode.ScreenPoint)
    {
        if (points == null || points.Count == 0)
            return;

        if (pointMode == PointMode.ScreenPoint)
        {
            for (int i = 0; i < points.Count; i++)
                points[i] = ScreenToGLPoint(points[i]);
        }

        GL.PushMatrix();

        GL.LoadOrtho();

        GL.Begin(GL.LINES);

        GL.Color(color);

        for (int i = 0; i < points.Count; i++)
        {
            GL.Vertex(points[i]);
        }

        GL.End();

        GL.PopMatrix();
    }

    public void DrawPolygon(List<Vector2> points, Color fillColor, Color lineColor, PointMode pointMode = PointMode.ScreenPoint)
    {

        if (points == null || points.Count == 0)
            return;

        if (pointMode == PointMode.ScreenPoint)
        {
            for (int i = 0; i < points.Count; i++)
                points[i] = ScreenToGLPoint(points[i]);
        }

        GL.PushMatrix();

        GL.LoadOrtho();

        GL.Begin(GL.TRIANGLES);

        GL.Color(fillColor);

        for (int i = 0; i < points.Count; i++)
        {
            if (i < points.Count - 2)
            {
                GL.Vertex(points[0]);
                GL.Vertex(points[i + 1]);
                GL.Vertex(points[i + 2]);
            }
        }

        GL.End();

        GL.Begin(GL.LINES);

        GL.Color(lineColor);

        for (int i = 0; i < points.Count; i++)
        {
            GL.Vertex(points[i]);

            if (i != points.Count - 1)
                GL.Vertex(points[i + 1]);
        }

        GL.Vertex(points[0]);

        GL.End();

        GL.PopMatrix();
    }

    public virtual void OnRenderObject()
    {
        lineMaterial.SetPass(0);
    }
}