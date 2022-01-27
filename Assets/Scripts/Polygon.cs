using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polygon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // �������� ����� �� 4 ������
        List<Line> myLines = new List<Line>();
        Vector2[] points =
        {
            new Vector2(0.0f, 3.0f),
            new Vector2(5.0f, 3.0f),
            new Vector2(1.0f, 2.0f),
            new Vector2(0.0f, 1.0f)
        };
        myLines.Add(new Line(points[0], points[1]));
        myLines.Add(new Line(points[1], points[2]));
        myLines.Add(new Line(points[2], points[3]));
        myLines.Add(new Line(points[3], points[0]));

        // ������ �������� ��� ���������� ������
        Building building = new Building(myLines, 1);

        // ����� ���������������� ������ � �������� ��� ������ ���������
        // ���� ��� ���������� ��� ������ ���, ��������� ��-�� ����������� Unity (������ ��������� ����� ����������� MonoBehavior)
        // ������ �������� ������������� � ������� ������ ������
        // ���������� ��������� ������ ���������������� ������ ������ � ������� ������
        // gameObject.Count �� ���� ����� ���������� ����� (���������������)
        for (int i = 0; i < building.gameObjects.Count; i++)
        {
            Instantiate<GameObject>(building.gameObjects[i]);   //��-�� ���� ������� �� ���������� ���������������� �����, �.�. ��� ������ � MonoBehavior
            building.meshRenderers.Add(new MeshRenderer());
            building.meshRenderers[i] = building.gameObjects[i].AddComponent<MeshRenderer>();
            building.meshFilters.Add(new MeshFilter());
            building.meshFilters[i] = building.gameObjects[i].AddComponent<MeshFilter>();

            building.meshRenderers[i].sharedMaterial = new Material(Shader.Find("Standard"));
            building.meshRenderers[i].sharedMaterial.SetColor("_Color", building.color);

            Mesh rect = new Mesh();
            rect.vertices = building.generateRect(building.lines[i], building.levels);
            int[] rectTriangles =
            {
                0, 1, 2,
                0, 2, 3,
                4, 6, 5,
                4, 7, 6
            };
            rect.triangles = rectTriangles;

            rect.RecalculateBounds();
            rect.RecalculateNormals();

            building.meshFilters[i].mesh = rect;

            //���� ��� ������ �� ������, ��� �� ���� �������� ��������� �����
            //building.roofVertices.Add(rect.vertices[1]);
        }

        //int[] roofTriangles = PolygonDecomposer.decompose(building.roofVertices);
    }

    

    // Update is called once per frame
    void Update()
    {

    }

}
