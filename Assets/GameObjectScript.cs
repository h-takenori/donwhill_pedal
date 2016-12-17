using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class GameObjectScript : MonoBehaviour {

    public Material material;
    public Transform goalLine;
	public Text timeText;
	private System.DateTime startTime;
	private string lastSpanStr = null;

   // Random _randam = new Random.State().();
	// Use this for initialization
	void Start ()
    {
        Random.seed = (int)System.DateTime.Now.Ticks;
        CreateMap();
		startTime = System.DateTime.Now;
    }

    /// <summary>
    /// マップを作成する
    /// </summary>
    private void CreateMap()
    {
        var mesh = new Mesh();
        var width = 2.5f;
        var len = 2.5f;
        var height = 0.25f;
        float y = 0.0f, x = 0.0f, z = -len;

        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        var uv = new List<Vector2>();
        vertices.Add(new Vector3(-width, 0, z));
        vertices.Add(new Vector3(width, 0, z));

        var curveLen = 5;
        var curve = 0.0f;


        uv.Add(GetUv(vertices[0]));
        uv.Add(GetUv(vertices[1]));

        for (var i = 0; i < 1000; i++)
        {
            z += len;
            y -= height;
            x += curve;

            vertices.Add(new Vector3(x - width, y, z));
            vertices.Add(new Vector3(x + width, y, z));

            triangles.AddRange(new int[] { i * 2 + 0, i * 2 + 2, i * 2 + 1 });
            triangles.AddRange(new int[] { i * 2 + 1, i * 2 + 2, i * 2 + 3 });

            uv.Add(GetUv(vertices[vertices.Count - 2]));
            uv.Add(GetUv(vertices[vertices.Count - 1]));

            curveLen -= 1;
            if(curveLen == 0)
            {
                curveLen = (int)(Random.value * 3) + 2;
                curve = Random.value * 6 -3 ;
            }
        }
        //ゴール位置
        goalLine.position = new Vector3(x,y-0.5f,z);

        mesh.vertices = vertices.ToArray();

        mesh.triangles = triangles.ToArray();
        mesh.uv = uv.ToArray();

        var filter = GetComponent<MeshFilter>();
        filter.sharedMesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;

        var renderer = GetComponent<MeshRenderer>();
        renderer.material = material;
    }

    Vector2 GetUv(Vector3 vertice)
    {
        return new Vector2(vertice.x * 0.1f, vertice.z * 0.1f);
    }
	// Update is called once per frame
	void Update () {
		var span = System.DateTime.Now - startTime;
		string spanStr = (span.TotalMilliseconds / 1000).ToString("F");
		if (spanStr != lastSpanStr && Time.timeScale != 0) {
			//Debug.Log();
			timeText.text = spanStr;
			lastSpanStr = spanStr;
		}
	}
}
