using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

using System.IO;

public class AddVoxModel : MonoBehaviour {
	public TextAsset model;
	public float voxelSize;
	private SkinnedMeshRenderer renderer;

	private int sizeX = 0, sizeY = 0, sizeZ = 0;	
	private static List<Color> defaultColors;
	private string[] defaultColorsValues = new string[] {
		"255,255,255", "255,255,204", "255,255,153", "255,255,102", "255,255,51", "255,255,0", "255,204,255", "255,204,204", 
		"255,204,153", "255,204,102", "255,204,51", "255,204,0", "255,153,255", "255,153,204", "255,153,153", "255,153,102", 
		"255,153,51", "255,153,0", "255,102,255", "255,102,204", "255,102,153", "255,102,102", "255,102,51", "255,102,0", 
		"255,51,255", "255,51,204", "255,51,153", "255,51,102", "255,51,51", "255,51,0", "255,0,255", "255,0,204", 
		"255,0,153", "255,0,102", "255,0,51", "255,0,0", "204,255,255", "204,255,204", "204,255,153", "204,255,102", 
		"204,255,51", "204,255,0", "204,204,255", "204,204,204", "204,204,153", "204,204,102", "204,204,51", "204,204,0", 
		"204,153,255", "204,153,204", "204,153,153", "204,153,102", "204,153,51", "204,153,0", "204,102,255", "204,102,204", 
		"204,102,153", "204,102,102", "204,102,51", "204,102,0", "204,51,255", "204,51,204", "204,51,153", "204,51,102", 
		"204,51,51", "204,51,0", "204,0,255", "204,0,204", "204,0,153", "204,0,102", "204,0,51", "204,0,0", 
		"153,255,255", "153,255,204", "153,255,153", "153,255,102", "153,255,51", "153,255,0", "153,204,255", "153,204,204", 
		"153,204,153", "153,204,102", "153,204,51", "153,204,0", "153,153,255", "153,153,204", "153,153,153", "153,153,102", 
		"153,153,51", "153,153,0", "153,102,255", "153,102,204", "153,102,153", "153,102,102", "153,102,51", "153,102,0", 
		"153,51,255", "153,51,204", "153,51,153", "153,51,102", "153,51,51", "153,51,0", "153,0,255", "153,0,204", 
		"153,0,153", "153,0,102", "153,0,51", "153,0,0", "102,255,255", "102,255,204", "102,255,153", "102,255,102", 
		"102,255,51", "102,255,0", "102,204,255", "102,204,204", "102,204,153", "102,204,102", "102,204,51", "102,204,0", 
		"102,153,255", "102,153,204", "102,153,153", "102,153,102", "102,153,51", "102,153,0", "102,102,255", "102,102,204", 
		"102,102,153", "102,102,102", "102,102,51", "102,102,0", "102,51,255", "102,51,204", "102,51,153", "102,51,102", 
		"102,51,51", "102,51,0", "102,0,255", "102,0,204", "102,0,153", "102,0,102", "102,0,51", "102,0,0", 
		"51,255,255", "51,255,204", "51,255,153", "51,255,102", "51,255,51", "51,255,0", "51,204,255", "51,204,204", 
		"51,204,153", "51,204,102", "51,204,51", "51,204,0", "51,153,255", "51,153,204", "51,153,153", "51,153,102", 
		"51,153,51", "51,153,0", "51,102,255", "51,102,204", "51,102,153", "51,102,102", "51,102,51", "51,102,0", 
		"51,51,255", "51,51,204", "51,51,153", "51,51,102", "51,51,51", "51,51,0", "51,0,255", "51,0,204", 
		"51,0,153", "51,0,102", "51,0,51", "51,0,0", "0,255,255", "0,255,204", "0,255,153", "0,255,102", 
		"0,255,51", "0,255,0", "0,204,255", "0,204,204", "0,204,153", "0,204,102", "0,204,51", "0,204,0", 
		"0,153,255", "0,153,204", "0,153,153", "0,153,102", "0,153,51", "0,153,0", "0,102,255", "0,102,204", 
		"0,102,153", "0,102,102", "0,102,51", "0,102,0", "0,51,255", "0,51,204", "0,51,153", "0,51,102", 
		"0,51,51", "0,51,0", "0,0,255", "0,0,204", "0,0,153", "0,0,102", "0,0,51", "238,0,0", 
		"221,0,0", "187,0,0", "170,0,0", "136,0,0", "119,0,0", "85,0,0", "68,0,0", "34,0,0", 
		"17,0,0", "0,238,0", "0,221,0", "0,187,0", "0,170,0", "0,136,0", "0,119,0", "0,85,0", 
		"0,68,0", "0,34,0", "0,17,0", "0,0,238", "0,0,221", "0,0,187", "0,0,170", "0,0,136", 
		"0,0,119", "0,0,85", "0,0,68", "0,0,34", "0,0,17", "238,238,238", "221,221,221", "187,187,187", 
		"170,170,170", "136,136,136", "119,119,119", "85,85,85", "68,68,68", "34,34,34", "17,17,17", "0,0,0"
	};

