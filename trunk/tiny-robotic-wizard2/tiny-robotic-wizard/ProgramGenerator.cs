using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tiny_robotic_wizard
{
    class ProgramGenerator
    {
        public static void Generate(ProgramData programData)
        {
            if (programData == null)
            {
                throw new ArgumentNullException("ProgramData is null.");
            }
            // Programを解析してツリー構造に変換．
            // ルートノードはNestDepth2のInput.Device[0]
            Node ProgramTree = new SwitchNode(1);
            ProgramTree = ((SwitchNode)ProgramTree).ChildNodes[0];
            {
                // すべてのコンテキストを探る
                foreach (Context context in programData.Keys)
                {
                    // CurrentNodeをリセット
                    Node current = ProgramTree;
                    for (int nestDepth = programData.NestDepth - 1; 0 <= nestDepth; nestDepth--)
                    {
                        for (int deviceIndex = 0; deviceIndex < programData.ProgramTemplate.Input.Device.Length; deviceIndex++)
                        {
                            // Inputの値がnullならInputの最大値+1にする．(ワイルドーカードnullをcase defaultに対応づける．)
                            int caseExpression = (context[nestDepth][deviceIndex] != null) ? (int)context[nestDepth][deviceIndex] : programData.ProgramTemplate.Input.Device[deviceIndex].Option.Length;
                            // CurrentNodeがnullならSwitchNodeを入れる．
                            if (current == null)
                            {
                                current = new SwitchNode(programData.ProgramTemplate.Input.Device[deviceIndex].Option.Length +1);
                            }
                            // CurrentNodeを移動
                            current = ((SwitchNode)current).ChildNodes[caseExpression];
                        }
                    }
                    // ツリーの末端にOutputNodeを追加
                    current = new OutputNode() { Output = programData[context] };
                }
            }
        }
    }
    class SwitchNode : Node
    {
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
