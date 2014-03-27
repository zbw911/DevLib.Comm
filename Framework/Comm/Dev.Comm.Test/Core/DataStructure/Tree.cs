//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Dev.Comm.DataStructure
//{
//    /***
//     *   构建树形
//     *   示例：
//     *    /// <summary>
//    /// 
//    /// </summary>
//    class ModelTypeTreeBuilder : TreeBuilder<ModelType>
//    {
//        public ModelTypeTreeBuilder(IEnumerable<ModelType> list)
//            : base(list)
//        {
//        }

//        public override List<ModelType> GetChild(IEnumerable<ModelType> list, ModelType node)
//        {
//            return list.Where(x => x.ParentId == node.ModelId).ToList();
//        }

//        public override ModelType GetRoot(IEnumerable<ModelType> list)
//        {
//            return list.First(x => x.ModelId == 0);
//        }
//    }
//     * 
//     * 
//     * USEAGE：
//     *     public static Node<ModelType> ToTree()
//        {
//            ModelTypeTreeBuilder mttb = new ModelTypeTreeBuilder(Syslist);

//            return mttb.Builder();

//        }
//     * 
//     * 
//     */


//    /// <summary>
//    /// 结点
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    public class Node<T> where T : class
//    {
//        /// <summary>
//        /// 当前结点
//        /// </summary>
//        public T Current { get; set; }
//        /// <summary>
//        /// 子结点
//        /// </summary>
//        public List<Node<T>> Children { get; set; }
//    }

//    /// <summary>
//    /// 创建树
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    public abstract class TreeBuilder<T> where T : class, new()
//    {
//        private readonly IEnumerable<T> _list;

//        public TreeBuilder(IEnumerable<T> list)
//        {
//            _list = list;
//        }
//        /// <summary>
//        /// 开始解析
//        /// </summary>
//        /// <returns></returns>
//        public Node<T> Builder()
//        {
//            var root = GetRoot(_list);

//            Node<T> rootNode = new Node<T>
//            {
//                Current = root,

//            };

//            return Builder(rootNode);
//            return null;
//        }


//        private Node<T> Builder(Node<T> parent)
//        {
//            if (parent == null)
//                return null;

//            if (parent.Current == default(T))
//                return null;

//            parent.Children = ToNodeList(GetChild(_list, parent.Current));


//            if (parent.Children == null)
//                return null;

//            foreach (var child in parent.Children)
//            {
//                this.Builder(child);
//            }

//            return parent;
//        }


//        /// <summary>
//        /// 取得子项目
//        /// </summary>
//        /// <param name="list"></param>
//        /// <param name="node"></param>
//        /// <returns></returns>
//        public abstract List<T> GetChild(IEnumerable<T> list, T node);
//        /// <summary>
//        /// 取得根项目
//        /// </summary>
//        /// <param name="list"></param>
//        /// <returns></returns>
//        public abstract T GetRoot(IEnumerable<T> list);

//        private static Node<T> ToNode(T t)
//        {
//            Node<T> node = new Node<T>();

//            node.Current = t;

//            return node;
//        }

//        static List<Node<T>> ToNodeList(List<T> list)
//        {
//            var listNode = new List<Node<T>>();

//            foreach (var nodeT in list)
//            {
//                listNode.Add(ToNode(nodeT));
//            }

//            return listNode;
//        }
//    }
//}
