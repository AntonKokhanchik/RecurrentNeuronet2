namespace RecurrentNeuronet2
{
	partial class Form1
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.buttonSelectFile = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.buttonLearn = new System.Windows.Forms.Button();
			this.textBoxEpsilon = new System.Windows.Forms.TextBox();
			this.textBoxInnerLength = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxString = new System.Windows.Forms.TextBox();
			this.buttonAnswer = new System.Windows.Forms.Button();
			this.textBoxAnswer = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxAlpha = new System.Windows.Forms.TextBox();
			this.buttonExitFile = new System.Windows.Forms.Button();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.textBoxTime = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
			this.buttonSaveDebug = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.textBoxInnerLength)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonSelectFile
			// 
			this.buttonSelectFile.Location = new System.Drawing.Point(227, 12);
			this.buttonSelectFile.Name = "buttonSelectFile";
			this.buttonSelectFile.Size = new System.Drawing.Size(90, 24);
			this.buttonSelectFile.TabIndex = 0;
			this.buttonSelectFile.Text = "Выбрать файл";
			this.buttonSelectFile.UseVisualStyleBackColor = true;
			this.buttonSelectFile.Click += new System.EventHandler(this.buttonSelectFile_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
			// 
			// buttonLearn
			// 
			this.buttonLearn.Enabled = false;
			this.buttonLearn.Location = new System.Drawing.Point(141, 141);
			this.buttonLearn.Name = "buttonLearn";
			this.buttonLearn.Size = new System.Drawing.Size(120, 22);
			this.buttonLearn.TabIndex = 1;
			this.buttonLearn.Text = "Начать обучение";
			this.buttonLearn.UseVisualStyleBackColor = true;
			this.buttonLearn.Click += new System.EventHandler(this.buttonLearn_Click);
			// 
			// textBoxEpsilon
			// 
			this.textBoxEpsilon.Enabled = false;
			this.textBoxEpsilon.Location = new System.Drawing.Point(227, 68);
			this.textBoxEpsilon.Name = "textBoxEpsilon";
			this.textBoxEpsilon.Size = new System.Drawing.Size(90, 20);
			this.textBoxEpsilon.TabIndex = 2;
			this.textBoxEpsilon.Text = "0,1";
			// 
			// textBoxInnerLength
			// 
			this.textBoxInnerLength.Enabled = false;
			this.textBoxInnerLength.Location = new System.Drawing.Point(258, 42);
			this.textBoxInnerLength.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.textBoxInnerLength.Name = "textBoxInnerLength";
			this.textBoxInnerLength.Size = new System.Drawing.Size(59, 20);
			this.textBoxInnerLength.TabIndex = 3;
			this.textBoxInnerLength.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(72, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(149, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Файл с обучаюей выборкой";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Enabled = false;
			this.label2.Location = new System.Drawing.Point(72, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(180, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Число нейронов на скрытом слое";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Enabled = false;
			this.label3.Location = new System.Drawing.Point(72, 71);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(57, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Точность ";
			// 
			// textBoxString
			// 
			this.textBoxString.Location = new System.Drawing.Point(12, 169);
			this.textBoxString.Multiline = true;
			this.textBoxString.Name = "textBoxString";
			this.textBoxString.Size = new System.Drawing.Size(387, 51);
			this.textBoxString.TabIndex = 2;
			// 
			// buttonAnswer
			// 
			this.buttonAnswer.Location = new System.Drawing.Point(12, 226);
			this.buttonAnswer.Name = "buttonAnswer";
			this.buttonAnswer.Size = new System.Drawing.Size(120, 22);
			this.buttonAnswer.TabIndex = 1;
			this.buttonAnswer.Text = "Получить ответ";
			this.buttonAnswer.UseVisualStyleBackColor = true;
			this.buttonAnswer.Click += new System.EventHandler(this.buttonAnswer_Click);
			// 
			// textBoxAnswer
			// 
			this.textBoxAnswer.Location = new System.Drawing.Point(309, 226);
			this.textBoxAnswer.Name = "textBoxAnswer";
			this.textBoxAnswer.ReadOnly = true;
			this.textBoxAnswer.Size = new System.Drawing.Size(90, 20);
			this.textBoxAnswer.TabIndex = 2;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 153);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(43, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Строка";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Enabled = false;
			this.label5.Location = new System.Drawing.Point(72, 97);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(112, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "Начальная скорость";
			// 
			// textBoxAlpha
			// 
			this.textBoxAlpha.Enabled = false;
			this.textBoxAlpha.Location = new System.Drawing.Point(227, 94);
			this.textBoxAlpha.Name = "textBoxAlpha";
			this.textBoxAlpha.Size = new System.Drawing.Size(90, 20);
			this.textBoxAlpha.TabIndex = 2;
			this.textBoxAlpha.Text = "30";
			// 
			// buttonExitFile
			// 
			this.buttonExitFile.Location = new System.Drawing.Point(12, 254);
			this.buttonExitFile.Name = "buttonExitFile";
			this.buttonExitFile.Size = new System.Drawing.Size(172, 24);
			this.buttonExitFile.TabIndex = 0;
			this.buttonExitFile.Text = "Записать результат в файл";
			this.buttonExitFile.UseVisualStyleBackColor = true;
			this.buttonExitFile.Click += new System.EventHandler(this.buttonExitFile_Click);
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
			// 
			// textBoxTime
			// 
			this.textBoxTime.Enabled = false;
			this.textBoxTime.Location = new System.Drawing.Point(227, 115);
			this.textBoxTime.Name = "textBoxTime";
			this.textBoxTime.Size = new System.Drawing.Size(90, 20);
			this.textBoxTime.TabIndex = 2;
			this.textBoxTime.Text = "5";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Enabled = false;
			this.label6.Location = new System.Drawing.Point(72, 118);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(89, 13);
			this.label6.TabIndex = 4;
			this.label6.Text = "Время обучения";
			// 
			// saveFileDialog2
			// 
			this.saveFileDialog2.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog2_FileOk);
			// 
			// buttonSaveDebug
			// 
			this.buttonSaveDebug.Location = new System.Drawing.Point(205, 255);
			this.buttonSaveDebug.Name = "buttonSaveDebug";
			this.buttonSaveDebug.Size = new System.Drawing.Size(194, 23);
			this.buttonSaveDebug.TabIndex = 5;
			this.buttonSaveDebug.Text = "Записать отладочную информацию";
			this.buttonSaveDebug.UseVisualStyleBackColor = true;
			this.buttonSaveDebug.Click += new System.EventHandler(this.buttonSaveDebug_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(411, 286);
			this.Controls.Add(this.buttonSaveDebug);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxInnerLength);
			this.Controls.Add(this.textBoxString);
			this.Controls.Add(this.textBoxAnswer);
			this.Controls.Add(this.textBoxTime);
			this.Controls.Add(this.textBoxAlpha);
			this.Controls.Add(this.textBoxEpsilon);
			this.Controls.Add(this.buttonAnswer);
			this.Controls.Add(this.buttonLearn);
			this.Controls.Add(this.buttonExitFile);
			this.Controls.Add(this.buttonSelectFile);
			this.Name = "Form1";
			this.Text = "Рекуррентная ИНС";
			((System.ComponentModel.ISupportInitialize)(this.textBoxInnerLength)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonSelectFile;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Button buttonLearn;
		private System.Windows.Forms.TextBox textBoxEpsilon;
		private System.Windows.Forms.NumericUpDown textBoxInnerLength;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxString;
		private System.Windows.Forms.Button buttonAnswer;
		private System.Windows.Forms.TextBox textBoxAnswer;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxAlpha;
		private System.Windows.Forms.Button buttonExitFile;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.TextBox textBoxTime;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.SaveFileDialog saveFileDialog2;
		private System.Windows.Forms.Button buttonSaveDebug;
	}
}

