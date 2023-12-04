using UnityEngine;

public class MapBuilder : MonoBehaviour
{

    [SerializeField] private Grid _grid;
    [SerializeField] private Vector2Int _mapSize;
    
    private Camera _mainCamera;
    private GameObject _currentTile;
    private Tile[,] _mapGrid;
    private Tile _currentTileComponent;
    
    private void Awake()
    {
        _mainCamera = Camera.main;
        InitializeTileGrid();
    }
    
    public void StartPlacingTile(GameObject tilePrefab)
    {
        if (_currentTile != null) return;
        
        _currentTile = Instantiate(tilePrefab);
        _currentTileComponent = _currentTile.GetComponent<Tile>();
    }
    
    private void InitializeTileGrid()
    { 
        _mapGrid = new Tile[_mapSize.x, _mapSize.y];
        
        for (int x = 0; x < _mapSize.x; x++)
        {
            for (int y = 0; y < _mapSize.y; y++)
            {
                _mapGrid[x, y] = null;
            }
        }
    }
    private void Update()
    {
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            
        if (Physics.Raycast(ray, out var hitInfo) && _currentTile != null)
        {
            var cellPosition = _grid.WorldToCell(hitInfo.point);
            var worldPosition = _grid.GetCellCenterWorld(cellPosition);
            
            _currentTile.transform.position = worldPosition;
            
            bool canPlaceTile = CanPlaceTile(cellPosition);
            _currentTileComponent.HighlightTile(canPlaceTile);

            if (Input.GetMouseButtonDown(0) && canPlaceTile)
            {
                _currentTileComponent.ReturnOriginalColor();
                _mapGrid[cellPosition.x, cellPosition.z] = _currentTileComponent;
                _currentTile = null;
            }
        }
    }
    
    private bool CanPlaceTile(Vector3Int cellPosition)
    {
        if (cellPosition.x < 0 || cellPosition.x >= _mapGrid.GetLength(0) ||
            cellPosition.z < 0 || cellPosition.z >= _mapGrid.GetLength(1))
        {
            return false;
        }
        return _mapGrid[cellPosition.x, cellPosition.z] == null;
    }
}