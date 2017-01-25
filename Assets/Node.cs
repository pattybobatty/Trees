using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour 
{
	[SerializeField] Node _left;
	[SerializeField] Node _right;
	[SerializeField] int _data;
	[SerializeField] Material _selectedMaterial;
	[SerializeField] Material _defaultMaterial;
	private GameObject _model;
	private GUIText _text;


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	private Node()
	{
		
	}

	public static Node Create(int data, Node left, Node right)
	{
		GameObject model = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		Node newNode = model.AddComponent<Node>();
		newNode.name = data.ToString(); 
		newNode._data = data;
		newNode._left = left;
		newNode._right = right;
		newNode.HookupNodes();

		GameObject textGO = new GameObject();
		textGO.name = "Text_" + data;
		TextMesh text = textGO.AddComponent<TextMesh>();
		text.transform.parent = newNode.transform;
		text.transform.localPosition = Vector3.zero;
		text.text = data.ToString();
		text.alignment = TextAlignment.Center;
		text.anchor = TextAnchor.MiddleCenter;
		text.color = Color.black;
		return newNode;
	}

	private void HookupNodes()
	{
		if ( _left != null )
		{
			_left.transform.position = transform.position + new Vector3(-1.0f, -1.0f, 0.0f);

			GameObject edge = GameObject.CreatePrimitive(PrimitiveType.Cube);
			edge.transform.localScale = new Vector3(0.1f, 1.0f, 0.1f);

			edge.transform.position = transform.position + new Vector3(0.0f, -1.0f, 0.0f);
			edge.transform.parent = transform;
			Quaternion cachedRotation = transform.localRotation;
			transform.Rotate(new Vector3(0.0f, 0.0f, -45.0f), Space.World);
			edge.transform.parent = null;
			transform.localRotation = cachedRotation;
			edge.transform.SetParent(transform, true);
			edge.name = "left";
			edge.transform.parent = transform;

			_left.transform.parent = transform;
		}

		if ( _right != null )
		{
			_right.transform.position = transform.position + new Vector3(1.0f, -1.0f, 0.0f);

			GameObject edge = GameObject.CreatePrimitive(PrimitiveType.Cube);

		
			edge.transform.position = transform.position + new Vector3(0.0f, -1.0f, 0.0f);
			edge.transform.parent = transform;
			Quaternion cachedRotation = transform.localRotation;
			transform.Rotate(new Vector3(0.0f, 0.0f, 45.0f), Space.World);
			edge.transform.parent = null;
			transform.localRotation = cachedRotation;
			edge.transform.SetParent(transform, true);
			edge.name = "right";
			edge.transform.localScale = new Vector3(0.1f, 1.0f, 0.1f);
			edge.transform.parent = transform;
			_right.transform.parent = transform;
		}
	}

	public static void Insert(int data, Node root)
	{
		root.StartCoroutine(root.InsertRoutine(data,root));
	}

	public static void Inserter(int data, Node root)
	{
		Node current = root;
		// Is it bigger or smaller?
		while ( true )
		{
			if ( data > current._data )
			{
				if ( current._right == null )
				{
					current._right = Create(data, null, null);
					current._right.transform.parent = current.transform;
					current._right.transform.position = current.transform.position + new Vector3(1.5f, -1.0f, 0.0f );
					break;
				}
				else
				{
					current = current._right;
				}
			}
			else if ( data < current._data )
			{
				if ( current._left == null )
				{
					current._left = Create(data, null, null);
					current._left.transform.parent = current.transform;
					current._left.transform.position = current.transform.position + new Vector3(-1.5f, -1.0f, 0.0f );
					break;
				}
				else
				{
					current = current._left;
				}
			}
			else
			{
				Debug.LogWarningFormat("[Node] Value {0} already exists in the tree, not readding", data);
				break;
			}
		}
	}

	public IEnumerator InsertRoutine(int data, Node root, float time = 0.25f)
	{
		Node current = root;

		Color cachedColor = Color.white;//root.GetComponent<Renderer>().material.color;
		// Is it bigger or smaller?
		while ( true )
		{
			Renderer renderer = current.GetComponent<Renderer>();
			renderer.material.color = Color.yellow;
			if ( data > current._data )
			{
				yield return new WaitForSeconds(time);
				renderer.material.color = cachedColor;
				if ( current._right == null )
				{
					current._right = Create(data, null, null);
					current._right.transform.parent = current.transform;
					current._right.transform.position = current.transform.position + new Vector3(1.5f, -1.0f, 0.0f );
					current._right.GetComponent<Renderer>().material.color = Color.green;
					yield return new WaitForSeconds(time);
					current._right.GetComponent<Renderer>().material.color = cachedColor;
					break;
				}
				else
				{
					current = current._right;
				}
	    	}
			else if ( data < current._data )
			{
				yield return new WaitForSeconds(time);
				renderer.material.color = cachedColor;
				if ( current._left == null )
				{
					
					//yield return new WaitForSeconds(0.5f);
					current._left = Create(data, null, null);
					current._left.transform.parent = current.transform;
					current._left.transform.position = current.transform.position + new Vector3(-1.5f, -1.0f, 0.0f );
					current._left.GetComponent<Renderer>().material.color = Color.green;
					yield return new WaitForSeconds(time);
					current._left.GetComponent<Renderer>().material.color = cachedColor;
					break;
				}
				else
				{
					
					current = current._left;
				}

			}
			else
			{
				Debug.LogWarningFormat("[Node] Value {0} already exists in the tree, not readding", data);
				break;
			}
		}
	}

	public static void Insert(Node newNode, Node root)
	{
		
	}

	IEnumerator Touch(Node node)
	{
		Renderer renderer = node.GetComponent<Renderer>();
		Color cachedColor = renderer.material.color;
		renderer.material.color = Color.yellow;
		yield return new WaitForSeconds(0.5f);
		renderer.material.color = cachedColor;

	}

	IEnumerator InOrderTraversal(Node root)
	{
		yield return null;
	}

	public IEnumerator PreOrderTraversalRecursive(Node node)
	{
		yield return StartCoroutine(Touch(node));
		if ( node._left != null )
		{
			yield return PreOrderTraversalRecursive(node._left);
		}

		if ( node._right != null )
		{
			yield return PreOrderTraversalRecursive(node._right);
		}

	}

	public IEnumerator PreOrderTraversal(Node root)
	{
		Stack<Node> stack = new Stack<Node>();
		stack.Push(root);
		Node current;
		while ( stack.Count != 0 )
		{
			current = stack.Pop();
			yield return StartCoroutine(Touch(current));

			if ( current._right != null )
			{
				stack.Push(current._right);
			}

			if ( current._left != null )
			{
				stack.Push(current._left);
			}
		}
	}

	IEnumerator PostOrderTraversal(Node root)
	{
		yield return null;
	}

	public Node FindLargest(Node root)
	{
		return null;
	}
}