	private struct Voxel {
		public short x;
		public short y;
		public short z;
		public short colorIndex;
		public Color color;

		public Voxel(short x, short y, short z, short colorIndex, Color color) {
			this.x = x;
			this.y = y;
			this.z = z;
			this.colorIndex = colorIndex;
			this.color = color;
		}
	}

	private enum Direction {UP, DOWN, FRONT, BACK, LEFT, RIGHT};

	void Start() {
		defaultColors = generateDefaultColors();

		addMesh();
		addSkeleton();
	}

	private List<Color> generateDefaultColors() {
		List<Color> colors = new List<Color>();
		foreach(string c in defaultColorsValues) {
			string[] parts = c.Split(',');
			float r =  float.Parse(parts[0]);
			float g =  float.Parse(parts[1]);
			float b =  float.Parse(parts[2]);
			float a = 255f;
			colors.Add (new Color(r/255, g/255, b/255, a/255));
		}
		return colors;
	}

	private void addMesh() {
		GameObject body = new GameObject("Body");
		body.transform.parent = gameObject.transform;
		renderer = body.AddComponent<SkinnedMeshRenderer>();
		renderer.sharedMesh = createMesh();
		renderer.material = Resources.Load("Materials/Voxel") as Material;
	}

	private Mesh createMesh() {
		Mesh mesh = new Mesh();
		mesh.name = model.name;

		List<Voxel> voxels = parseVox();
		voxels.TrimExcess();

		List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();
		List<Vector3> uv = new List<Vector3>();
		List<Color> colors = new List<Color>();

		bool[,,] voxelPositions = new bool[sizeX, sizeY, sizeZ];
		foreach(Voxel v in voxels) {
			voxelPositions[v.x, v.y, v.z] = true;
		}

		foreach(Voxel v in voxels) {
			createVertices(v, voxelPositions, vertices, triangles, colors);
		}
		mesh.Clear();
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.colors = colors.ToArray();

		mesh.RecalculateNormals();
		mesh.Optimize();

		return mesh;
	}

	void createVertices(Voxel v, bool[,,] voxelPositions, List<Vector3> vertices, List<int> triangles, List<Color> colors) {
		foreach(Direction d in Enum.GetValues(typeof(Direction))) {
			if(faceVisible(v, voxelPositions, d)) {
				addVerticesDirection(v, vertices, d);
				createTriangles(vertices, triangles);
				colors.Add (v.color);
				colors.Add (v.color);
				colors.Add (v.color);
				colors.Add (v.color);
			}
		}
	}

