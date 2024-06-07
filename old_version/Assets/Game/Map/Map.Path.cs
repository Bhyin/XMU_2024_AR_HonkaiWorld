using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Map
{
    public partial class MapManager : MonoBehaviour
    {

        public List<Vector3> GetPath() => m_PathPositions;
        
        public void UpdatePath()
        {
            m_Path.Clear();
            m_PathPositions.Clear();
            m_Path = m_AStarInstance.FindPath(m_Start, m_End);

            if(m_Path.Count > 2)
            {
                Vector2Int coord;
                MapSquareBehaviour square;

                m_PathPositions.Add(this[m_Path[0]].Position);

                for (int i = 1; i < m_Path.Count - 1; ++i)
                {
                    coord = m_Path[i];
                    square = this[coord];
                    square.SetType(Type.ROAD);
                    square.SetDirection(m_Path[i + 1] - coord);
                    m_PathPositions.Add(square.Position);
                }
                m_PathPositions.Add(this[m_Path[m_Path.Count - 1]].Position);
            }
            
        }

    }
}

