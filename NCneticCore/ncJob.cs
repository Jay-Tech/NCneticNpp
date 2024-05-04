using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NCneticCore
{
    public class NcJob
    {
        public string FileName = String.Empty;
        

        public event EventHandler EndProcessing;
        public List<string> TextList; 
        public List<ncMove> MoveList = new List<ncMove>();

        public NcJob()
        {
            TextList = new List<string>();
        }
        public void Process(ncMachine machine)
        {
            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += new DoWorkEventHandler((sw, eaw) =>
            {
                MoveList = new List<ncMove>();
                ncParser parser = new ncParser();
                parser.ComputeRawJob(TextList, machine.Lexer);
                MoveList = FAO.GetMoveList(parser.Rawoperation, machine.Definition);
            });

            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler((s, ea) =>
            {
                EndProcessing?.Invoke(this, new EventArgs());
            });

            worker.RunWorkerAsync();
        }
    }
}
