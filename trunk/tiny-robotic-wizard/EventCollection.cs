using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace tiny_robotic_wizard
{
    class EventCollection : IDisposable
    {
        public EventCollection(Panel panel, ResolutionList resolution, LineSensorNumberList lineSensorNumber)
        {
            this.canvas = panel;
            this.lineSensorNumber = lineSensorNumber;
            this.resolution = resolution;
            this.eventAndFunction = new EventAndFunction[(int)resolution+1, (int)lineSensorNumber+1];
            this.distanceSensorStatusOutcomes = (int)resolution + 1;
            this.lineSensorStatusOutcomes = 1 << ((int)lineSensorNumber + 1);
            this.eventAndFunction = new EventAndFunction[distanceSensorStatusOutcomes, lineSensorStatusOutcomes];
            int temp = 0;
            for (int i = 0; i <= distanceSensorStatusOutcomes-1; i++)
            {
                for (int j = 0; j <= this.lineSensorStatusOutcomes - 1; j++)
                {
                    eventAndFunction[i, j] = new EventAndFunction();

                    eventAndFunction[i, j].distanceSensor = new DistanceSensor { Distance = i };
                    eventAndFunction[i, j].lineSensor = new LineSensor { StatusNumber = j };
                    eventAndFunction[i, j].led = new LED();
                    eventAndFunction[i, j].move = new Move();

                    if (resolution != ResolutionList.notUse)
                    {
                        eventAndFunction[i, j].distanceSensor.Location = new Point(180 * 0, temp * 100);
                        eventAndFunction[i, j].lineSensor.Location = new Point(180 * 1, temp * 100);
                        eventAndFunction[i, j].move.Location = new Point(180 * 2, temp * 100);
                        eventAndFunction[i, j].led.Location = new Point(180 * 3, temp * 100);
                    }
                    else
                    {
                        eventAndFunction[i, j].lineSensor.Location = new Point(180 * 0, temp * 100);
                        eventAndFunction[i, j].move.Location = new Point(180 * 1, temp * 100);
                        eventAndFunction[i, j].led.Location = new Point(180 * 2, temp * 100);
                    }
                    temp++;

                    if (resolution != ResolutionList.notUse)
                    {
                        this.canvas.Controls.Add(eventAndFunction[i, j].distanceSensor);
                    }
                    this.canvas.Controls.Add(eventAndFunction[i, j].lineSensor);
                    this.canvas.Controls.Add(eventAndFunction[i, j].led);
                    this.canvas.Controls.Add(eventAndFunction[i, j].move);
                }
            }
        }

        private readonly Panel canvas;
        private readonly LineSensorNumberList lineSensorNumber;
        private readonly ResolutionList resolution;
        private readonly int lineSensorStatusOutcomes;
        private readonly int distanceSensorStatusOutcomes;
        private readonly EventAndFunction[,] eventAndFunction;

        private class EventAndFunction
        {
            public DistanceSensor distanceSensor;
            public LineSensor lineSensor;
            public LED led;
            public Move move;
        }

        public void Draw()
        {
           
        }

        public void Dispose()
        {

        }
    }
}