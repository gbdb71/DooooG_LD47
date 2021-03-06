﻿using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;
using Sirenix.Utilities;

public class GridEditor : SerializedMonoBehaviour
{
    public bool useCustomGrid = false;

    [Range(1, 25)] 
    public int gridSizeX;
    [Range(1, 25)] 
    public int gridSizeZ;

    [Range(0f, 1.5f), Space]
    public float wallPositionY = 0.5f;

    [ShowIf("useCustomGrid"), TableMatrix(HorizontalTitle = "Custom Grid", DrawElementMethod = "DrawColoredEnumElement", ResizableColumns = false, RowHeight = 20), Space(20)]
    public CellType[,] CustomCellDrawing = new CellType[20, 20];



    [FoldoutGroup("Settings"), Range(0, 1)] 
    public float extraSpaceBetweenCells = 0f;
    [FoldoutGroup("Settings"), Space]
    public bool getCellSizeFromPrefabScale = true;
    [FoldoutGroup("Settings"), HideIf("getCellSizeFromPrefabScale"), MinValue(0.1f)] 
    public float cellSize = 1f;
    [FoldoutGroup("Settings"), Space]
    public bool getWallSizeFromPrefabScale = true;
    [FoldoutGroup("Settings"), HideIf("getWallSizeFromPrefabScale"), MinValue(0.1f)]
    public float wallSize = 1f;
    [FoldoutGroup("Settings"), Space(20)]
    public bool resetPosition = true;
    

    [FoldoutGroup("References"), AssetList]
    public GameObject cellPrefab;
    [FoldoutGroup("References"), AssetList, Space]
    public GameObject wallPrefab;
    [FoldoutGroup("References"), AssetList, Space]
    public GameObject wallInvisiblePrefab;

    [FoldoutGroup("Internal Ref"), ChildGameObjectsOnly(IncludeSelf = false)]
    public Transform CellsFather;
    [FoldoutGroup("Internal Ref"), ChildGameObjectsOnly(IncludeSelf = false), Space]
    public Transform WallsFather;
    [FoldoutGroup("Internal Ref"), ChildGameObjectsOnly(IncludeSelf = false), Space]
    public Transform MapBorderFather;


    #region BUTTONS
    [Button("Regenerates the Cells", ButtonSizes.Gigantic), ShowIf("ShowButtons"), GUIColor(0.7f, 1f, 0.7f, 1f), PropertySpace(20)]
    public void CreateGrid()
    {
        ImmediateDestroyChilds(CellsFather);
        ImmediateDestroyChilds(WallsFather);
        ImmediateDestroyChilds(MapBorderFather);


        if (getCellSizeFromPrefabScale)
        {
            cellSize = cellPrefab.transform.localScale.x;
        }
        if (getWallSizeFromPrefabScale)
        {
            wallSize = wallPrefab.transform.localScale.x;
        }

        transform.position = new Vector3(cellSize* gridSizeX / 2, 0, cellSize* gridSizeZ / 2 );

		for (int y = 0; y < gridSizeZ; y++)
        {
            for(int x = 0; x < gridSizeX; x++)
            {
                if (useCustomGrid)
                {
                    if(CustomCellDrawing[x, gridSizeZ - y - 1] != CellType.Empty)
                    {
                        InstantiateCell(x, y);
                    } 
                    else 
                    {
                        InstantiateInvisibleWall(x, y);
                    }

                    if(CustomCellDrawing[x, gridSizeZ - y - 1] == CellType.Left_None || 
                       CustomCellDrawing[x, gridSizeZ - y - 1] == CellType.Left_Up)
                    {
                        InstantiateWallLeft(x, y);
					}

                    if(CustomCellDrawing[x, gridSizeZ - y-1] == CellType.None_Up ||
                       CustomCellDrawing[x, gridSizeZ - y-1] == CellType.Left_Up)
                    {
                        InstantiateWallUp(x, y);
                    }
                } 
                else 
                {
                    InstantiateCell(x, y);
                }
			}
		}
        
        InstantiateMapBorder();
    }

    [Button("Reset Custom Grid", ButtonSizes.Gigantic), ShowIf("useCustomGrid"), GUIColor(1f, 0.7f, 0.7f, 1f), PropertySpace(30)]
    public void ResetCustomGrid()
    {
        bool userChoice = true;
        if (CustomCellDrawing != null)
        {
            userChoice = EditorUtility.DisplayDialog("Custom Grid Reset",
                                                     "Hei, attento, guarda che se hai fatto delle modifiche alla CustomGrid cosi vengono perse, sei sicuro di voler proseguire?",
                                                     "Ma certo!", "Oh cazzo...");
        }
        if (userChoice)
        {
            CustomCellDrawing = new CellType[gridSizeX, gridSizeZ];
            ImmediateDestroyChilds(CellsFather);
            ImmediateDestroyChilds(WallsFather);
            ImmediateDestroyChilds(MapBorderFather);
        }
    }

