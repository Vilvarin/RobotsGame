using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RobotsGame
{
    /// <summary>
    /// Рандомно создаёт карту после загрузки сцены
    /// </summary>
    public class RandomBoardGenerator : MonoBehaviour, IBoardGenerating {
        /// <summary>Ширина карты</summary>
        public int width = 20;
        /// <summary>Высота карты</summary>
        public int height = 11;
        /// <summary>Шанс создания блока грунта в процентах</summary>
        public int chanseForDirt = 50;
        /// <summary>Шанс создания блока камня в процентах</summary>
        public int chanseForStone = 20;
        /// <summary>Максимальное количество ресурсов на карте</summary>
        public int maxCountOfResources = 5;
        /// <summary>Минимальное количество ресурсов на карте</summary>
        public int minCountOfResources = 3;

        /// <summary>Префаб платформы</summary>
        public GameObject platform;
        /// <summary>Префаб ресурсов</summary>
        public GameObject resource;

        /// <summary>Массив префабов внешних стен</summary>
        public GameObject[] outherWall;
        /// <summary>Массив префабов грунта</summary>
        public GameObject[] dirt;
        /// <summary>Массив префабов камня</summary>
        public GameObject[] stone;

        /// <summary>Массив с полным набором тайлов</summary>
        public Tile[] AllTiles { get; private set; }
        /// <summary>Массив с тайлами платформ</summary>
        List<Tile> _platformTiles = new List<Tile>();
        public List<Tile> PlatformTiles { get { return _platformTiles; } }

        public int ResourceCount { get; private set; }
        
        Transform _transform;

        void Awake()
        {
            ResourceCount = 0;
            _transform = GetComponent<Transform>();

            AllTiles = new Tile[width * height];
        }

        public void CreateBoard()
        {
            InitialiseTiles();
            List<Tile> emptyTiles = FillingTiles();
            AddResources(emptyTiles);
        }

        void InitialiseTiles()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Tile tile = new Tile(new Vector2(i, j));
                    AllTiles[i*height + j] = tile;
                }
            }
        }

        List<Tile> FillingTiles()
        {
            List<Tile> emptyTiles = new List<Tile>();

            foreach (Tile tile in AllTiles)
            {
                Vector2 coor = tile.coor;

                if (coor.x == 0 || coor.y == 0 || coor.x == width - 1 || coor.y == height - 1)
                    tile.block = outherWall[Random.Range(0, outherWall.Length)];
                else if (coor.x == 1)
                {
                    tile.block = platform;
                    _platformTiles.Add(tile);
                }
                else
                {
                    int chanse = Random.Range(0, 100);
                    if (chanse < chanseForDirt)
                        tile.block = dirt[Random.Range(0, dirt.Length)];
                    else if (chanse < chanseForDirt + chanseForStone)
                        tile.block = stone[Random.Range(0, stone.Length)];
                    else
                        emptyTiles.Add(tile);
                }

                if (tile.block != null)
                {
                    GameObject inst = Instantiate(tile.block, tile.coor, Quaternion.identity) as GameObject;
                    inst.transform.SetParent(_transform);
                    tile.block = inst;
                }
                    
            }

            return emptyTiles;
        }

        void AddResources(List<Tile> emptyTiles)
        {
            ResourceCount = Random.Range(minCountOfResources, maxCountOfResources + 1);
            for (int i = 0; i < ResourceCount; i++)
            {
                Tile tile = emptyTiles[Random.Range(0, emptyTiles.Count)];
                tile.block = resource;
                emptyTiles.Remove(tile);
                GameObject inst = Instantiate(tile.block, tile.coor, Quaternion.identity) as GameObject;
                inst.transform.SetParent(_transform);
                tile.block = inst;
            }
        }
    }
}

