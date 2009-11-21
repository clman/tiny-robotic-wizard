using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tiny_robotic_wizard
{
    class ProgramGenerator
    {
        ProgramData ProgramData { get; set; }
        public void Generate()
        {
            if (this.ProgramData == null)
            {
                throw new NullReferenceException("ProgramData is null.");
            }
            // Programを解析してProgramTreeのインスタンスを生成する．
            // root node is InputDevice 1 of NestDepth 3.
            SwitchNode ProgramTree = new SwitchNode(this.ProgramData.ProgramTemplate.Input.Device.Length);
            {
                int switchNestDepth = 1;
                // 
                foreach (Device device in this.ProgramData.ProgramTemplate.Input.Device)
                {
                    switchNestDepth *= (device.Option.Length + 1);
                }
                for (int i = 0; i < switchNestDepth; i++)
                {
                }
            }
        }
        // ProgramTreeを作るための再帰関数
        int makeProgramTree(SwitchNode node, int depth, int i)
        {
            if (i < depth)
            {
                foreach (Node childNode in node.ChildNodes)
                {

                }
            }
        }
    }
    class SwitchNode : Node
    {
        public readonly NodeType NodeType = NodeType.SwitchNode;
        public Node[] ChildNodes;
        public SwitchNode(int nodeCount):base(NodeType.SwitchNode)
        {
            if (nodeCount < 1)
            {
                throw new ArgumentOutOfRangeException("NodeCount must be 1 and over.");
            }
            else
            {
                ChildNodes = new Node[nodeCount];
            }
        }
    }
    class OutputNode : Node
    {
        public readonly NodeType NodeType = NodeType.OutputNode;
        public Output Output { get; set; }
        public OutputNode() : base(NodeType.OutputNode) { }
    }
    class Node
    {
        public readonly NodeType NodeType;
        public Node(NodeType nodeType)
        {
            this.NodeType = nodeType;
        }
    }
    enum NodeType
    {
        SwitchNode,
        OutputNode
    }
}
