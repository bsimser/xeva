namespace XF.Controls.Sandbox
{
   partial class CalculatorSample
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this._run = new System.Windows.Forms.Button();
         this.label1A = new System.Windows.Forms.Label();
         this._input1A = new System.Windows.Forms.NumericUpDown();
         this._input1B = new System.Windows.Forms.NumericUpDown();
         this.label1B = new System.Windows.Forms.Label();
         this._output1 = new System.Windows.Forms.NumericUpDown();
         this.label1 = new System.Windows.Forms.Label();
         this._output2 = new System.Windows.Forms.NumericUpDown();
         this.label2 = new System.Windows.Forms.Label();
         this._input2B = new System.Windows.Forms.NumericUpDown();
         this.label2B = new System.Windows.Forms.Label();
         this._input2A = new System.Windows.Forms.NumericUpDown();
         this.label2A = new System.Windows.Forms.Label();
         this._output4 = new System.Windows.Forms.NumericUpDown();
         this.label10 = new System.Windows.Forms.Label();
         this._input4B = new System.Windows.Forms.NumericUpDown();
         this.label4B = new System.Windows.Forms.Label();
         this.Step01 = new System.Windows.Forms.Label();
         this.Step02 = new System.Windows.Forms.Label();
         this.Step04 = new System.Windows.Forms.Label();
         this.label4 = new System.Windows.Forms.Label();
         this.label5 = new System.Windows.Forms.Label();
         this.label6 = new System.Windows.Forms.Label();
         this.label7 = new System.Windows.Forms.Label();
         this.Step03 = new System.Windows.Forms.Label();
         this._output3 = new System.Windows.Forms.NumericUpDown();
         this.label9 = new System.Windows.Forms.Label();
         this.label3 = new System.Windows.Forms.Label();
         this._output5 = new System.Windows.Forms.NumericUpDown();
         this.label8 = new System.Windows.Forms.Label();
         this.label11 = new System.Windows.Forms.Label();
         this.label12 = new System.Windows.Forms.Label();
         this.label13 = new System.Windows.Forms.Label();
         this._output6 = new System.Windows.Forms.NumericUpDown();
         this.label14 = new System.Windows.Forms.Label();
         ((System.ComponentModel.ISupportInitialize)(this._input1A)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this._input1B)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this._output1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this._output2)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this._input2B)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this._input2A)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this._output4)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this._input4B)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this._output3)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this._output5)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this._output6)).BeginInit();
         this.SuspendLayout();
         // 
         // _run
         // 
         this._run.Location = new System.Drawing.Point(616, 212);
         this._run.Name = "_run";
         this._run.Size = new System.Drawing.Size(75, 23);
         this._run.TabIndex = 0;
         this._run.Text = "Run";
         this._run.UseVisualStyleBackColor = true;
         this._run.Click += new System.EventHandler(this.OnRunClick);
         // 
         // label1A
         // 
         this.label1A.AutoSize = true;
         this.label1A.Location = new System.Drawing.Point(68, 12);
         this.label1A.Name = "label1A";
         this.label1A.Size = new System.Drawing.Size(36, 13);
         this.label1A.TabIndex = 1;
         this.label1A.Text = "Arg1A";
         this.label1A.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // _input1A
         // 
         this._input1A.Location = new System.Drawing.Point(113, 12);
         this._input1A.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
         this._input1A.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
         this._input1A.Name = "_input1A";
         this._input1A.Size = new System.Drawing.Size(120, 20);
         this._input1A.TabIndex = 2;
         this._input1A.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
         // 
         // _input1B
         // 
         this._input1B.Location = new System.Drawing.Point(292, 12);
         this._input1B.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
         this._input1B.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
         this._input1B.Name = "_input1B";
         this._input1B.Size = new System.Drawing.Size(120, 20);
         this._input1B.TabIndex = 4;
         this._input1B.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
         // 
         // label1B
         // 
         this.label1B.AutoSize = true;
         this.label1B.Location = new System.Drawing.Point(247, 12);
         this.label1B.Name = "label1B";
         this.label1B.Size = new System.Drawing.Size(36, 13);
         this.label1B.TabIndex = 3;
         this.label1B.Text = "Arg1B";
         this.label1B.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // _output1
         // 
         this._output1.Location = new System.Drawing.Point(490, 10);
         this._output1.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
         this._output1.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
         this._output1.Name = "_output1";
         this._output1.ReadOnly = true;
         this._output1.Size = new System.Drawing.Size(120, 20);
         this._output1.TabIndex = 6;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(432, 12);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(45, 13);
         this.label1.TabIndex = 5;
         this.label1.Text = "Output1";
         this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // _output2
         // 
         this._output2.Location = new System.Drawing.Point(490, 36);
         this._output2.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
         this._output2.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
         this._output2.Name = "_output2";
         this._output2.ReadOnly = true;
         this._output2.Size = new System.Drawing.Size(120, 20);
         this._output2.TabIndex = 12;
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(432, 38);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(45, 13);
         this.label2.TabIndex = 11;
         this.label2.Text = "Output2";
         this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // _input2B
         // 
         this._input2B.Location = new System.Drawing.Point(292, 38);
         this._input2B.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
         this._input2B.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
         this._input2B.Name = "_input2B";
         this._input2B.Size = new System.Drawing.Size(120, 20);
         this._input2B.TabIndex = 10;
         this._input2B.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
         // 
         // label2B
         // 
         this.label2B.AutoSize = true;
         this.label2B.Location = new System.Drawing.Point(247, 38);
         this.label2B.Name = "label2B";
         this.label2B.Size = new System.Drawing.Size(36, 13);
         this.label2B.TabIndex = 9;
         this.label2B.Text = "Arg2B";
         this.label2B.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // _input2A
         // 
         this._input2A.Location = new System.Drawing.Point(113, 38);
         this._input2A.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
         this._input2A.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
         this._input2A.Name = "_input2A";
         this._input2A.Size = new System.Drawing.Size(120, 20);
         this._input2A.TabIndex = 8;
         this._input2A.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
         // 
         // label2A
         // 
         this.label2A.AutoSize = true;
         this.label2A.Location = new System.Drawing.Point(68, 38);
         this.label2A.Name = "label2A";
         this.label2A.Size = new System.Drawing.Size(36, 13);
         this.label2A.TabIndex = 7;
         this.label2A.Text = "Arg2A";
         this.label2A.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // _output4
         // 
         this._output4.Location = new System.Drawing.Point(490, 88);
         this._output4.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
         this._output4.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
         this._output4.Name = "_output4";
         this._output4.ReadOnly = true;
         this._output4.Size = new System.Drawing.Size(120, 20);
         this._output4.TabIndex = 18;
         // 
         // label10
         // 
         this.label10.AutoSize = true;
         this.label10.Location = new System.Drawing.Point(432, 90);
         this.label10.Name = "label10";
         this.label10.Size = new System.Drawing.Size(45, 13);
         this.label10.TabIndex = 17;
         this.label10.Text = "Output4";
         this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // _input4B
         // 
         this._input4B.Location = new System.Drawing.Point(292, 90);
         this._input4B.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
         this._input4B.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
         this._input4B.Name = "_input4B";
         this._input4B.Size = new System.Drawing.Size(120, 20);
         this._input4B.TabIndex = 16;
         this._input4B.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
         // 
         // label4B
         // 
         this.label4B.AutoSize = true;
         this.label4B.Location = new System.Drawing.Point(247, 90);
         this.label4B.Name = "label4B";
         this.label4B.Size = new System.Drawing.Size(36, 13);
         this.label4B.TabIndex = 15;
         this.label4B.Text = "Arg4B";
         this.label4B.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // Step01
         // 
         this.Step01.AutoSize = true;
         this.Step01.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.Step01.Location = new System.Drawing.Point(7, 12);
         this.Step01.Name = "Step01";
         this.Step01.Size = new System.Drawing.Size(55, 13);
         this.Step01.TabIndex = 19;
         this.Step01.Text = "Step 01:";
         // 
         // Step02
         // 
         this.Step02.AutoSize = true;
         this.Step02.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.Step02.Location = new System.Drawing.Point(7, 38);
         this.Step02.Name = "Step02";
         this.Step02.Size = new System.Drawing.Size(55, 13);
         this.Step02.TabIndex = 20;
         this.Step02.Text = "Step 02:";
         // 
         // Step04
         // 
         this.Step04.AutoSize = true;
         this.Step04.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.Step04.Location = new System.Drawing.Point(6, 90);
         this.Step04.Name = "Step04";
         this.Step04.Size = new System.Drawing.Size(55, 13);
         this.Step04.TabIndex = 21;
         this.Step04.Text = "Step 04:";
         // 
         // label4
         // 
         this.label4.AutoSize = true;
         this.label4.Location = new System.Drawing.Point(616, 12);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(77, 13);
         this.label4.TabIndex = 22;
         this.label4.Text = "Arg1A + Arg1B";
         // 
         // label5
         // 
         this.label5.AutoSize = true;
         this.label5.Location = new System.Drawing.Point(616, 38);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(75, 13);
         this.label5.TabIndex = 23;
         this.label5.Text = "Arg2A * Arg2B";
         // 
         // label6
         // 
         this.label6.AutoSize = true;
         this.label6.Location = new System.Drawing.Point(615, 90);
         this.label6.Name = "label6";
         this.label6.Size = new System.Drawing.Size(68, 13);
         this.label6.TabIndex = 24;
         this.label6.Text = "% of Output3";
         // 
         // label7
         // 
         this.label7.AutoSize = true;
         this.label7.Location = new System.Drawing.Point(615, 64);
         this.label7.Name = "label7";
         this.label7.Size = new System.Drawing.Size(95, 13);
         this.label7.TabIndex = 30;
         this.label7.Text = "Output1 + Output2";
         // 
         // Step03
         // 
         this.Step03.AutoSize = true;
         this.Step03.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.Step03.Location = new System.Drawing.Point(6, 64);
         this.Step03.Name = "Step03";
         this.Step03.Size = new System.Drawing.Size(55, 13);
         this.Step03.TabIndex = 29;
         this.Step03.Text = "Step 03:";
         // 
         // _output3
         // 
         this._output3.Location = new System.Drawing.Point(490, 62);
         this._output3.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
         this._output3.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
         this._output3.Name = "_output3";
         this._output3.ReadOnly = true;
         this._output3.Size = new System.Drawing.Size(120, 20);
         this._output3.TabIndex = 28;
         // 
         // label9
         // 
         this.label9.AutoSize = true;
         this.label9.Location = new System.Drawing.Point(432, 64);
         this.label9.Name = "label9";
         this.label9.Size = new System.Drawing.Size(45, 13);
         this.label9.TabIndex = 27;
         this.label9.Text = "Output3";
         this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(615, 116);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(92, 13);
         this.label3.TabIndex = 33;
         this.label3.Text = "Sum(1..3) Outputs";
         // 
         // _output5
         // 
         this._output5.Location = new System.Drawing.Point(490, 114);
         this._output5.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
         this._output5.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
         this._output5.Name = "_output5";
         this._output5.ReadOnly = true;
         this._output5.Size = new System.Drawing.Size(120, 20);
         this._output5.TabIndex = 32;
         // 
         // label8
         // 
         this.label8.AutoSize = true;
         this.label8.Location = new System.Drawing.Point(432, 116);
         this.label8.Name = "label8";
         this.label8.Size = new System.Drawing.Size(45, 13);
         this.label8.TabIndex = 31;
         this.label8.Text = "Output5";
         this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // label11
         // 
         this.label11.AutoSize = true;
         this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.label11.Location = new System.Drawing.Point(6, 116);
         this.label11.Name = "label11";
         this.label11.Size = new System.Drawing.Size(55, 13);
         this.label11.TabIndex = 34;
         this.label11.Text = "Step 05:";
         // 
         // label12
         // 
         this.label12.AutoSize = true;
         this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.label12.Location = new System.Drawing.Point(6, 141);
         this.label12.Name = "label12";
         this.label12.Size = new System.Drawing.Size(55, 13);
         this.label12.TabIndex = 38;
         this.label12.Text = "Step 06:";
         // 
         // label13
         // 
         this.label13.AutoSize = true;
         this.label13.Location = new System.Drawing.Point(615, 141);
         this.label13.Name = "label13";
         this.label13.Size = new System.Drawing.Size(44, 13);
         this.label13.TabIndex = 37;
         this.label13.Text = "SumList";
         // 
         // _output6
         // 
         this._output6.Location = new System.Drawing.Point(490, 139);
         this._output6.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
         this._output6.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
         this._output6.Name = "_output6";
         this._output6.ReadOnly = true;
         this._output6.Size = new System.Drawing.Size(120, 20);
         this._output6.TabIndex = 36;
         // 
         // label14
         // 
         this.label14.AutoSize = true;
         this.label14.Location = new System.Drawing.Point(432, 141);
         this.label14.Name = "label14";
         this.label14.Size = new System.Drawing.Size(45, 13);
         this.label14.TabIndex = 35;
         this.label14.Text = "Output6";
         this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // CalculatorSample
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(717, 247);
         this.Controls.Add(this.label12);
         this.Controls.Add(this.label13);
         this.Controls.Add(this._output6);
         this.Controls.Add(this.label14);
         this.Controls.Add(this.label11);
         this.Controls.Add(this.label3);
         this.Controls.Add(this._output5);
         this.Controls.Add(this.label8);
         this.Controls.Add(this.label7);
         this.Controls.Add(this.Step03);
         this.Controls.Add(this._output3);
         this.Controls.Add(this.label9);
         this.Controls.Add(this.label6);
         this.Controls.Add(this.label5);
         this.Controls.Add(this.label4);
         this.Controls.Add(this.Step04);
         this.Controls.Add(this.Step02);
         this.Controls.Add(this.Step01);
         this.Controls.Add(this._output4);
         this.Controls.Add(this.label10);
         this.Controls.Add(this._input4B);
         this.Controls.Add(this.label4B);
         this.Controls.Add(this._output2);
         this.Controls.Add(this.label2);
         this.Controls.Add(this._input2B);
         this.Controls.Add(this.label2B);
         this.Controls.Add(this._input2A);
         this.Controls.Add(this.label2A);
         this.Controls.Add(this._output1);
         this.Controls.Add(this.label1);
         this.Controls.Add(this._input1B);
         this.Controls.Add(this.label1B);
         this.Controls.Add(this._input1A);
         this.Controls.Add(this.label1A);
         this.Controls.Add(this._run);
         this.Name = "CalculatorSample";
         this.Text = "CalculatorSample";
         ((System.ComponentModel.ISupportInitialize)(this._input1A)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this._input1B)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this._output1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this._output2)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this._input2B)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this._input2A)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this._output4)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this._input4B)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this._output3)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this._output5)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this._output6)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button _run;
      private System.Windows.Forms.Label label1A;
      private System.Windows.Forms.NumericUpDown _input1A;
      private System.Windows.Forms.NumericUpDown _input1B;
      private System.Windows.Forms.Label label1B;
      private System.Windows.Forms.NumericUpDown _output1;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.NumericUpDown _output2;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.NumericUpDown _input2B;
      private System.Windows.Forms.Label label2B;
      private System.Windows.Forms.NumericUpDown _input2A;
      private System.Windows.Forms.Label label2A;
      private System.Windows.Forms.NumericUpDown _output4;
      private System.Windows.Forms.Label label10;
      private System.Windows.Forms.NumericUpDown _input4B;
      private System.Windows.Forms.Label label4B;
      private System.Windows.Forms.Label Step01;
      private System.Windows.Forms.Label Step02;
      private System.Windows.Forms.Label Step04;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.Label label7;
      private System.Windows.Forms.Label Step03;
      private System.Windows.Forms.NumericUpDown _output3;
      private System.Windows.Forms.Label label9;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.NumericUpDown _output5;
      private System.Windows.Forms.Label label8;
      private System.Windows.Forms.Label label11;
      private System.Windows.Forms.Label label12;
      private System.Windows.Forms.Label label13;
      private System.Windows.Forms.NumericUpDown _output6;
      private System.Windows.Forms.Label label14;
   }
}