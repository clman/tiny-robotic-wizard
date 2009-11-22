using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tiny_robotic_wizard
{
    public delegate void ContextAdded(ProgramData sender, Context addedData);
    public class Input : List<int?> { }
    public class Output : List<int?> { }
    public class Context : List<Input> { }
    public class ProgramData : Dictionary<Context, Output>
    {
        /// <summary>
        /// Contextが追加された時に発生するイベント
        /// </summary>
        public event ContextAdded ContextAdded;
        /// <summary>
        /// ProgramTemplate
        /// </summary>
        public readonly ProgramTemplate ProgramTemplate;
        /// <summary>
        /// イベントの連鎖数
        /// </summary>
        public readonly int NestDepth;
        public new Output this[Context context]{
            get { return base[context]; }
            set
            {
                base[context] = value;
                if (this.ContextAdded != null)
                {
                    this.ContextAdded(this, context);
                }
            }
        }
        public ProgramData(ProgramTemplate programTemplate, int nestLevel)
        {
            this.ProgramTemplate = programTemplate;
            if (1 <= nestLevel)
            {
                this.NestDepth = nestLevel;
            }
            else
            {
                throw new ArgumentOutOfRangeException("NestLevel must be 1 and over.");
            }
            // とりあえず，何もしない(すべてワイルドカードの)データを追加しておく．
            Context context = new Context();
            Input input = new Input();
            Output output = new Output();
            for (int i = 1; i <= this.ProgramTemplate.Input.Device.Length; i++)
            {
                input.Add(null);
            }
            for (int i = 1; i <= this.ProgramTemplate.Output.Device.Length; i++)
            {
                output.Add(null);
            }
            for (int i = 1; i <= this.NestDepth; i++)
            {
                context.Add(input);
            }
            this[context] = output;
        }
    }
}
