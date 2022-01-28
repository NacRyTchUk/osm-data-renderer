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
    private const float heightPerLevel = 1.0f;

    // ���� ���� ��� �����������, � ������ ���� ��� ����� ��������
    public static Color Color = new Color(0.3f, 0.3f, 0.3f);
    public static uint BuilingsNumber = 0;

    public List<Line> lines;
    public int levels;
    private List<Vector3> roofVertices = new List<Vector3>();

    public List<Vector3> GetRoofVertices()
    {
        return roofVertices;
    }

    public Building(List<Line> _lines, int _levels)
    {
        for (int i = 0; i < _lines.Count; i++)
        {
            roofVertices.Add(new Vector3(_lines[i].start.x, heightPerLevel * _levels, _lines[i].start.y));
        }
        levels = _levels;
        lines = _lines;
    }

    //public static Building CreateFromJSON(string json)
    //{

    //}

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
