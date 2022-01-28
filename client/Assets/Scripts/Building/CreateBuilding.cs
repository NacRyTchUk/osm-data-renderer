using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProceduralToolkit;

/* ���� ������ ������ ���� ������ � ������ ���
*/

public class CreateBuilding : MonoBehaviour
{
    List<GameObject> createWalls(Building building)
    {
        List<GameObject> walls = new List<GameObject>();

        for (int i = 0; i < building.lines.Count; i++)
        {
            GameObject wallGo = new GameObject();
            wallGo.name = "wall" + i;

            MeshRenderer wallRenderer;
            wallRenderer = wallGo.AddComponent<MeshRenderer>();
            var wallFilter = wallGo.AddComponent<MeshFilter>();
            wallRenderer.sharedMaterial = new Material(Shader.Find("Standard"));
            wallRenderer.sharedMaterial.color = Building.Color;

            /* �� ������ ������ �������� ������� ���, ����� ��� �����
             * �������� �� ��� ����� ���������� ������ (���� �������� ������� � Backface Culling)
             * ������ ������� ��� ������ ����� � ��� �� ������ ��������, �������
             * ���������� ��������� ������ ������� ������ ���� ����� ������ �
             * ��� ���� ������� ���, ����� ��� ��������� ������������
             * 
             * ��� ������� ����� ������ ��������� Backface Culling � ���������,
             * �� ����� � � ����� �������� �����������, ��� �� ����� �� ����
             * �������� ������ ������������������, ��� ��������
            */
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

            wallFilter.mesh = rect;

            walls.Add(wallGo);
        }

        return walls;
    }

    GameObject createRoof(Building building)
    {
        GameObject roofGo = new GameObject();
        roofGo.name = "roof";

        var roofRenderer = roofGo.AddComponent<MeshRenderer>();
        var roofFilter = roofGo.AddComponent<MeshFilter>();

        roofRenderer.sharedMaterial = new Material(Shader.Find("Standard"));
        roofRenderer.sharedMaterial.color = Building.Color;

        Mesh roof = new Mesh();

        // ����� �������� ������� �� N ����� � ������� ProceduralToolkit
        Tessellator tessellator = new Tessellator();

        tessellator.AddContour(building.GetRoofVertices());
        tessellator.Tessellate();
        roof = tessellator.ToMesh();
        roof.RecalculateNormals();

        roofFilter.mesh = roof;

        return roofGo;
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector2[] points =
        {
            new Vector2(0.0f, 3.0f),
            new Vector2(5.0f, 3.0f),
            new Vector2(1.0f, 2.0f),
            new Vector2(0.0f, 1.0f)
        };

        // �������� ����� �� 4 ������
        List<Line> myLines = new List<Line>();
        myLines.Add(new Line(points[0], points[1]));
        myLines.Add(new Line(points[1], points[2]));
        myLines.Add(new Line(points[2], points[3]));
        myLines.Add(new Line(points[3], points[0]));

        // ������ �������� ��� ���������� ������
        Building building = new Building(myLines, 5);

        // ���� �������� ��� ��� GameObject ������ � ����� ���������� ����������� ����� ��� �����

        List<GameObject> buildingGo = createWalls(building);
        buildingGo.Add(createRoof(building));
        CombineInstance[] combine = new CombineInstance[buildingGo.Count];

        for (int i = 0; i < buildingGo.Count; i++)
        {
            MeshFilter mf = buildingGo[i].GetComponent<MeshFilter>();
            combine[i].mesh = mf.mesh;
            combine[i].transform = mf.transform.localToWorldMatrix;
            Destroy(mf.gameObject);
        }

        MeshRenderer buildingMR = gameObject.AddComponent<MeshRenderer>();
        buildingMR.sharedMaterial = new Material(Shader.Find("Standard"));
        buildingMR.sharedMaterial.color = Building.Color;
        MeshFilter buildngMF = gameObject.AddComponent<MeshFilter>();
        buildngMF.mesh = new Mesh();
        buildngMF.mesh.CombineMeshes(combine);
        gameObject.name = "building";
        gameObject.SetActive(true);
    }

    

    // Update is called once per frame
    void Update()
    {

    }

}
