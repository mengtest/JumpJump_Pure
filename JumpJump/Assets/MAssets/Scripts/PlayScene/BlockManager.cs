
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockManager : MonoBehaviour
{

	static BlockManager g_Instance;
	public static BlockManager Instance(){
		return g_Instance;
	}

	public GameObject CloneDynamicBrick;
	ObjectPool<Brick> m_Pool_Brick;
	ObjectPool<Block> m_Pool_Block;
	List<Block> m_Blocks = new List<Block> ();

	void Awake ()
	{
		g_Instance=this;
		InitPool ();

		CreateTestBlock();
	}

	void InitPool ()
	{
		m_Pool_Brick = new ObjectPool<Brick> (100, 20);
		m_Pool_Brick.NewObject = NewBrick;
		m_Pool_Brick.Init ();
		
	}
	
	Brick NewBrick ()
	{
		GameObject go = GameObject.Instantiate (CloneDynamicBrick) as GameObject;
		go.transform.parent = this.transform;
		go.SetActive (false);
		Brick b = new Brick (go);
		return b;
	}

	public Brick ObtainBrick ()
	{
		return m_Pool_Brick.Obtain ();
	}

	void Update ()
	{
		for (int i=0; i<m_Blocks.Count; i++) {
			m_Blocks [i].Update ();
		}
	}

	void CreateTestBlock ()
	{
		Vector3 startPot=Vector3.zero;
		Vector3 endPot=Vector3.one*5f;

		Block b=new H_Block(null,BlockType.H,4,startPot,endPot,5f,
		                    Block.GetDiriction(startPot,endPot),new Vector3[]{
			new Vector3(-5,0,0),new Vector3(-5,0,0),new Vector3(-5,0,0),new Vector3(-3,0,0)
		});
		m_Blocks.Add(b);

		startPot=new Vector3(6,0,0);
		endPot=new Vector3(8,0,0);
		b=new H_Block(null,BlockType.H,2,startPot,endPot,6f,
		              Block.GetDiriction(startPot,endPot),null);
		m_Blocks.Add(b);

	}

}