    [FoldoutGroup("Extra Functions"), Button("Fill Grid With Flat Cells", ButtonSizes.Large), ShowIf("ShowFillGridWithBlankCells"), GUIColor(0.7f, 0.7f, 1f, 1f)]
    public void FillGridWithBlankCells()
    {
        for (int y = 0; y < CustomCellDrawing.GetLength(0); y++)
        {
            for (int x = 0; x < CustomCellDrawing.GetLength(1); x++)
            {
                CustomCellDrawing[y, x] = CellType.None_None;
            }
        }
    }

    #endregion


    #region INTERNAL
    private void ImmediateDestroyChilds(Transform target)
    {
        while(target.transform.childCount  > 0)
        {
            DestroyImmediate(target.transform.GetChild(0).gameObject);
		}
    }

    private void InstantiateCell(int x, int y)
    {
        GameObject tmp = (GameObject)PrefabUtility.InstantiatePrefab(cellPrefab);
        tmp.transform.localPosition = new Vector3((cellSize * (x - gridSizeX / 2)) + extraSpaceBetweenCells * x,
                                                  0,
                                                  (cellSize * (y - gridSizeZ / 2)) + extraSpaceBetweenCells * y);
        tmp.transform.localRotation = Quaternion.Euler(0, 0, 0);
        tmp.transform.parent = CellsFather.transform;
        tmp.name = "Cell [ " + x + " , " + y + " ]";
    }

    private void InstantiateWallUp(int x, int y)
    {
        GameObject tmp = (GameObject)PrefabUtility.InstantiatePrefab(wallPrefab);
        tmp.transform.localPosition = new Vector3((cellSize * (x - gridSizeX / 2)) + extraSpaceBetweenCells * x,
                                                  wallPositionY,
                                                  (cellSize * (y - gridSizeZ / 2)) + extraSpaceBetweenCells * y + (cellSize + extraSpaceBetweenCells) / 2);
        tmp.transform.localRotation = Quaternion.Euler(0, 90, 0);
        tmp.transform.parent = WallsFather.transform;
        tmp.name = "WallUp [ " + x + " , " + y + " ]";
    }

    private void InstantiateWallLeft(int x, int y)
    {
        GameObject tmp = (GameObject)PrefabUtility.InstantiatePrefab(wallPrefab);
        tmp.transform.localPosition = new Vector3((cellSize * (x - gridSizeX / 2)) + extraSpaceBetweenCells * x + (cellSize + extraSpaceBetweenCells) / 2 - cellSize - extraSpaceBetweenCells,
                                                  wallPositionY,
                                                  (cellSize * (y - gridSizeZ / 2)) + extraSpaceBetweenCells * y);
        tmp.transform.localRotation = Quaternion.Euler(0, 0, 0);
        tmp.transform.parent = WallsFather.transform;
        tmp.name = "WallUp [ " + x + " , " + y + " ]";
    }

    private void InstantiateInvisibleWall(int x, int y)
    {
        GameObject tmp = (GameObject)PrefabUtility.InstantiatePrefab(wallInvisiblePrefab);
        tmp.transform.localPosition = new Vector3((cellSize * (x - gridSizeX / 2)) + extraSpaceBetweenCells * x,
                                                  1,
                                                  (cellSize * (y - gridSizeZ / 2)) + extraSpaceBetweenCells * y);
        tmp.transform.localRotation = Quaternion.Euler(0, 0, 0);
        tmp.transform.parent = MapBorderFather.transform;
        tmp.name = "BorderInternal [ " + x + " , " + y + " ]";
    }

