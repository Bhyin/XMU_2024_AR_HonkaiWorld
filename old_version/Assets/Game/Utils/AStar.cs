using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    namespace Utils
    {

        public class AStar
        {
            // 剪枝函数，输入坐标，能够通行则返回true
            public delegate bool PruningFunction(Vector2Int coord);

            private Vector2Int[] directions = new Vector2Int[4]
            {
                new Vector2Int(-1, 0),
                new Vector2Int(0, 1),
                new Vector2Int(1, 0),
                new Vector2Int(0, -1)
            };
            private Vector2Int[] neighbors = new Vector2Int[4];

            // 地图所有坐标
            private List<Vector2Int> allCoordinates = new List<Vector2Int>();
            private PruningFunction pruningFunction = default;

            private List<Vector2Int> path = new List<Vector2Int>();

            private HashSet<Vector2Int> closedSet = new HashSet<Vector2Int>();
            private HashSet<Vector2Int> openSet = new HashSet<Vector2Int>();
            private Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
            private Dictionary<Vector2Int, float> gScore = new Dictionary<Vector2Int, float>();
            private Dictionary<Vector2Int, float> fScore = new Dictionary<Vector2Int, float>();

            public AStar(List<Vector2Int> allCoordinates, PruningFunction pruningFunction)
            {
                this.allCoordinates = allCoordinates;
                this.pruningFunction = pruningFunction;
            }

            public List<Vector2Int> FindPath(Vector2Int start, Vector2Int end)
            {
                path.Clear();
                openSet.Clear();
                closedSet.Clear();
                cameFrom.Clear();
                gScore.Clear();
                fScore.Clear();

                openSet.Add(start);

                foreach (Vector2Int coord in allCoordinates)
                {
                    gScore[coord] = float.MaxValue;
                    fScore[coord] = float.MaxValue;
                }
                gScore[start] = 0;
                fScore[start] = Heuristic(start, end);

                Vector2Int current;
                while (openSet.Count > 0)
                {
                    current = GetLowestFScore();

                    if (current == end)
                    {
                        path = ReconstructPath(current);
                        break;
                    }

                    openSet.Remove(current);
                    closedSet.Add(current);

                    GetNeighbors(current);
                    foreach (Vector2Int neighbor in neighbors) // 遍历邻节点
                    {
                        if (!pruningFunction(neighbor) || closedSet.Contains(neighbor))
                            continue;
                        float tentativeGScore = gScore[current] + 1; // Assuming each move has a cost of 1

                        if (!openSet.Contains(neighbor))
                            openSet.Add(neighbor);
                        else if (tentativeGScore >= gScore[neighbor])
                            continue;

                        cameFrom[neighbor] = current;
                        gScore[neighbor] = tentativeGScore;
                        fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, end);
                    }
                }

                return path;
            }

            private static float Heuristic(Vector2Int a, Vector2Int b)
            {
                // Manhattan distance heuristic
                return Distance.D4(a, b);
            }

            private Vector2Int GetLowestFScore()
            {
                Vector2Int lowest = default;
                float lowestScore = float.MaxValue;

                foreach (Vector2Int tile in openSet)
                {
                    if (fScore[tile] < lowestScore)
                    {
                        lowest = tile;
                        lowestScore = fScore[tile];
                    }
                }

                return lowest;
            }

            private List<Vector2Int> ReconstructPath(Vector2Int current)
            {
                path.Add(current);

                while (cameFrom.ContainsKey(current))
                {
                    current = cameFrom[current];
                    path.Add(current);
                }

                path.Reverse();
                return path;
            }

            private void GetNeighbors(Vector2Int position)
            {
                for (int i = 0; i < neighbors.Length; ++i) neighbors[i] = position + directions[i];
            }
        }
    }
}