	private bool faceVisible(Voxel v, bool[,,] voxelPositions, Direction d) {
		switch(d) {
			case Direction.UP:
				if(v.y+1 == voxelPositions.GetLength(1)) return true;
				return !voxelPositions[v.x, v.y+1, v.z];
			case Direction.DOWN:
				if(v.y-1 < 0) return true;
				return !voxelPositions[v.x, v.y-1, v.z];
			case Direction.FRONT:
				if(v.z-1 < 0) return true;
				return !voxelPositions[v.x, v.y, v.z-1];
			case Direction.BACK:
				if(v.z+1 == voxelPositions.GetLength(2)) return true;
				return !voxelPositions[v.x, v.y, v.z+1];
			case Direction.LEFT:
				if(v.x-1 < 0) return true;
				return !voxelPositions[v.x-1, v.y, v.z];
			case Direction.RIGHT:
				if(v.x+1 == voxelPositions.GetLength(0)) return true;
				return !voxelPositions[v.x+1, v.y, v.z];
		}
		return true;
	}

	private void addVerticesDirection(Voxel v, List<Vector3> vertices, Direction d) {
		switch(d) {
			case Direction.UP:
				vertices.Add(new Vector3((v.x - 0.5f) * voxelSize, (v.y + 0.5f) * voxelSize, (v.z + 0.5f) * voxelSize));
				vertices.Add(new Vector3((v.x + 0.5f) * voxelSize, (v.y + 0.5f) * voxelSize, (v.z + 0.5f) * voxelSize));
				vertices.Add(new Vector3((v.x + 0.5f) * voxelSize, (v.y + 0.5f) * voxelSize, (v.z - 0.5f) * voxelSize));
				vertices.Add(new Vector3((v.x - 0.5f) * voxelSize, (v.y + 0.5f) * voxelSize, (v.z - 0.5f) * voxelSize));
				break;
			case Direction.DOWN:
				vertices.Add(new Vector3((v.x - 0.5f) * voxelSize, (v.y - 0.5f) * voxelSize, (v.z - 0.5f) * voxelSize));
				vertices.Add(new Vector3((v.x + 0.5f) * voxelSize, (v.y - 0.5f) * voxelSize, (v.z - 0.5f) * voxelSize));
				vertices.Add(new Vector3((v.x + 0.5f) * voxelSize, (v.y - 0.5f) * voxelSize, (v.z + 0.5f) * voxelSize));
				vertices.Add(new Vector3((v.x - 0.5f) * voxelSize, (v.y - 0.5f) * voxelSize, (v.z + 0.5f) * voxelSize));
				break;
			case Direction.FRONT:
				vertices.Add(new Vector3((v.x - 0.5f) * voxelSize, (v.y - 0.5f) * voxelSize, (v.z - 0.5f) * voxelSize));
				vertices.Add(new Vector3((v.x - 0.5f) * voxelSize, (v.y + 0.5f) * voxelSize, (v.z - 0.5f) * voxelSize));
				vertices.Add(new Vector3((v.x + 0.5f) * voxelSize, (v.y + 0.5f) * voxelSize, (v.z - 0.5f) * voxelSize));
				vertices.Add(new Vector3((v.x + 0.5f) * voxelSize, (v.y - 0.5f) * voxelSize, (v.z - 0.5f) * voxelSize));
				break;
			case Direction.BACK:
				vertices.Add(new Vector3((v.x + 0.5f) * voxelSize, (v.y - 0.5f) * voxelSize, (v.z + 0.5f) * voxelSize));
				vertices.Add(new Vector3((v.x + 0.5f) * voxelSize, (v.y + 0.5f) * voxelSize, (v.z + 0.5f) * voxelSize));
				vertices.Add(new Vector3((v.x - 0.5f) * voxelSize, (v.y + 0.5f) * voxelSize, (v.z + 0.5f) * voxelSize));
				vertices.Add(new Vector3((v.x - 0.5f) * voxelSize, (v.y - 0.5f) * voxelSize, (v.z + 0.5f) * voxelSize));
				break;
			case Direction.LEFT:
				vertices.Add(new Vector3((v.x - 0.5f) * voxelSize, (v.y - 0.5f) * voxelSize, (v.z + 0.5f) * voxelSize));
				vertices.Add(new Vector3((v.x - 0.5f) * voxelSize, (v.y + 0.5f) * voxelSize, (v.z + 0.5f) * voxelSize));
				vertices.Add(new Vector3((v.x - 0.5f) * voxelSize, (v.y + 0.5f) * voxelSize, (v.z - 0.5f) * voxelSize));
				vertices.Add(new Vector3((v.x - 0.5f) * voxelSize, (v.y - 0.5f) * voxelSize, (v.z - 0.5f) * voxelSize));
				break;
			case Direction.RIGHT:
				vertices.Add(new Vector3((v.x + 0.5f) * voxelSize, (v.y - 0.5f) * voxelSize, (v.z - 0.5f) * voxelSize));
				vertices.Add(new Vector3((v.x + 0.5f) * voxelSize, (v.y + 0.5f) * voxelSize, (v.z - 0.5f) * voxelSize));
				vertices.Add(new Vector3((v.x + 0.5f) * voxelSize, (v.y + 0.5f) * voxelSize, (v.z + 0.5f) * voxelSize));
				vertices.Add(new Vector3((v.x + 0.5f) * voxelSize, (v.y - 0.5f) * voxelSize, (v.z + 0.5f) * voxelSize));
				break;
		}
	}
	
