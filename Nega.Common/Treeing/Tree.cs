using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public static class Tree<T>
    {

        #region TreeNode

        public static TreeNode<T> Find(TreeNode<T> node,
            Func<TreeNode<T>, bool> action)
        {
            TreeNode<T> found = null;
            PreorderTraverse(node,
                        (e) =>
                        {
                            if (action(e))
                            {
                                found = e;
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        });
            return found;
        }

        public static TreeNode<T> Find(IEnumerable<TreeNode<T>> col, 
            Func<TreeNode<T>, bool> action)
        {
            TreeNode<T> found = null;
            PreorderTraverse(col,
                        (e) =>
                        {
                            if (action(e))
                            {
                                found = e;
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        });
            return found;
        }

        public static void PreorderTraverse(TreeNode<T> node,
            Action<TreeNode<T>> action)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }
            PreorderTraverse(new TreeNode<T>[] { node }, action);
        }

        /// <summary>
        /// 先序遍历。
        /// </summary>
        /// <param name="col"></param>
        /// <param name="action"></param>
        public static void PreorderTraverse(IEnumerable<TreeNode<T>> col,
            Action<TreeNode<T>> action)
        {
            if (col == null || action == null)
            {
                throw new ArgumentNullException();
            }
            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
            foreach (var item in col)
            {
                stack.Push(item);
            }
            while (stack.Count > 0)
            {
                TreeNode<T> current = stack.Pop();
                action(current);
                var children = current.Children;
                if (children != null && children.Count > 0)
                {
                    foreach (var item in current.Children)
                    {
                        stack.Push(item);
                    }
                }
            }
        }

        public static void PreorderTraverse(TreeNode<T> node,
            Func<TreeNode<T>, bool> action)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }
            PreorderTraverse(new TreeNode<T>[] { node }, action);
        }

        /// <summary>
        /// 先序遍历。
        /// </summary>
        /// <param name="col"></param>
        /// <param name="action"></param>
        public static void PreorderTraverse(IEnumerable<TreeNode<T>> col,
            Func<TreeNode<T>, bool> action)
        {
            if (col == null || action == null)
            {
                throw new ArgumentException();
            }
            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
            foreach (var item in col)
            {
                stack.Push(item);
            }
            while (stack.Count > 0)
            {
                TreeNode<T> current = stack.Pop();
                if (action(current))
                {
                    stack.Clear();
                    return;
                }
                var children = current.Children;
                if (children != null && children.Count > 0)
                {
                    foreach (var item in current.Children)
                    {
                        stack.Push(item);
                    }
                }
            }
        }

        public static void PostorderTraverse(TreeNode<T> node,
            Action<TreeNode<T>> action)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }
            PostorderTraverse(new TreeNode<T>[] { node }, action);
        }

        /// <summary>
        /// 后序遍历。
        /// </summary>
        /// <param name="col"></param>
        /// <param name="action"></param>
        public static void PostorderTraverse(IEnumerable<TreeNode<T>> col,
            Action<TreeNode<T>> action)
        {
            if (col == null || action == null)
            {
                throw new ArgumentException();
            }
            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
            Stack<bool> visitedStack = new Stack<bool>();
            foreach (var item in col)
            {
                stack.Push(item);
                visitedStack.Push(false);
            }
            while (stack.Count > 0)
            {
                TreeNode<T> current = stack.Pop();
                bool visited = visitedStack.Pop();
                if (visited)
                {
                    action(current);
                }
                else
                {
                    stack.Push(current);
                    visitedStack.Push(true);
                    var children = current.Children;
                    if (children != null && children.Count > 0)
                    {
                        foreach (var item in current.Children)
                        {
                            stack.Push(item);
                            visitedStack.Push(false);
                        }
                    }
                }
            }
        }

        #endregion

    }

    public static class Tree
    {

        #region ITreeNode

        public static ITreeNode Find(ITreeNode node,
            Func<ITreeNode, bool> action)
        {
            ITreeNode found = null;
            PreorderTraverse(node,
                        (e) =>
                        {
                            if (action(e))
                            {
                                found = e;
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        });
            return found;
        }

        public static ITreeNode Find(IEnumerable<ITreeNode> col,
            Func<ITreeNode, bool> action)
        {
            ITreeNode found = null;
            PreorderTraverse(col,
                        (e) =>
                        {
                            if (action(e))
                            {
                                found = e;
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        });
            return found;
        }

        public static void PreorderTraverse(ITreeNode node,
            Action<ITreeNode> action)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }
            PreorderTraverse(new ITreeNode[] { node }, action);
        }

        public static void PreorderTraverse(IEnumerable<ITreeNode> col,
            Action<ITreeNode> action)
        {
            if (col == null || action == null)
            {
                throw new ArgumentNullException();
            }
            Stack<ITreeNode> stack = new Stack<ITreeNode>();
            foreach (var item in col)
            {
                stack.Push(item);
            }
            while (stack.Count > 0)
            {
                ITreeNode current = stack.Pop();
                action(current);
                var childNodes = current.ChildNodes;
                if (childNodes != null && childNodes.Count() > 0)
                {
                    foreach (var item in childNodes)
                    {
                        stack.Push(item);
                    }
                }
            }
        }

        public static void PreorderTraverse(ITreeNode node,
            Func<ITreeNode, bool> action)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }
            PreorderTraverse(new ITreeNode[] { node }, action);
        }

        public static void PreorderTraverse(IEnumerable<ITreeNode> col,
            Func<ITreeNode, bool> action)
        {
            if (col == null || action == null)
            {
                throw new ArgumentException();
            }
            Stack<ITreeNode> stack = new Stack<ITreeNode>();
            foreach (var item in col)
            {
                stack.Push(item);
            }
            while (stack.Count > 0)
            {
                ITreeNode current = stack.Pop();
                if (action(current))
                {
                    stack.Clear();
                    return;
                }
                var childNodes = current.ChildNodes;
                if (childNodes != null && childNodes.Count() > 0)
                {
                    foreach (var item in childNodes)
                    {
                        stack.Push(item);
                    }
                }
            }
        }

        public static void PostorderTraverse(ITreeNode node,
            Action<ITreeNode> action)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }
            PostorderTraverse(new ITreeNode[] { node }, action);
        }

        public static void PostorderTraverse(IEnumerable<ITreeNode> col,
            Action<ITreeNode> action)
        {
            if (col == null || action == null)
            {
                throw new ArgumentException();
            }
            Stack<ITreeNode> stack = new Stack<ITreeNode>();
            Stack<bool> visitedStack = new Stack<bool>();
            foreach (var item in col)
            {
                stack.Push(item);
                visitedStack.Push(false);
            }
            while (stack.Count > 0)
            {
                ITreeNode current = stack.Pop();
                bool visited = visitedStack.Pop();
                if (visited)
                {
                    action(current);
                }
                else
                {
                    stack.Push(current);
                    visitedStack.Push(true);
                    var childNodes = current.ChildNodes;
                    if (childNodes != null && childNodes.Count() > 0)
                    {
                        foreach (var item in childNodes)
                        {
                            stack.Push(item);
                            visitedStack.Push(false);
                        }
                    }
                }
            }
        }

        #endregion

    }

}
