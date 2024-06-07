using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using URamdom = UnityEngine.Random;

namespace Game.Map
{
    public partial class MapManager : MonoBehaviour
    {
        public void ChangeSquareType(Type sourceType, Type targetType)
        {
            foreach(var square in m_SquareDict.Values.ToList())
            {
                if(square.type == sourceType)square.type = targetType;
            }
        }


        public Vector2Int RandomCoord()
        {
            int x = URamdom.Range(0, m_Row - 1);
            int y = URamdom.Range(0, m_Col - 1);
            return new Vector2Int(x, y);
        }

        public Vector2Int RandomSpace()
        {
            Vector2Int randomCoord = RandomCoord();
            while (this[randomCoord].type != Type.SPACE)
            {
                randomCoord = RandomCoord();
            }
            return randomCoord;
        }
    }
}

