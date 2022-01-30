using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingClass;

// ��������� ��� ������, ������� ������ ������� Building � ������ �� ��� ���������

namespace DataManagement
{
    public class BuildingData
    {
        public List<Building> BuildingsList;

        public void addBuilding(List<Line> _lines, uint _levels)
        {
            BuildingsList.Add(new Building(_lines, _levels));
        }
    }
}
