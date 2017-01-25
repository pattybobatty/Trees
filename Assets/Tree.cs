using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour 
{
	[SerializeField]
	Node _root;
	// Use this for initialization
	void Start () 
	{
		StartCoroutine(_root.PreOrderTraversal(_root));
	}

	IEnumerator RandomFillupRoutine(int count)
	{
		
		_root = Node.Create(UnityEngine.Random.Range(0, 100), null,null);
		count--;
		while ( count > 0 )
		{
			yield return StartCoroutine(_root.InsertRoutine(UnityEngine.Random.Range(0,100), _root, 0.0f));
			count--;
			
		}

		yield return new WaitForSeconds(2.0f);
		yield return StartCoroutine(_root.PreOrderTraversalRecursive(_root));
	}

	void Basic ()
	{
		Node root = Node.Create(3, null, null);
		Node.Insert(4, root);
		Node.Insert(5, root);
		Node.Insert(2, root);
		Node.Insert(1, root);
		Node.Insert(8, root);



		_root = root;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
