using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ��������� ��� ������, ������� ������ ������� Building � ������ �� ��� ���������
*/

namespace OSMDataRenderer
{
    public static class BuildingData
    {
        public static List<Building> BuildingsList;

        public static void addBuilding(List<Line> _lines, uint _levels)
        {
            BuildingsList.Add(new Building(_lines, _levels));
        }
    }
}
