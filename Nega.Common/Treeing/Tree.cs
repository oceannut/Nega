using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public static class Tree<T>
    {

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
                if (current.Children != null && current.Children.Count > 0)
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
                if (current.Children != null && current.Children.Count > 0)
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
                    if (current.Children != null && current.Children.Count > 0)
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

    }

}
