using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ScannerEffectDemo : MonoBehaviour
{
	public Transform ScannerOrigin;
	public Material EffectMaterial;
	public float ScanDistance;
    public Flashlight flash;
	private Camera _camera;

	bool _scanning;
	GameObject[] POI;
    GameObject[] Keys;
	Color[] original;
    Color[] keyCol;
	Scannable[] _scannables;
	float time2wait;

	bool highlighted = false;

	void Start()
	{
		_scannables = FindObjectsOfType<Scannable>();

		Invoke ("fillList", 0.2f);
    }

	void fillList(){
		POI = GameObject.FindGameObjectsWithTag ("Battery");
		original = new Color[POI.Length];
		flash = GetComponent<Flashlight>();
		for (int i = 0; i < POI.Length; i++) 
		{
			original [i] = POI [i].GetComponent<MeshRenderer> ().material.GetColor ("_EmissionColor");
		}
        Keys = GameObject.FindGameObjectsWithTag("key");
        keyCol = new Color[Keys.Length];
        for (int i = 0; i < Keys.Length; i++)
        {
            keyCol[i] = Keys[i].GetComponent<MeshRenderer>().material.GetColor("_EmissionColor");
        }

    }

	void Update()
	{
		if (_scanning)
		{
			ScanDistance += Time.deltaTime * 9;
			highlighted = true;
			foreach (GameObject p in POI) 
			{
				try{
					if (Vector3.Distance (ScannerOrigin.position, p.transform.position) <= ScanDistance)
					{
						p.GetComponent<MeshRenderer> ().material.SetColor ("_EmissionColor", Color.yellow);

					}
				}catch(MissingReferenceException){
				}
			}
            foreach (GameObject p in Keys)
            {
				try{
	                if (Vector3.Distance(ScannerOrigin.position, p.transform.position) <= ScanDistance)
	                {

	                    p.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.red);

	                }
				}catch(MissingReferenceException){
				}
            }

            //			foreach (Scannable s in _scannables)
            //			{
            //				if (Vector3.Distance(ScannerOrigin.position, s.transform.position) <= ScanDistance)
            //					s.Ping();
            //			}
        }

		if (Time.time >= time2wait && highlighted)
		{
			for (int i = 0; i < POI.Length; i++) 
			{
				try{
					POI [i].GetComponent<MeshRenderer> ().material.SetColor ("_EmissionColor", original[i]);
				}catch(MissingReferenceException){
				}
			}
            for (int i = 0; i < Keys.Length; i++)
            {
				try{
                	Keys[i].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", keyCol[i]);
				}catch(MissingReferenceException){
				}
            }
            highlighted = false;
		}

		//if (Input.GetKeyDown(KeyCode.E))
		//{
		//	time2wait = Time.time + 6;
		//	_scanning = true;
		//	ScanDistance = 0;
		//}

//		if (Input.GetMouseButtonDown(0))
//		{
//			Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
//			RaycastHit hit;
//		
//			if (Physics.Raycast(ray, out hit))
//			{
//				_scanning = true;
//				ScanDistance = 0;
//				ScannerOrigin.position = hit.point;
//			}
//		}
	}

	void OnEnable()
	{
		_camera = GetComponent<Camera>();
		_camera.depthTextureMode = DepthTextureMode.Depth;
	}

	[ImageEffectOpaque]
	void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		EffectMaterial.SetVector("_WorldSpaceScannerPos", ScannerOrigin.position);
		EffectMaterial.SetFloat("_ScanDistance", ScanDistance);
		RaycastCornerBlit(src, dst, EffectMaterial);
	}

	void RaycastCornerBlit(RenderTexture source, RenderTexture dest, Material mat)
	{
		// Compute Frustum Corners
		float camFar = _camera.farClipPlane;
		float camFov = _camera.fieldOfView;
		float camAspect = _camera.aspect;

		float fovWHalf = camFov * 0.5f;

		Vector3 toRight = _camera.transform.right * Mathf.Tan(fovWHalf * Mathf.Deg2Rad) * camAspect;
		Vector3 toTop = _camera.transform.up * Mathf.Tan(fovWHalf * Mathf.Deg2Rad);

		Vector3 topLeft = (_camera.transform.forward - toRight + toTop);
		float camScale = topLeft.magnitude * camFar;

		topLeft.Normalize();
		topLeft *= camScale;

		Vector3 topRight = (_camera.transform.forward + toRight + toTop);
		topRight.Normalize();
		topRight *= camScale;

		Vector3 bottomRight = (_camera.transform.forward + toRight - toTop);
		bottomRight.Normalize();
		bottomRight *= camScale;

		Vector3 bottomLeft = (_camera.transform.forward - toRight - toTop);
		bottomLeft.Normalize();
		bottomLeft *= camScale;

		// Custom Blit, encoding Frustum Corners as additional Texture Coordinates
		RenderTexture.active = dest;

		mat.SetTexture("_MainTex", source);

		GL.PushMatrix();
		GL.LoadOrtho();

		mat.SetPass(0);

		GL.Begin(GL.QUADS);

		GL.MultiTexCoord2(0, 0.0f, 0.0f);
		GL.MultiTexCoord(1, bottomLeft);
		GL.Vertex3(0.0f, 0.0f, 0.0f);

		GL.MultiTexCoord2(0, 1.0f, 0.0f);
		GL.MultiTexCoord(1, bottomRight);
		GL.Vertex3(1.0f, 0.0f, 0.0f);

		GL.MultiTexCoord2(0, 1.0f, 1.0f);
		GL.MultiTexCoord(1, topRight);
		GL.Vertex3(1.0f, 1.0f, 0.0f);

		GL.MultiTexCoord2(0, 0.0f, 1.0f);
		GL.MultiTexCoord(1, topLeft);
		GL.Vertex3(0.0f, 1.0f, 0.0f);

		GL.End();
		GL.PopMatrix();
	}
    public void pulse() {
        time2wait = Time.time + 6;
        _scanning = true;
        ScanDistance = 0;
    }
}
