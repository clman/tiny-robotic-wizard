using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tiny_robotic_wizard
{
    public class Input : List<int?> { }
    public class Output : List<int?> { }
    public class Context : List<Input> { }
    public class ProgramData : Dictionary<Context, Output>
    {
        /// <summary>
        /// ProgramTemplate
        /// </summary>
        public ProgramTemplate ProgramTemplate { get; private set; }
        /// <summary>
        /// イベントの連鎖数
        /// </summary>
        private int nestLevel = 3;
        public int NestLevel { get; set; }
        public ProgramData(ProgramTemplate programTemplate)
        {
            this.ProgramTemplate = programTemplate;
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
            for (int i = 1; i <= this.nestLevel; i++)
            {
                context.Add(input);
            }
            this.Add(context, output);
        }
    }
}
