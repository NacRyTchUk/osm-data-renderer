using System.Collections.Generic;
using UnityEngine;
using ProceduralToolkit;
using BuildingClass;
using TerrainGeneration;

// ���� ������ ������ ���� ������ � ������ ���

namespace Generation
{
    public class GenerateBuilding : MonoBehaviour
    {
        static List<GameObject> createWalls(Building building)
        {
            List<GameObject> walls = new List<GameObject>();

            for (int i = 0; i < building.GetWallsList().Count; i++)
            {
                GameObject wallGo = new GameObject();
                wallGo.name = "wall" + i;

                MeshRenderer wallRenderer;
                wallRenderer = wallGo.AddComponent<MeshRenderer>();
                var wallFilter = wallGo.AddComponent<MeshFilter>();
                wallRenderer.material = Resources.Load("BuildingMaterial", typeof(Material)) as Material;

                Mesh wall = new Mesh();
                Tessellator tessellator = new Tessellator();
                tessellator.AddContour(building.GetWallsList()[i]);
                tessellator.Tessellate();
                wall = tessellator.ToMesh();
                wall.RecalculateNormals();
                wall.RecalculateUVDistributionMetrics();

                wallFilter.mesh = wall;

                walls.Add(wallGo);
            }

            return walls;
        }

        static GameObject createRoof(Building building)
        {
            GameObject roofGo = new GameObject();
            roofGo.name = "roof";

            var roofRenderer = roofGo.AddComponent<MeshRenderer>();
            var roofFilter = roofGo.AddComponent<MeshFilter>();

            roofRenderer.material = Resources.Load("BuildingMaterial", typeof(Material)) as Material;

            Mesh roof = new Mesh();

            // ����� �������� ������� �� N ����� � ������� ProceduralToolkit
            Tessellator tessellator = new Tessellator();
            tessellator.AddContour(building.GetRoofVertices());
            tessellator.Tessellate();
            roof = tessellator.ToMesh();
            roof.RecalculateNormals();
            roof.RecalculateUVDistributionMetrics();

            roofFilter.mesh = roof;

            return roofGo;
        }

        public static GameObject createBuilding(Building building)
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

            GameObject buildingGo = Instantiate(new GameObject());
            MeshRenderer buildingMR = buildingGo.AddComponent<MeshRenderer>();
            buildingMR.material = Resources.Load("BuildingMaterial", typeof(Material)) as Material;
            MeshFilter buildingMF = buildingGo.AddComponent<MeshFilter>();
            buildingMF.mesh = new Mesh();
            buildingMF.mesh.CombineMeshes(combine);
            buildingMF.mesh.RecalculateNormals();
            buildingMF.mesh.RecalculateUVDistributionMetrics();
            Building.incrementBuildingsNumber();
            buildingGo.name = "building" + Building.getBuildingsNumber();
            buildingGo.SetActive(true);

            float minDistance = Building.getHeight() * building.levels + TerrainGenerator.maxHeight;
            buildingMF.mesh.Move(new Vector3(0.0f, minDistance, 0.0f));
            RaycastHit hit;
            for (int i = 0; i < buildingMF.mesh.vertices.Length; i++)
            {
                Ray ray = new Ray(buildingMF.mesh.vertices[i], Vector3.down);
                if (Physics.Raycast(ray, out hit))
                    {
                    if (hit.distance < minDistance)
                        minDistance = hit.distance;
                }
            }
            buildingMF.mesh.Move(new Vector3(0.0f, -minDistance - Building.getHeight(), 0.0f));

            buildingGo.transform.Translate(new Vector3(0.0f, 0.01f, 0.0f));

            return buildingGo;
        }

        // ���� ��� ��������� �������� (� ��������� ����� ��������� ���������� ����� � ���-�� ������)
        [SerializeField] public uint levels;

        // Start is called before the first frame update
        void Start()
        {

        }



        // Update is called once per frame
        void Update()
        {

        }

    }
}