using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  ����� ������� ������ �� ������ ������
*/

/*  ���������, �������������� ������ �����
 *  � ��������� Vector2, ������ ��� ��� �� ����� ������ (Y) �������, ��� ��� ����� ���������
 *  � ���� �� ���������� ����� �� ������ ������� �� 2 �����
 */
public struct Line
{
    public Vector2 start;
    public Vector2 end;
    public Line(Vector2 _start, Vector2 _end)
    {
        start = _start;
        end = _end;
    }
}

/*  ����� ������
 */
public class Building
{
    // ������ �����
    const float heightPerLevel = 1.0f;

    // ���� ���� ��� �����������, � ������ ���� ��� ����� ��������
    public Color color = new Color(0.2f, 0.2f, 0.2f);

    public List<Line> lines;
    public int levels;

    public List<Mesh> meshes = new List<Mesh>();
    public List<GameObject> gameObjects = new List<GameObject>();
    public List<MeshRenderer> meshRenderers = new List<MeshRenderer>();
    public List<MeshFilter> meshFilters = new List<MeshFilter>();

    public List<Vector3> roofVertices = new List<Vector3>();
    public Mesh roof = new Mesh();
    public GameObject roofObject = new GameObject();
    public MeshRenderer roofRenderer = new MeshRenderer();
    public MeshFilter roofFilter = new MeshFilter();

    public Building(List<Line> _lines, int _levels)
    {
        for (int i = 0; i < _lines.Count; i++)
        {
            meshes.Add(new Mesh());
            gameObjects.Add(new GameObject());
            meshRenderers.Add(new MeshRenderer());
            meshFilters.Add(new MeshFilter());
        }
        levels = _levels;
        lines = _lines;
    }
    
    /*  ���������� ���������� ������ �������������� �� ��������� ����� � ���������� ������
    */
    public Vector3[] generateRect(Line inputLine, int levels)
    {
        float height = levels * heightPerLevel;
        Vector3[] vertices = new Vector3[8];
        /*  ������� ������������ �� ��� ���, � � ����������� ������� (��� ����� ����� ������������ ���������� ���������)
         *  ��� ��� ��� ������� ��������:
         *  
         *  2--------3
         *  |        |
         *  |        |
         *  1--------4
         *  
         *  ��� ����� �������� �� ������� � �������, �� ������ ���� ������ �����, ����� �� ����� ���������� ��������
         *  ����� 1 � 4 - ��� ����� ������ ����� (Line.start � Line.end) ��������������
         */
        vertices[0].x = inputLine.start.x; vertices[0].y = 0.0f; vertices[0].z = inputLine.start.y;
        vertices[1].x = inputLine.start.x; vertices[1].y = height; vertices[1].z = inputLine.start.y;
        vertices[2].x = inputLine.end.x; vertices[2].y = height; vertices[2].z = inputLine.end.y;
        vertices[3].x = inputLine.end.x; vertices[3].y = 0.0f; vertices[3].z = inputLine.end.y;

        /*  ������ ������ ����� ������ � ������ �� �����������, ��� ����� ��������������
         *  ��� ���� ����� ������������� ��� ����� � ���� ������ (���� ��� ������ �� �������������
         *  ����� ������������ ���� � ����� �������, � ������ ���� ���������� �� ������ ������)
         *  ��� ���������� ��-�� �.�. Backface Culling
        */
        vertices[4] = vertices[0];
        vertices[5] = vertices[1];
        vertices[6] = vertices[2];
        vertices[7] = vertices[3];

        return vertices;
    }
}
