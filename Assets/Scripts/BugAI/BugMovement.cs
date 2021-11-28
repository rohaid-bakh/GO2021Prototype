using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BugMovement : MonoBehaviour
{
    public int gridWidth;
    public int gridHeight;
    public float cellSize;
    public Vector3Int originPosition;

    private Pathfinding pathfinding;
    
    private GridMap<PathNode> grid;
    private List<PathNode> path;
    private List<Vector3> pathVectorList = new List<Vector3>();

    public Tilemap map;
    
    private Rigidbody2D rigidbody;

    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        Debug.Log("Origin Position " + originPosition);
        pathfinding = new Pathfinding(gridWidth, gridHeight, cellSize, map.CellToWorld(originPosition));
        grid = pathfinding.GetGrid();

        for (int x = 0; x < grid.GetWidth(); x++) {
            for (int y = 0; y < grid.GetHeight(); y++) {
                PathNode pathNode = grid.GetGridMapObject(x, y);
                if (map.GetTile(new Vector3Int(x, y, 0) - originPosition) != null) {
                    pathNode.isWalkable = false;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            grid.GetXY(transform.position, out int startX, out int startY);
            grid.GetXY(mousePosition, out int endX, out int endY);

            print("Start: " + startX + " " + startY);
            print("End: " + endX + " " + endY);

            path = pathfinding.FindPath(startX, startY, endX, endY);
            
            foreach (PathNode node in path)
            {
                // Debug.Log(node.x + " " + node.y);
            
            }
        }
    }

    private void FixedUpdate() {
        if (path != null)
        {
            if (path.Count > 0)
            {
                Vector3 destination = grid.getWorldPosition(path[0].x, path[0].y) + new Vector3(cellSize * .5f, cellSize * .5f, 0);
                Vector3 velocity = (destination - transform.position).normalized * speed;
                rigidbody.MovePosition(transform.position + velocity * Time.fixedDeltaTime);

                if (Vector3.Distance(transform.position, destination) < 0.1f)
                {
                    path.RemoveAt(0);
                }
            } else 
            {
                path = null;
            }
        }
    }
}
