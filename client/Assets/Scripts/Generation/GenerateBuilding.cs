using System.Collections.Generic;
using UnityEngine;
using ProceduralToolkit;
using BuildingClass;


// ���� ������ ������ ���� ������ � ������ ���

namespace Generation
{
    public class GenerateBuilding : MonoBehaviour
    {
        List<GameObject> createWalls(Building building)
        {
            List<GameObject> walls = new List<GameObject>();

            for (int i = 0; i < building.GetWallsList().Count; i++)
            {
                GameObject wallGo = new GameObject();
                wallGo.name = "wall" + i;

                MeshRenderer wallRenderer;
                wallRenderer = wallGo.AddComponent<MeshRenderer>();
                var wallFilter = wallGo.AddComponent<MeshFilter>();
                wallRenderer.sharedMaterial = new Material(Shader.Find("Standard"));
                wallRenderer.sharedMaterial.color = Building.GlobalColor;

                Mesh wall = new Mesh();

                Tessellator tessellator = new Tessellator();
                tessellator.AddContour(building.GetWallsList()[i]);
                tessellator.Tessellate();
                wall = tessellator.ToMesh();

                wallFilter.mesh = wall;

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
            roofRenderer.sharedMaterial.color = Building.GlobalColor;

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

        GameObject createBuilding(Building building)
        {
            // ���� �������� ��� ��� GameObject ������ � ����� ���������� ����������� ����� ��� �����

            List<GameObject> buildingMeshes = createWalls(building);
            buildingMeshes.Add(createRoof(building));
            CombineInstance[] combine = new CombineInstance[buildingMeshes.Count];

            for (int i = 0; i < buildingMeshes.Count; i++)
            {
                MeshFilter mf = buildingMeshes[i].GetComponent<MeshFilter>();
                combine[i].mesh = mf.mesh;
                combine[i].transform = mf.transform.localToWorldMatrix;
                Destroy(mf.gameObject);
            }

            GameObject buildingGo = new GameObject();
            MeshRenderer buildingMR = buildingGo.AddComponent<MeshRenderer>();
            buildingMR.sharedMaterial = new Material(Shader.Find("Standard"));
            buildingMR.sharedMaterial.color = Building.GlobalColor;
            MeshFilter buildngMF = buildingGo.AddComponent<MeshFilter>();
            buildngMF.mesh = new Mesh();
            buildngMF.mesh.CombineMeshes(combine);
            Building.BuildingsNumber++;
            buildingGo.name = "building" + Building.BuildingsNumber;
            buildingGo.SetActive(true);

            return buildingGo;
        }

        // ���� ��� ��������� �������� (� ��������� ����� ��������� ���������� ����� � ���-�� ������)
        [SerializeField] public Vector2 point1;
        [SerializeField] public Vector2 point2;
        [SerializeField] public Vector2 point3;
        [SerializeField] public Vector2 point4;
        [SerializeField] public uint levels;

        // Start is called before the first frame update
        void Start()
        {
            // �������� ����� �� 4 ������
            List<Line> myLines = new List<Line>();
            myLines.Add(new Line(point1, point2));
            myLines.Add(new Line(point2, point3));
            myLines.Add(new Line(point3, point4));
            myLines.Add(new Line(point4, point1));

            Building b = new Building(myLines, levels);

            createBuilding(b).transform.parent = gameObject.transform;
        }



        // Update is called once per frame
        void Update()
        {

        }

    }
}