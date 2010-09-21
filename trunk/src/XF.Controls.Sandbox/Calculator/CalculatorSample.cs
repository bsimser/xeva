using System.Collections.Generic;
using System.Windows.Forms;
using Model;

namespace XF.Controls.Sandbox
{
   public partial class CalculatorSample : Form
   {
      public CalculatorSample()
      {
         InitializeComponent();
      }

      private void OnRunClick(object sender, System.EventArgs e)
      {

         // CLEAR OUTPUT SO FORM IN RE-ENTRANT
         _output1.Value = 0m;
         _output2.Value = 0m;
         _output3.Value = 0m;
         _output4.Value = 0m;
         _output5.Value = 0m;
         _output6.Value = 0m;

         var variables = new XFCalculatorVariable().AddVariable("Input1A", _input1A.Value)
                                                   .AddVariable("Input1B", _input1B.Value)
                                                   .AddVariable("Input2A", _input2A.Value)
                                                   .AddVariable("Input2B", _input2B.Value)
                                                   .AddVariable("Input4B", _input4B.Value);
         var calulator = XFCalculatorFactory.BuildCalculator(@"Calculator\Sample1.xml", variables);
         var outputs = calulator.Run();

         if (outputs.ContainsKey("Output1")) _output1.Value = (decimal)outputs["Output1"];
         if (outputs.ContainsKey("Output2")) _output2.Value = (decimal)outputs["Output2"];
         if (outputs.ContainsKey("Output3")) _output3.Value = (decimal)outputs["Output3"];
         if (outputs.ContainsKey("Output4")) _output4.Value = (decimal)outputs["Output4"] ;
         if (outputs.ContainsKey("Output5")) _output5.Value = (decimal)outputs["Output5"];

      }

   }
}
