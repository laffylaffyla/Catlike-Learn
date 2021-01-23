using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class Graph : MonoBehaviour
{

	public Transform pointPrefab;

	[Range(10, 100)]
	public int resolution = 10;

	public GraphFunctionName function;

	Transform[] points;

	static GraphFunction[] functions =
	{
		SineFunction,
		Sine2DFuntion,
		MultiSineFunction,
		MultiSine2DFunction,
		Ripple,
		Cylinder,
		Sphere,
		Torus
	};

	void Awake ()
	{
		float step = 2f / resolution;
		Vector3 scale = Vector3.one * step;
		points = new Transform[resolution * resolution];
		for(int i = 0; i < points.Length; i++)
        {
			Transform point = Instantiate(pointPrefab);
			point.localScale = scale;
			point.SetParent(transform, false);
			points[i] = point;
        }
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		float t = Time.time;
		GraphFunction f = functions[(int)function];
		float step = 2f / resolution;
		for (int i = 0, z = 0; z < resolution; z++)
        {
			float v = (z + 0.5f) * step - 1f;
			for(int x = 0; x < resolution; x++, i++)
            {
				float u = (x + 0.5f) * step - 1f;
				points[i].localPosition = f(u, v, t);
            }
        }
	}

	static Vector3 SineFunction(float x, float z, float t)
    {
		Vector3 p;
		p.x = x;
		p.y = Sin(PI * (x + t));
		p.y += Sin(PI * (z + t));
		p.y *= 0.5f;
		p.z = z;
		return p;
	}

	static Vector3 MultiSineFunction(float x, float z, float t)
	{
		Vector3 p;
		p.x = x;
		p.y = Sin(PI * (x + t));
		p.y += Sin(2f * PI * (x + t)) / 2f;
		p.y *= 2f / 3f;
		p.z = z;
		return p;
	}

	static Vector3 Sine2DFuntion(float x, float z, float t)
    {
		Vector3 p;
		p.x = x;
		p.y = Sin(PI * (x + t));
		p.y += Sin(PI * (z + t));
		p.y *= 0.5f;
		p.z = z;
		return p;
    }

	static Vector3 MultiSine2DFunction(float x, float z, float t)
    {
		Vector3 p;
		p.x = x;
		p.y = 4f * Sin(PI * (x + z + t * 0.5f));
		p.y += Sin(PI * (x + t));
		p.y += Sin(2f * PI * (z + 2f * t)) * 0.5f;
		p.y *= 1f / 5.5f;
		p.z = z;
		return p;
    }

	static Vector3 Ripple(float x,float z,float t)
    {
		Vector3 p;
		float d = Sqrt(x * x + z * z);
		p.x = x;
		p.y = Sin(PI * (4f * d - t));
		p.y /= 1f + 10f * d;
		p.z = z;
		return p;
    }

	static Vector3 Cylinder (float u,float v,float t)
    {
		Vector3 p;
		float r = 0.8f + Sin(PI * (6f * u + 2f * v + t)) * 0.2f;
		p.x = r * Sin(PI * u);
		p.y = v;
		p.z = r * Cos(PI * u);
		return p;
    }

	static Vector3 Sphere (float u, float v, float t)
	{
		Vector3 p;
		float r = 0.8f + Sin(PI * (6f * u + t)) * 0.1f;
		r += Sin(PI * (4f * v + t)) * 0.1f;
		float s = r * Cos(PI * 0.5f * v);
		p.x = s * Sin(PI * u);
		p.y = r * Sin(PI * 0.5f * v);
		p.z = s * Cos(PI * u);
		return p;
	}

	static Vector3 Torus (float u, float v, float t)
	{
		Vector3 p;
		float r1 = 0.7f + 0.1f * Sin(PI * (6f * u + 0.5f * t));
		float r2 = 0.15f + 0.05f * Sin(PI * (8f * u + 4f * v + 2f * t));
		float s = r2 * Cos(PI * v) + 0.5f + r1;
		p.x = s * Sin(PI * u);
		p.y = r2 * Sin(PI * v);
		p.z = s * Cos(PI * u);
		return p;
	}
}