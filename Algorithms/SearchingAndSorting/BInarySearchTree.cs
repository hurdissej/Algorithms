using System;

public static class BinarySearchTree
{
    public static void RunAllTests(int sampleSize){
        // Create Values
        var size = sampleSize;
        var values = new int[size];
        var random = new Random();
        for(var i = 0; i< size; i++){
            values[i] = random.Next(1000000);
        }
        values[size/2] = 10;
        values[size -1] = 90;
        // Create Tree
        Node node = null;
        Node SortedTree = new Node();
        for(var i = 0; i < values.Length; i++)
        {
            node = InsertNode(node, values[i]);
        }

        // Run Test 
        var Does10Exist = FindValue(node, 10); 
        var Does90Exist = FindValue(node, 90); 
        var Does1000001Exist = FindValue(node, 1000001); 
        System.Console.WriteLine(Does1000001Exist);
        System.Console.WriteLine(Does90Exist);
        System.Console.WriteLine(Does10Exist);
    }

    private static Node InsertNode(Node root, int v)
        {
            if (root == null)
            {
                root = new Node();
                root.value = v;
            }
            else if (v < root.value)
            {
                root.left = InsertNode(root.left, v);
            }
            else
            {
                root.right = InsertNode(root.right, v);
            }

            return root;
        }

    private static bool FindValue(Node root, int value)
    {
        if(root.value == value)
            return true;
        if(value < root.value && root.left != null)
            return FindValue(root.left, value);
        if(root.right != null)
            return FindValue(root.right, value);      
        return false;
    }
}

public class Node
{
    public int? value;
    public Node left; 
    public Node right; 
}