    private void InstantiateMapBorder()
    {
        GameObject tmp;
        for (int i = 0; i < gridSizeZ; i++)
        {
            //LEFT
            tmp = (GameObject)PrefabUtility.InstantiatePrefab(wallInvisiblePrefab);
            tmp.transform.localPosition = new Vector3((cellSize * (-gridSizeX / 2)) + (cellSize + extraSpaceBetweenCells) / 2 - cellSize - extraSpaceBetweenCells - cellSize / 2,
                                                      1,
                                                      (cellSize * (i - gridSizeZ / 2)) + extraSpaceBetweenCells * i);
            tmp.transform.localRotation = Quaternion.Euler(0, 0, 0);
            tmp.transform.parent = MapBorderFather.transform;
            tmp.name = "BorderLeft [ " + i + " ]";

            //RIGHT
            tmp = (GameObject)PrefabUtility.InstantiatePrefab(wallInvisiblePrefab);
            tmp.transform.localPosition = new Vector3((cellSize * Mathf.CeilToInt((float)gridSizeX / 2)) + (cellSize + extraSpaceBetweenCells) / 2 - extraSpaceBetweenCells - cellSize / 2,
                                                      1,
                                                      (cellSize * (i - gridSizeZ / 2)) + extraSpaceBetweenCells * i);
            tmp.transform.localRotation = Quaternion.Euler(0, 0, 0);
            tmp.transform.parent = MapBorderFather.transform;
            tmp.name = "BorderRight [ " + i + " ]";
        }

        for(int i = 0; i < gridSizeX; i++)
        {
            //DOWN
            tmp = (GameObject)PrefabUtility.InstantiatePrefab(wallInvisiblePrefab);
            tmp.transform.localPosition = new Vector3((cellSize * (i - gridSizeX / 2)) + extraSpaceBetweenCells * i + (cellSize + extraSpaceBetweenCells) / 2 - cellSize / 2 - extraSpaceBetweenCells,
                                          1,
                                          cellSize * (-gridSizeZ / 2) - cellSize);
            tmp.transform.localRotation = Quaternion.Euler(0, 90, 0);
            tmp.transform.parent = MapBorderFather.transform;
            tmp.name = "BorderDown [ " + i + " ]";

            //UP
            tmp = (GameObject)PrefabUtility.InstantiatePrefab(wallInvisiblePrefab);
            tmp.transform.localPosition = new Vector3((cellSize * (i - gridSizeX / 2)) + extraSpaceBetweenCells * i + (cellSize + extraSpaceBetweenCells) / 2 - cellSize / 2 - extraSpaceBetweenCells,
                                          1,
                                          cellSize * Mathf.CeilToInt((float)gridSizeZ / 2));
            tmp.transform.localRotation = Quaternion.Euler(0, 90, 0);
            tmp.transform.parent = MapBorderFather.transform;
            tmp.name = "BorderUp [ " + i + " ]";

        }
    }

	#endregion


	#region EDITOR GUI
	private static CellType DrawColoredEnumElement(Rect rect, CellType value)
    {
        if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
        {
            switch (value)
            {
                case CellType.Empty:
                    value = CellType.None_None;
                    break;
                case CellType.None_None:
                    value = CellType.Left_None;
                    break;
                case CellType.Left_None:
                    value = CellType.None_Up;
                    break;
                case CellType.None_Up:
                    value = CellType.Left_Up;
                    break;
                case CellType.Left_Up:
                    value = CellType.Empty;
                    break;
            }
            GUI.changed = true;
            Event.current.Use();
        }

        //UnityEditor.EditorGUI.DrawRect(rect.Padding(1f), value ? new Color(0.1f, 0.8f, 0.2f) : new Color(0, 0, 0, 0.5f));
        Rect tmpLeft = new Rect(rect);
        Rect tmpUp = new Rect(rect);
        tmpLeft.width /= 3;
        tmpUp.height /= 3;

        UnityEditor.EditorGUI.DrawRect(rect.Padding(1f), value != CellType.Empty ? new Color(0.1f, 0.4f, 0.1f, 1f)
                                                                         : new Color(0, 0, 0, 0.5f));

        switch (value)
        {
            case CellType.Left_None:
                UnityEditor.EditorGUI.DrawRect(tmpLeft.Padding(1f), Color.red);
                break;
            case CellType.None_Up:
                UnityEditor.EditorGUI.DrawRect(tmpUp.Padding(1f), Color.red);
                break;
            case CellType.Left_Up:
                UnityEditor.EditorGUI.DrawRect(tmpLeft.Padding(1f), Color.red);
                UnityEditor.EditorGUI.DrawRect(tmpUp.Padding(1f), Color.red);
                break;
        }

        return value;
    }

    private bool ShowButtons()
    {
        if (CustomCellDrawing == null)
            CustomCellDrawing = new CellType[gridSizeX, gridSizeZ];
        return !useCustomGrid || (gridSizeX == CustomCellDrawing.GetLength(0) && gridSizeZ == CustomCellDrawing.GetLength(1));
	}

    private bool ShowFillGridWithBlankCells()
    {
        return useCustomGrid && ShowButtons();
    }
    #endregion


    public enum CellType
    {
        Empty = 0,
        None_None = 1,
        Left_None = 2,
        None_Up = 3,
        Left_Up = 4
    }

}


