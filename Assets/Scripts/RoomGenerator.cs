using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomGenerator : MonoBehaviour
{
    public enum Direction { up, down, left, right }; //枚举类型
    public Direction direction; //生成枚举变量

    [Header("房间信息")]
    public GameObject roomPrefab;
    public int roomNumber;//房间数量
    public Color startColor,endColor;//最开始房间和最终的颜色
    public GameObject endRoom;

    
    [Header("位置控制")]
    public Transform generatorPoint;
    public float x0ffset;
    public float y0ffset;
    public LayerMask roomLayer;


    public List<GameObject> rooms = new List<GameObject>();


    void Start()
    {
        for (int i = 0; i<roomNumber; i++)
        {
            rooms.Add(Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity));
            ChangePointPos();
            //每生成一个位置要改变一下point位置
        }
        
        rooms[0].GetComponent<SpriteRenderer>().color = startColor;
       //rooms[roomNumber - 1].GetComponent<SpriteRenderer>().color = endColor;
        endRoom = rooms[0];
        foreach (var room in rooms )
        {
            if(room.transform.position.sqrMagnitude > endRoom.transform.position.sqrMagnitude){
                endRoom = room;
            }
        }
        endRoom.GetComponent<SpriteRenderer>().color = endColor;
    }


    void Update()
    {
        if(Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
        }     
    }

public void ChangePointPos(){
    do{
            direction = (Direction)Random.Range(0,4);
            switch (direction)
        {
            case Direction.up:
                generatorPoint.position += new Vector3(0,y0ffset,0);
                break;
            case Direction.down:
                generatorPoint.position += new Vector3(0,-y0ffset,0);
                break;
            case Direction.left:
                generatorPoint.position += new Vector3(-x0ffset,0,0);
                break;
            case Direction.right:
                generatorPoint.position += new Vector3(x0ffset,0,0);
                break;
        }
    }while(Physics2D.OverlapCircle(generatorPoint.position,0.2f,roomLayer));
}


}
