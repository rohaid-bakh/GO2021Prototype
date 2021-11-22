using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node<T> {
    
    public T val;
    public List<Node<T>> children;
    public Node() { this.val = default(T); this.children = default(List<Node<T>>); }
    public Node(T val) { this.val = val; this.children = default(List<Node<T>>); }
    public Node(T val, List<Node<T>> children) { this.val = val ; this.children = children; }
}
