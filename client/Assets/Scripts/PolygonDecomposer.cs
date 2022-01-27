using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  ������� ��� ������ ������������ ��������,
 *  ����� ������ ����� �������� �������� ������������ � ���������
 *  ����� ����� ������������ ���� �����
 */
public static class PolygonDecomposer
{
    //������� ������� ������� ����� ��������� ������������ �������� (��������� ������� �� ������������)
    public static int[] decompose(List<Vector3> vertices)
    {
        List<int> triangles = new List<int>();

        //���� ��� ������� ����������� ���������, ����� ��������� ���-�� �������
        for (int i = 0; i < vertices.Count - 1; i++)
        {
            for (int j = i + 1; j < vertices.Count; j++)
            {
                if (vertices[j].x < vertices[i].x)
                {
                    var t = vertices[j];
                    vertices[j] = vertices[i];
                    vertices[i] = t;
                }
            }
        }

        return new int[0];
    }
}
