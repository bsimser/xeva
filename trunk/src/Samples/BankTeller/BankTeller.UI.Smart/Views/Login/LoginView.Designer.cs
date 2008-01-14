namespace BankTeller.UI.Smart.Views
{
   partial class LoginView
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

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this._usernameTextbox = new System.Windows.Forms.TextBox();
         this._passwordTextbox = new System.Windows.Forms.TextBox();
         this._usernameLabel = new System.Windows.Forms.Label();
         this._passwordLabel = new System.Windows.Forms.Label();
         this._loginButton = new System.Windows.Forms.Button();
         this._cancelButton = new System.Windows.Forms.Button();
         this._errorMessageLabel = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // _usernameTextbox
         // 
         this._usernameTextbox.Location = new System.Drawing.Point(87, 13);
         this._usernameTextbox.Name = "_usernameTextbox";
         this._usernameTextbox.Size = new System.Drawing.Size(142, 20);
         this._usernameTextbox.TabIndex = 0;
         // 
         // _passwordTextbox
         // 
         this._passwordTextbox.Location = new System.Drawing.Point(87, 39);
         this._passwordTextbox.Name = "_passwordTextbox";
         this._passwordTextbox.Size = new System.Drawing.Size(142, 20);
         this._passwordTextbox.TabIndex = 1;
         // 
         // _usernameLabel
         // 
         this._usernameLabel.AutoSize = true;
         this._usernameLabel.Location = new System.Drawing.Point(15, 16);
         this._usernameLabel.Name = "_usernameLabel";
         this._usernameLabel.Size = new System.Drawing.Size(55, 13);
         this._usernameLabel.TabIndex = 1;
         this._usernameLabel.Text = "Username";
         // 
         // _passwordLabel
         // 
         this._passwordLabel.AutoSize = true;
         this._passwordLabel.Location = new System.Drawing.Point(15, 42);
         this._passwordLabel.Name = "_passwordLabel";
         this._passwordLabel.Size = new System.Drawing.Size(53, 13);
         this._passwordLabel.TabIndex = 1;
         this._passwordLabel.Text = "Password";
         // 
         // _loginButton
         // 
         this._loginButton.Location = new System.Drawing.Point(87, 102);
         this._loginButton.Name = "_loginButton";
         this._loginButton.Size = new System.Drawing.Size(68, 23);
         this._loginButton.TabIndex = 2;
         this._loginButton.Text = "Login";
         this._loginButton.UseVisualStyleBackColor = true;
         // 
         // _cancelButton
         // 
         this._cancelButton.Location = new System.Drawing.Point(161, 102);
         this._cancelButton.Name = "_cancelButton";
         this._cancelButton.Size = new System.Drawing.Size(68, 23);
         this._cancelButton.TabIndex = 3;
         this._cancelButton.Text = "Cancel";
         this._cancelButton.UseVisualStyleBackColor = true;
         // 
         // _errorMessageLabel
         // 
         this._errorMessageLabel.AutoSize = true;
         this._errorMessageLabel.Location = new System.Drawing.Point(84, 74);
         this._errorMessageLabel.Name = "_errorMessageLabel";
         this._errorMessageLabel.Size = new System.Drawing.Size(29, 13);
         this._errorMessageLabel.TabIndex = 1;
         this._errorMessageLabel.Text = "Error";
         this._errorMessageLabel.Visible = false;
         // 
         // LoginView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this._cancelButton);
         this.Controls.Add(this._loginButton);
         this.Controls.Add(this._errorMessageLabel);
         this.Controls.Add(this._passwordLabel);
         this.Controls.Add(this._usernameLabel);
         this.Controls.Add(this._passwordTextbox);
         this.Controls.Add(this._usernameTextbox);
         this.Name = "LoginView";
         this.Size = new System.Drawing.Size(255, 137);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox _usernameTextbox;
      private System.Windows.Forms.TextBox _passwordTextbox;
      private System.Windows.Forms.Label _usernameLabel;
      private System.Windows.Forms.Label _passwordLabel;
      private System.Windows.Forms.Button _loginButton;
      private System.Windows.Forms.Button _cancelButton;
      private System.Windows.Forms.Label _errorMessageLabel;
   }
}
