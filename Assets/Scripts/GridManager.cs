using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CubeType {Sine};
public class GridManager : MonoBehaviour {

	List<GameObject> cubePool; // For object pooling down the line
	public TextAsset levelMap;
	public float heightUnit = 0.7f;
	GameObject[,] grid;

	public delegate void CubeTransform();
	CubeTransform cubeTransformationFunction;
	public bool infinteScrolling; // Not actually handled yet
	// Use this for initialization
	void Awake () {
		CreateGrid();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreateGrid() {
		if(levelMap != null) {
			string temp = levelMap.text;
			string[] lines = temp.Split('\n');

			GameObject tempBlock = Resources.Load<GameObject>("GroundSinWave0.2Blue0");
			CubeBehavior.gridManager = this;
			SetGridDimensions(lines);
			for(int i = 0; i < lines.Length; i++) {
				char[] blocks = lines[i].ToCharArray();
				for(int k = 0; k < blocks.Length; k++) {
					int xPos = Mathf.RoundToInt(k / 2);
					tempBlock = ParseBlockType(blocks[k]);
					k++;
					if(tempBlock != null) {
						grid[xPos, i] = (GameObject)Instantiate(tempBlock, new Vector3(xPos, float.Parse(blocks[k].ToString()) * heightUnit, i), Quaternion.identity);
					}
				}
			}
		}
	}

	void SetGridDimensions(string[] lines) {
		int height = lines.Length;
		int width = 0;
		foreach(string line in lines) {
			if(Mathf.RoundToInt(line.Length / 2) > width) {
				width = Mathf.RoundToInt(line.Length / 2);
			}
		}

		grid = new GameObject[width,height];
	}

	void ExpandGrid() {
		// Used for inifinite mode
	}

	GameObject[] GetOrthogonalNeighbors(GameObject cubeToCheck) {
		GameObject[] returnVal = new GameObject[4];
		Vector2 gridPos = FindCubePosition(cubeToCheck);
		int x = Mathf.RoundToInt(gridPos.x);
		int y = Mathf.RoundToInt(gridPos.y);
		if(y < grid.GetLength(1) - 2 && CubeExistsAt(x, y + 1)) {
			returnVal[0] = grid[x, y + 1];
		}
		if(y > 0 && CubeExistsAt(x, y -1)) {
			returnVal[1] = grid[x, y - 1];
		}
		if(x > 0 && CubeExistsAt(x - 1, y)) {
			returnVal[2] = grid[x - 1, y];
		}
		if(x < grid.GetLength(0) - 2 && CubeExistsAt(x + 1, y)) {
			returnVal[3] = grid[x + 1, y] ;
		}
		 
		return returnVal;
	}

	GameObject[] GetDiagonalNeighbors(GameObject cubeToCheck) {
		List<GameObject> returnVal = new List<GameObject>();
		Vector2 gridPos = FindCubePosition(cubeToCheck);
		int x = Mathf.RoundToInt(gridPos.x);
		int y = Mathf.RoundToInt(gridPos.y);
		if(x > 0) {
			if(y > 0 && CubeExistsAt(x - 1, y - 1)){ 
				returnVal.Add(grid[x - 1, y - 1]);
				}
			if(y < grid.GetLength(1) - 2 && CubeExistsAt(x - 1, y + 1)) {
				returnVal.Add(grid[x - 1, y + 1]);
			}
		}
		if(x < grid.GetLength(0) - 2) {
			if(y > 0 && CubeExistsAt(x + 1, y - 1)){ 
				returnVal.Add(grid[x + 1, y - 1]);
				}
			if(y < grid.GetLength(1) - 2 && CubeExistsAt(x + 1, y + 1)) {
				returnVal.Add(grid[x + 1, y + 1]);
			}
		}
		return returnVal.ToArray();
	}

	GameObject[] GetAllNeighbors(GameObject cube) {
		GameObject[] orthogonal = GetOrthogonalNeighbors(cube);
		GameObject[] diagonal = GetDiagonalNeighbors(cube);
		Debug.Log(orthogonal.Length);
		Debug.Log(diagonal.Length);
		Array.Resize<GameObject>(ref orthogonal, orthogonal.Length + diagonal.Length + 1);
		Debug.Log(orthogonal.Length);
		Array.Copy(diagonal, 0, orthogonal, orthogonal.Length + 1, diagonal.Length);
		return orthogonal;
	}

	void StartWaterWave(int row) {
		for(int i = 0; i < grid.GetLength(0); i++) {
			if(!grid[i, row].Equals(null) && grid[i, row].tag == "GroundCube") {
				grid[i, row].GetComponent<CubeBehavior>().StartBigWave();
			}
		}
	}

	void StartSeismicWave(int x, int y) {
		
	}

	GameObject ParseBlockType(char type) {
		// x and y are for calculation 
		// i means nothing just holding a border mark
		switch(type) {
		case 'b':
			return Resources.Load<GameObject> ("ShellFish");
		case 'c':
			return Resources.Load<GameObject> ("coralv");
		case 'd':
			return Resources.Load<GameObject> ("GroundTallPlantBlue0");
		case 'e':
			return Resources.Load<GameObject> ("Sardin");
		case 'g':
			return Resources.Load<GameObject> ("GardenEelContainer");
		case 's':
			return Resources.Load<GameObject> ("GroundSinWave0.2Blue0");
		case 'z':
			return Resources.Load<GameObject> ("StargazerContainer");

		// not using dark cubes in testLevel 1
		case 't':
			return Resources.Load<GameObject> ("GroundSinWave0.2Blue1");
		case 'u':
			return Resources.Load<GameObject> ("GroundSinWave0.2Blue2");
		case 'v':
			return Resources.Load<GameObject> ("GroundSinWave0.2Blue3");
		
		default:
			return null;//Resources.Load<GameObject>("Empty"); // make an empty object prefab as default
		} ;
	}

	Vector2 FindCubePosition(GameObject cube) {
		for(int i = 0; i < grid.GetLength(0); i++) {
			for(int k = 0; k < grid.GetLength(1); k++) {
				if(grid[i, k].Equals(cube)) {
					return new Vector2(i, k);
				}
			}
		}

		return new Vector2(-1, -1);
	}

	bool CubeExistsAt(int x, int y) {
		return grid[x, y] != null;
	}

	bool CubeExistsAt(Vector2 pos) {
		return grid[Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y)] != null;
	}

}
