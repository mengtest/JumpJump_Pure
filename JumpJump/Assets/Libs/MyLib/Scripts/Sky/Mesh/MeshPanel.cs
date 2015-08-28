using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[ExecuteInEditMode,RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshPanel : MonoBehaviour
{
	public float Width = 5;
	public float shadoWidth=5;
//	public Color MianColor;
//	public Color ShodowColor;  

	public List<Line> Lines;
	public bool shadowOn = true;
	public bool halfOfBase = true;
	private int VerticesBase = 8;
	private int VerticesNumber;
	private int TrianglesBase = 24;

	[Serializable]
	public class Line
	{

		public List<Vector3> points;

		public Line(List<Vector3> points,Color MainColor,Color ShodowColor){
			this.points = points;
			this.MainColor = MainColor;
			this.ShodowColor = ShodowColor;
		}
		public Color MainColor = Color.red;
		public Color ShodowColor= Color.red;
	}

	private bool isDirty = true;
	// Update is called once per frame
	void Update ()
	{

		#if UNITY_EDITOR
				isDirty = true;
				MeshRectangle ();

		#else
		
		#endif
//		isDirty = true;
//		MeshRectangle ();
		CreateMesh ();
	}

	void MeshRectangle ()
	{ 
//		if (Lines.Count < 1)
//			return;
//
//		if (halfOfBase) {
//			ShodowColor = new Color (MianColor.r / 2f, MianColor.g / 2f, MianColor.b / 2f, 0.5f);
//		}
//		MeshFilter mFilter = gameObject.GetComponent<MeshFilter> ();  
//		MeshRenderer mRen = gameObject.GetComponent<MeshRenderer> (); 
//		mRen.sortingOrder = 2;
//		if (shadowOn) {
//			VerticesBase = 8;
//			TrianglesBase = 36;
//		} else {
//			VerticesBase = 4;
//			TrianglesBase = 12;
//		}
//
//		VerticesNumber = (Lines.Count - 1) * VerticesBase;
//		Vector3[] vertices = new Vector3[VerticesNumber];
//
//		int[] triangles = new int[(Lines.Count - 1) * TrianglesBase];
//		Color[] colors = new Color[VerticesNumber];
//
//		Vector3[] normals = new Vector3[VerticesNumber];
//		Vector2[] uvs = new Vector2[VerticesNumber];
//		for (int i=0; i<Lines.Count-1; i++) {
//			int offset = VerticesBase * i;
//			int offsetTriangles = TrianglesBase * i;
//			Vector2 k = new Vector2 (Lines [i + 1].x - Lines [i].x, Lines [i + 1].y - Lines [i].y);
//		
//			float lenth = k.x * k.x + k.y + k.y;
//			float angle = Vector2.Angle (Vector2.zero, k) * Mathf.PI / 180f;
//			float distan = Vector2.Distance (Vector2.zero, k);
//
//			float width_x_half = (k.y / distan * Width / 2f);
//			float width_y_half = (k.x / distan * Width / 2f);
//			float s_x_half = (k.y / distan * (Width + shadoWidth) / 2f);
//			float s_y_half = (k.x / distan * (Width + shadoWidth) / 2f);
//
//
//
//			vertices [0 + offset] = new Vector3 (Lines [i].x - width_x_half, Lines [i].y + width_y_half, Lines [i].z);
//			vertices [1 + offset] = new Vector3 (Lines [i].x + width_x_half, Lines [i].y - width_y_half, Lines [i].z);
//			vertices [2 + offset] = new Vector3 (Lines [i + 1].x + width_x_half, Lines [i + 1].y - width_y_half, Lines [i + 1].z);
//			vertices [3 + offset] = new Vector3 (Lines [i + 1].x - width_x_half, Lines [i + 1].y + width_y_half, Lines [i + 1].z);
//
//
//			triangles [0 + offsetTriangles] = 0 + offset;
//			triangles [1 + offsetTriangles] = 1 + offset;
//			triangles [2 + offsetTriangles] = 2 + offset;
//			triangles [3 + offsetTriangles] = 2 + offset;
//			triangles [4 + offsetTriangles] = 3 + offset;
//			triangles [5 + offsetTriangles] = 0 + offset;
//
//			
//			if (i > 0) {
//				triangles [6 + offsetTriangles] = 0 + offset;
//				triangles [7 + offsetTriangles] = 1 + offset;
//				triangles [8 + offsetTriangles] = 2 + (offset - VerticesBase);
//				triangles [9 + offsetTriangles] = 2 + (offset - VerticesBase);
//				triangles [10 + offsetTriangles] = 3 + (offset - VerticesBase);
//				triangles [11 + offsetTriangles] = 0 + offset;
//			} else {
//				triangles [6 + offsetTriangles] = 0;
//				triangles [7 + offsetTriangles] = 0;
//				triangles [8 + offsetTriangles] = 0;
//				triangles [9 + offsetTriangles] = 0;
//				triangles [10 + offsetTriangles] = 0;
//				triangles [11 + offsetTriangles] = 0;
//			}
//
//			normals [0 + offset] = new Vector3 (0, 0, -5);
//			normals [1 + offset] = new Vector3 (0, 0, -5);
//			normals [2 + offset] = new Vector3 (0, 0, -5);
//			normals [3 + offset] = new Vector3 (0, 0, -5); 
//
//			colors [0 + offset] = MianColor;
//			colors [1 + offset] = MianColor;
//			colors [2 + offset] = MianColor;
//			colors [3 + offset] = MianColor;
//
//			uvs [0 + offset] = new Vector2 (0, 0); 
//			uvs [1 + offset] = new Vector2 (1, 0);
//			uvs [2 + offset] = new Vector2 (1, 1); 
//			uvs [3 + offset] = new Vector2 (0, 1);   
//
//
//			if (shadowOn) {
//				vertices [4 + offset] = new Vector3 (Lines [i].x - s_x_half, Lines [i].y + s_y_half, Lines [i].z);
//				vertices [5 + offset] = new Vector3 (Lines [i].x + s_x_half, Lines [i].y - s_y_half, Lines [i].z);
//				vertices [6 + offset] = new Vector3 (Lines [i + 1].x + s_x_half, Lines [i + 1].y - s_y_half, Lines [i + 1].z);
//				vertices [7 + offset] = new Vector3 (Lines [i + 1].x - s_x_half, Lines [i + 1].y + s_y_half, Lines [i + 1].z);
//
//
//	
//
//				triangles [12 + offsetTriangles] = 1 + offset;
//				triangles [13 + offsetTriangles] = 5 + offset;
//				triangles [14 + offsetTriangles] = 6 + offset;
//				triangles [15 + offsetTriangles] = 6 + offset;
//				triangles [16 + offsetTriangles] = 2 + offset;
//				triangles [17 + offsetTriangles] = 1 + offset;
//
//				triangles [18 + offsetTriangles] = 4 + offset;
//				triangles [19 + offsetTriangles] = 0 + offset;
//				triangles [20 + offsetTriangles] = 3 + offset;
//				triangles [21 + offsetTriangles] = 3 + offset;
//				triangles [22 + offsetTriangles] = 7 + offset;
//				triangles [23 + offsetTriangles] = 4 + offset;
//
//
//				if (i > 0) {
//					triangles [24 + offsetTriangles] = 5 + offset;
//					triangles [25 + offsetTriangles] = 1 + offset;
//					triangles [26 + offsetTriangles] = 2 + (offset - VerticesBase);
//					triangles [27 + offsetTriangles] = 2 + (offset - VerticesBase);
//					triangles [28 + offsetTriangles] = 6 + (offset - VerticesBase);
//					triangles [29 + offsetTriangles] = 5 + offset;
//
//					triangles [30 + offsetTriangles] = 0 + offset;
//					triangles [31 + offsetTriangles] = 4 + offset;
//					triangles [32 + offsetTriangles] = 7 + (offset - VerticesBase);
//					triangles [33 + offsetTriangles] = 7 + (offset - VerticesBase);
//					triangles [34 + offsetTriangles] = 3 + (offset - VerticesBase);
//					triangles [35 + offsetTriangles] = 0 + offset;
//				} else {
//					triangles [24 + offsetTriangles] = 0;
//					triangles [25 + offsetTriangles] = 0;
//					triangles [26 + offsetTriangles] = 0;
//					triangles [27 + offsetTriangles] = 0;
//					triangles [28 + offsetTriangles] = 0;
//					triangles [29 + offsetTriangles] = 0;
//
//					triangles [30 + offsetTriangles] = 0;
//					triangles [31 + offsetTriangles] = 0;
//					triangles [32 + offsetTriangles] = 0;
//					triangles [33 + offsetTriangles] = 0;
//					triangles [34 + offsetTriangles] = 0;
//					triangles [35 + offsetTriangles] = 0;
//				}
//		
//
//		
//				normals [4 + offset] = new Vector3 (0, 0, -5);
//				normals [5 + offset] = new Vector3 (0, 0, -5);
//				normals [6 + offset] = new Vector3 (0, 0, -5);
//				normals [7 + offset] = new Vector3 (0, 0, -5); 
//	
//
//
//				colors [4 + offset] = ShodowColor;
//				colors [5 + offset] = ShodowColor;
//				colors [6 + offset] = ShodowColor;
//				colors [7 + offset] = ShodowColor;
//
//
//		
//				uvs [4 + offset] = new Vector2 (0, 0); 
//				uvs [5 + offset] = new Vector2 (1, 0);
//				uvs [6 + offset] = new Vector2 (1, 1); 
//				uvs [7 + offset] = new Vector2 (0, 1);  
//			}
//		}
//
//		Mesh mesh = new Mesh ();  
//		mesh.hideFlags = HideFlags.DontSave;
//		mesh.vertices = vertices;
//		mesh.triangles = triangles;
//		mesh.colors = colors;
//		mesh.uv = uvs; 
//		mesh.normals = normals;
//		mFilter.mesh = mesh;
	}

	List<Vector3 > vertices = new List<Vector3 > ();
	
	List<int> triangles = new List<int> ();
	List<Color> colors = new List<Color> ();
	
	List<Vector3 > normals = new List<Vector3 > ();
	List<Vector2 > uvs = new List<Vector2 > ();

	void CreateMesh(){
		if (Lines.Count < 1 || (!isDirty))
			return;
		MeshFilter mFilter = gameObject.GetComponent<MeshFilter> ();  
		MeshRenderer mRen = gameObject.GetComponent<MeshRenderer> (); 
		vertices.Clear ();
		triangles.Clear ();
		colors.Clear ();
		normals.Clear ();
		uvs.Clear ();

		mRen.sortingOrder = 2;
		if (shadowOn) {
			VerticesBase = 8;
			TrianglesBase = 36;
		} else {
			VerticesBase = 4;
			TrianglesBase = 12;
		}

		foreach(Line line in Lines){
			drawLine (line);
		}

		Mesh mesh = new Mesh ();  
		mesh.hideFlags = HideFlags.DontSave;
		mesh.vertices = vertices.ToArray ();
		mesh.triangles = triangles.ToArray ();
		mesh.colors = colors.ToArray ();
		mesh.uv = uvs.ToArray (); 
		mesh.normals = normals.ToArray ();
		mFilter.mesh = mesh;
		isDirty = false;
	}
	
	void drawLine (Line line)
	{ 
//		Debug.Log(vertices.Count);
		List<Vector3> points = line.points;
		int PreVecticesNumber = vertices.Count;
		int PreTrianglesNumber = triangles.Count;

		if (points.Count < 1)
			return;
		if (halfOfBase) {
			line.ShodowColor = new Color (line.MainColor.r / 2f, line.MainColor.g / 2f, line.MainColor.b / 2f, 0.5f);
		}
	
		
//		VerticesNumber = (points.Count - 1) * VerticesBase;
	
		for (int i=0; i<points.Count-1; i++) {
			int offset = VerticesBase * i + PreVecticesNumber;
			int offsetTriangles = TrianglesBase * i + PreTrianglesNumber;
			Vector2 k = new Vector2 (points [i + 1].x - points [i].x, points [i + 1].y - points [i].y);
			
			float lenth = k.x * k.x + k.y + k.y;
			float angle = Vector2.Angle (Vector2.zero, k) * Mathf.PI / 180f;
			float distan = Vector2.Distance (Vector2.zero, k);
			
			float width_x_half = (k.y / distan * Width / 2f);
			float width_y_half = (k.x / distan * Width / 2f);
			float s_x_half = (k.y / distan * (Width + shadoWidth) / 2f);
			float s_y_half = (k.x / distan * (Width + shadoWidth) / 2f);
			
			
			
			vertices.Add (new Vector3 (points [i].x - width_x_half, points [i].y + width_y_half, points [i].z));
			vertices.Add (new Vector3 (points [i].x + width_x_half, points [i].y - width_y_half, points [i].z));
			vertices.Add (new Vector3 (points [i + 1].x + width_x_half, points [i + 1].y - width_y_half, points [i + 1].z));
			vertices.Add (new Vector3 (points [i + 1].x - width_x_half, points [i + 1].y + width_y_half, points [i + 1].z));
			
			
			triangles.Add (0 + offset);
			triangles.Add (1 + offset);
			triangles.Add (2 + offset);
			triangles.Add (2 + offset);
			triangles.Add (3 + offset);
			triangles.Add (0 + offset);
			
			
			if (i > 0) {
				triangles.Add (0 + offset);
				triangles.Add (1 + offset);
				triangles.Add (2 + (offset - VerticesBase));
				triangles.Add (2 + (offset - VerticesBase));
				triangles.Add (3 + (offset - VerticesBase));
				triangles.Add (0 + offset);
			} else {
				triangles.Add (0);
				triangles.Add (0);
				triangles.Add (0);
				triangles.Add (0);
				triangles.Add (0);
				triangles.Add (0);
			}
			
			normals.Add (new Vector3 (0, 0, -5));
			normals.Add (new Vector3 (0, 0, -5));
			normals.Add (new Vector3 (0, 0, -5));
			normals .Add (new Vector3 (0, 0, -5)); 
			
			colors .Add (line.MainColor);
			colors.Add (line.MainColor);
			colors .Add (line.MainColor);
			colors .Add (line.MainColor);
			
			uvs.Add (new Vector2 (0, 0)); 
			uvs.Add (new Vector2 (1, 0));
			uvs.Add (new Vector2 (1, 1)); 
			uvs.Add (new Vector2 (0, 1));   
			
			
			if (shadowOn) {
				vertices .Add (new Vector3 (points [i].x - s_x_half, points [i].y + s_y_half, points [i].z));
				vertices.Add (new Vector3 (points [i].x + s_x_half, points [i].y - s_y_half, points [i].z));
				vertices .Add(new Vector3 (points [i + 1].x + s_x_half, points [i + 1].y - s_y_half, points [i + 1].z));
				vertices  .Add(new Vector3 (points [i + 1].x - s_x_half, points [i + 1].y + s_y_half, points [i + 1].z));
				
				triangles.Add (1 + offset);
				triangles.Add (5 + offset);
				triangles.Add (6 + offset);
				triangles .Add (6 + offset);
				triangles .Add (2 + offset);
				triangles .Add (1 + offset);
				
				triangles .Add (4 + offset);
				triangles.Add (0 + offset);
				triangles.Add (3 + offset);
				triangles.Add (3 + offset);
				triangles .Add (7 + offset);
				triangles .Add (4 + offset);
				
				if (i > 0) {
					triangles .Add (5 + offset);
					triangles.Add (1 + offset);
					triangles.Add (2 + (offset - VerticesBase));
					triangles .Add (2 + (offset - VerticesBase));
					triangles .Add (6 + (offset - VerticesBase));
					triangles .Add (5 + offset);
					
					triangles .Add (0 + offset);
					triangles .Add (4 + offset);
					triangles .Add (7 + (offset - VerticesBase));
					triangles .Add (7 + (offset - VerticesBase));
					triangles .Add (3 + (offset - VerticesBase));
					triangles .Add (0 + offset);
				} else {
					triangles.Add (0);
					triangles .Add (0);
					triangles .Add (0);
					triangles.Add (0);
					triangles .Add (0);
					triangles .Add (0);
					
					triangles .Add (0);
					triangles.Add (0);
					triangles .Add (0);
					triangles .Add (0);
					triangles .Add (0);
					triangles .Add (0);
				}
				
				
				
				normals .Add (new Vector3 (0, 0, -5));
				normals .Add (new Vector3 (0, 0, -5));
				normals .Add (new Vector3 (0, 0, -5));
				normals .Add (new Vector3 (0, 0, -5)); 
				
				
				
				colors .Add (line.ShodowColor);
				colors .Add (line.ShodowColor);
				colors .Add (line.ShodowColor);
				colors .Add (line.ShodowColor);
				
				
				
				uvs .Add (new Vector2 (0, 0)); 
				uvs .Add (new Vector2 (1, 0));
				uvs .Add (new Vector2 (1, 1)); 
				uvs .Add (new Vector2 (0, 1));  
			}
		}
//		Debug.Log(vertices.Count);

	}


	public void AddLine(List<Vector3> line,Color MainColor,Color ShodowColor){
		Lines.Add (new Line(line,MainColor,ShodowColor));
		isDirty = true;
	}

}