	void createTriangles(List<Vector3> vertices, List<int> triangles) {
		triangles.Add (vertices.Count-4);
		triangles.Add (vertices.Count-3);
		triangles.Add (vertices.Count-2);
		triangles.Add (vertices.Count-4);
		triangles.Add (vertices.Count-2);
		triangles.Add (vertices.Count-1);
	}
	
	private List<Voxel> parseVox() {
		string path = AssetDatabase.GetAssetPath(model);
		BinaryReader stream = new BinaryReader(File.Open(path, FileMode.Open));
		List<Voxel> voxels = new List<Voxel>();

		string magic = new string(stream.ReadChars(4));
		int version = stream.ReadInt32();

		if(magic == "VOX ") {
			while(stream.BaseStream.Position < stream.BaseStream.Length) {
				string chunkName = new string(stream.ReadChars(4));
				int chunkSize = stream.ReadInt32();
				int childChunks = stream.ReadInt32();

				if(chunkName == "SIZE") {
					sizeX = stream.ReadInt32();
					sizeY = stream.ReadInt32();
					sizeZ = stream.ReadInt32();
				} else if(chunkName == "XYZI") {
					int nVoxels = stream.ReadInt32();

					for(int i = 0; i < nVoxels; i++) {
						byte x = stream.ReadByte();
						byte y = stream.ReadByte();
						byte z = stream.ReadByte();
						byte c = stream.ReadByte();
						voxels.Add(new Voxel(x, y, z, c, defaultColors[c-1]));
					}
				} else if(chunkName == "RGBA") {
					List<Color> customColors = new List<Color>();
					for(int i = 0; i < 256; i++) {
						float r = stream.ReadByte();
						float g = stream.ReadByte();
						float b = stream.ReadByte();
						float a = stream.ReadByte();

						Color c = new Color(r/255, g/255, b/255, a/255);
						customColors.Add(c);
					}
					for(int i = 0; i < voxels.Count; i++) {
						Voxel v = voxels[i];
						voxels[i] = new Voxel(v.x, v.y, v.z, v.colorIndex, customColors[v.colorIndex-1]);
					}
				} else {
					stream.ReadBytes(chunkSize);
				}
			}
			stream.Close();
		}
		return voxels;
	}

	private void addSkeleton() {
		//TODO: Add skeleton
		//TODO: Add mesh collider
		renderer.rootBone = null;
	}
}
