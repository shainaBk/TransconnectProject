using System;
using TransconnectProject.Model;
namespace TransconnectProject.Util
{
    /*public class Tree<T>
    {
        protected Node<T> root;

        public Tree(Node<T> r)
        {
			this.root = r;
        }
		
    }*/

    public class Node<T>
	{
		private T key;
		private List<Node<T>> childs;
		//Tree leaf
		public Node(T k)
		{
			this.key = k;
			this.childs = new List<Node<T>>();
		}
		//Tree with kids
		public Node(T k, List<Node<T>> childs)
		{
			this.key = k;
			this.childs = childs;
		}

		public T Key { get => this.key; set => this.key = value; }
		public List<Node<T>> Childs{ get => this.childs; set => this.childs = value; }

		public bool isEmpty()
		{
			if (this.key == null && this.childs == null)
				return true;
			return false;
		}
		public bool isaLeaf()
		{
			if (this.Childs == null|| this.childs.Count().Equals(0))
				return true;
			return false;
		}
	}

    public class SalarieTree
    {
        protected SalarieNode root;
        public SalarieTree(SalarieNode r)
        {
			this.root = r;
        }
		public SalarieNode Root { get => this.root; set => this.root = value;}

		//À TESTER
		public bool isEmpty()
		{
			if (this.root == null || this.root.isEmpty())
				return true;
			return false;
		}

		/*public string showTree()
		{
			if (!this.isEmpty())
			{
				if(this.root.isaLeaf())
					return this.root.Key.ToString();
				else
					return this.root.showKids(this.root,true,1);
            }
			return null;
		}*/
		//À TESTER
		public static SalarieNode getaNewNode(Salarie root)
		{
			SalarieNode newNode = new SalarieNode(root);
			SalarieNode currentNode=null;
			if (root.Employ.Count > 0)
			{
				foreach (Salarie item in root.Employ)
				{
					if (item.Employ.Count > 0)
						currentNode = getaNewNode(item);
					else
						currentNode = new SalarieNode(item);
                    newNode.Childs.Add(currentNode);
                }
				return newNode;
			}
			else
				return newNode;

		}
        public override string ToString()
        {
            if (!this.isEmpty())
            {
                if (this.root.isaLeaf())
                    return this.root.Key.ToString();
                else
                    return this.root.showKids(this.root, true, 1);
            }
            return null;
        }
    }

    public class SalarieNode : Node<Salarie>
    {
        public SalarieNode(Salarie k) : base(k)
        {
        }
        public SalarieNode(Salarie k, List<Node<Salarie>> childs) : base(k, childs)
        {
        }
        //Temporary
        public string showKids(SalarieNode s, bool isRoot,int nbTab) {
			string m=null;
			if (this.isaLeaf() && !isRoot)
				return  this.Key.ToString();
			else
			{
				m = this.Key.ToString();

                foreach (SalarieNode item in this.Childs)
				{
					m += "\n"+toolForTab(nbTab)+"|"+"\n"+ toolForTab(nbTab) + "---> " + item.showKids(item,false,nbTab+1);
				}
				return m;
			}
        }

		/**
		 * 
		 * Cette methode return n fois "\t"
		 * 
		 * */
		public static string toolForTab(int n)
		{
			string tab = null;
			for (int i = 0; i < n; i++)
			{
				tab += "\t";
			}
			return tab;
		}
    }
}
