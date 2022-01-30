using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;

// ����� ����� ��� ��� ����������� � �������

namespace HTTPClient
{
    public class HTTPClient : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            /** 
             * ���� �� ������ ��� StartCoroutine(), ������� ��������, ���������� ����� ���� ������ �������
             * ������� � �������� ������ ���� �������� ������ ��� ��� ��� ��������
             */
            StartCoroutine(GetText());
            StartCoroutine(GetFile());
        }

        IEnumerator GetText()
        {
            /**
             * ����� ������ �������� � ����� ������ ���� �� ����������, ����������
             * ������ ��� ����� UnityWebRequest ����� ������ ��� ��� �������� 
             */
            UnityWebRequest request = UnityWebRequest.Get("http://localhost:5000/test.txt");
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                TestingHTTPdata.setData(request.downloadHandler.text);

                Debug.Log("Finished getting data");
            }
        }

        IEnumerator GetFile()
        {
            // �� �� �����, ��� � � GetText()
            UnityWebRequest request = new UnityWebRequest("http://localhost:5000/test.json", UnityWebRequest.kHttpVerbGET);
            string path = Path.Combine(Application.dataPath, "Resources/test.json");
            request.downloadHandler = new DownloadHandlerFile(path);

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log("Successfully downloaded and saved to " + path);
            }
        }
    }
}