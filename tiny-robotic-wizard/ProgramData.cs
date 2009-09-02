using System;
using System.Collections.Generic;
using System.Text;

namespace tiny_robotic_wizard
{
    class ProgramData
    {
        /// <summary>
        /// ProgramTemplate
        /// </summary>
        public readonly ProgramTemplate Template;
        private readonly int[] context;
        private readonly int[] action;

        /// <summary>
        /// ProgramTemplateを引数にProgramDataのインスタンスを生成
        /// </summary>
        /// <param name="template">ProgramTemplate(設定後は変更不可)</param>
        ProgramData(ProgramTemplate template)
        {
            Template = template;
        }
    }
}
