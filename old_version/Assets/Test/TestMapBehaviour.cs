using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using URandom = UnityEngine.Random;


public class TestMapBehaviour : MonoBehaviour
{
    //Map map = null;
    //[SerializeField] SquareRenderingProperty squareRenderingProperty;
    //public InputManager interactionManager;

    //public SquareType type = SquareType.START;

    //private void Awake()
    //{
    //    CreateMap();
    //}

    //private void OnEnable()
    //{
    //    interactionManager.pressEvent += OnPressCallback;
    //    //interactionManager.longPressEvent += OnLongPressCallback;
    //    //interactionManager.clickEvent += OnClickCallback;
    //    //interactionManager.releaseEvent += OnReleaseCallback;
    //}

    //public void OnPressCallback()
    //{
    //    int randrow = URandom.Range(0, 9);
    //    int rnadcol = URandom.Range(0, 9);
    //    map.SetSquareType(new Vector2Int(randrow, rnadcol), type);
    //}
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    map.UpdateMaterial();
    //}

    ///// <summary>
    ///// 创建地图，但不修改地图方块的状态
    ///// </summary>
    //public void CreateMap()
    //{
    //    // 获取地图数据实例
    //    map = Map.instance;
    //    // 设置地图渲染属性
    //    map.SetSquareRenderProperty(squareRenderingProperty);


    //    Transform child;
    //    MeshRenderer renderer;
    //    string[] coordStr;
    //    int row, col;
    //    for(int i = 0; i < transform.childCount; ++i)
    //    {
    //        child = transform.GetChild(i);
    //        coordStr = child.name.Split('_');
    //        row = int.Parse(coordStr[0]);
    //        col = int.Parse(coordStr[1]);

    //        renderer = child.GetComponent<MeshRenderer>();
    //        map.AddSquare(new Vector2Int(row, col), child, ref renderer);
    //    }
    //}

    //private IEnumerator CreateMapCoroutine()
    //{
    //    // 通过协程来表达创建地图的过程，实现动画效果
    //    yield return null;
    //}

    ///// <summary>
    ///// 初始化地图
    ///// 创建起点、终点、检测障碍物，设置敌人路径等，同时播放动画来表达初始化地图的过程
    ///// </summary>
    //public void InitMap()
    //{
        
    //}

    //private IEnumerator InitMapCoroutine()
    //{
    //    // 通过协程表达初始化地图的过程，实现动画效果
    //    yield return null;
    //}

    ///// <summary>
    ///// 在交互中控制地图旋转
    ///// </summary>
    ///// <param name="factor"></param>
    //public void SetMapRotation(float factor)
    //{
    //}

    ///// <summary>
    ///// 交互中控制地图缩放
    ///// </summary>
    ///// <param name="factor"></param>
    //public void SetMapScale(float factor)
    //{

    //}
}
