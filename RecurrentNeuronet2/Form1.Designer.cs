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
			this.openFileDialogInput = new System.Windows.Forms.OpenFileDialog();
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
			this.saveFileDialogResults = new System.Windows.Forms.SaveFileDialog();
			this.textBoxTime = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.saveFileDialogLog = new System.Windows.Forms.SaveFileDialog();
			this.buttonSaveLog = new System.Windows.Forms.Button();
			this.buttonLoadNet = new System.Windows.Forms.Button();
			this.buttonSaveNet = new System.Windows.Forms.Button();
			this.saveFileDialogNet = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialogLoadNet = new System.Windows.Forms.OpenFileDialog();
			this.buttonContinueLearn = new System.Windows.Forms.Button();
			this.buttonStop = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.textBoxInnerLength)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonSelectFile
			// 
			this.buttonSelectFile.Location = new System.Drawing.Point(164, 12);
			this.buttonSelectFile.Name = "buttonSelectFile";
			this.buttonSelectFile.Size = new System.Drawing.Size(90, 24);
			this.buttonSelectFile.TabIndex = 0;
			this.buttonSelectFile.Text = "Выбрать файл";
			this.buttonSelectFile.UseVisualStyleBackColor = true;
			this.buttonSelectFile.Click += new System.EventHandler(this.buttonSelectFile_Click);
			// 
			// openFileDialogInput
			// 
			this.openFileDialogInput.FileName = "openFileDialog1";
			this.openFileDialogInput.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogInput_FileOk);
			// 
			// buttonLearn
			// 
			this.buttonLearn.Enabled = false;
			this.buttonLearn.Location = new System.Drawing.Point(78, 141);
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
			this.textBoxEpsilon.Location = new System.Drawing.Point(164, 68);
			this.textBoxEpsilon.Name = "textBoxEpsilon";
			this.textBoxEpsilon.Size = new System.Drawing.Size(90, 20);
			this.textBoxEpsilon.TabIndex = 2;
			this.textBoxEpsilon.Text = "0,1";
			// 
			// textBoxInnerLength
			// 
			this.textBoxInnerLength.Enabled = false;
			this.textBoxInnerLength.Location = new System.Drawing.Point(195, 42);
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
			this.label1.Location = new System.Drawing.Point(9, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(149, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Файл с обучаюей выборкой";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Enabled = false;
			this.label2.Location = new System.Drawing.Point(9, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(180, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Число нейронов на скрытом слое";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Enabled = false;
			this.label3.Location = new System.Drawing.Point(9, 71);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(57, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Точность ";
			// 
			// textBoxString
			// 
			this.textBoxString.Enabled = false;
			this.textBoxString.Location = new System.Drawing.Point(12, 169);
			this.textBoxString.Multiline = true;
			this.textBoxString.Name = "textBoxString";
			this.textBoxString.Size = new System.Drawing.Size(387, 51);
			this.textBoxString.TabIndex = 2;
			// 
			// buttonAnswer
			// 
			this.buttonAnswer.Enabled = false;
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
			this.textBoxAnswer.Enabled = false;
			this.textBoxAnswer.Location = new System.Drawing.Point(138, 226);
			this.textBoxAnswer.Name = "textBoxAnswer";
			this.textBoxAnswer.ReadOnly = true;
			this.textBoxAnswer.Size = new System.Drawing.Size(261, 20);
			this.textBoxAnswer.TabIndex = 2;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Enabled = false;
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
			this.label5.Location = new System.Drawing.Point(9, 97);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(112, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "Начальная скорость";
			// 
			// textBoxAlpha
			// 
			this.textBoxAlpha.Enabled = false;
			this.textBoxAlpha.Location = new System.Drawing.Point(164, 94);
			this.textBoxAlpha.Name = "textBoxAlpha";
			this.textBoxAlpha.Size = new System.Drawing.Size(90, 20);
			this.textBoxAlpha.TabIndex = 2;
			this.textBoxAlpha.Text = "30";
			// 
			// buttonExitFile
			// 
			this.buttonExitFile.Enabled = false;
			this.buttonExitFile.Location = new System.Drawing.Point(12, 254);
			this.buttonExitFile.Name = "buttonExitFile";
			this.buttonExitFile.Size = new System.Drawing.Size(172, 24);
			this.buttonExitFile.TabIndex = 0;
			this.buttonExitFile.Text = "Записать результат в файл";
			this.buttonExitFile.UseVisualStyleBackColor = true;
			this.buttonExitFile.Click += new System.EventHandler(this.buttonExitFile_Click);
			// 
			// saveFileDialogResults
			// 
			this.saveFileDialogResults.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
			// 
			// textBoxTime
			// 
			this.textBoxTime.Enabled = false;
			this.textBoxTime.Location = new System.Drawing.Point(164, 115);
			this.textBoxTime.Name = "textBoxTime";
			this.textBoxTime.Size = new System.Drawing.Size(90, 20);
			this.textBoxTime.TabIndex = 2;
			this.textBoxTime.Text = "5";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Enabled = false;
			this.label6.Location = new System.Drawing.Point(9, 118);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(89, 13);
			this.label6.TabIndex = 4;
			this.label6.Text = "Время обучения";
			// 
			// saveFileDialogLog
			// 
			this.saveFileDialogLog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog2_FileOk);
			// 
			// buttonSaveLog
			// 
			this.buttonSaveLog.Enabled = false;
			this.buttonSaveLog.Location = new System.Drawing.Point(205, 255);
			this.buttonSaveLog.Name = "buttonSaveLog";
			this.buttonSaveLog.Size = new System.Drawing.Size(194, 23);
			this.buttonSaveLog.TabIndex = 5;
			this.buttonSaveLog.Text = "Записать отладочную информацию";
			this.buttonSaveLog.UseVisualStyleBackColor = true;
			this.buttonSaveLog.Click += new System.EventHandler(this.buttonSaveDebug_Click);
			// 
			// buttonLoadNet
			// 
			this.buttonLoadNet.Enabled = false;
			this.buttonLoadNet.Location = new System.Drawing.Point(274, 12);
			this.buttonLoadNet.Name = "buttonLoadNet";
			this.buttonLoadNet.Size = new System.Drawing.Size(125, 24);
			this.buttonLoadNet.TabIndex = 0;
			this.buttonLoadNet.Text = "Загрузить веса";
			this.buttonLoadNet.UseVisualStyleBackColor = true;
			this.buttonLoadNet.Click += new System.EventHandler(this.buttonLoadNet_Click);
			// 
			// buttonSaveNet
			// 
			this.buttonSaveNet.Enabled = false;
			this.buttonSaveNet.Location = new System.Drawing.Point(274, 60);
			this.buttonSaveNet.Name = "buttonSaveNet";
			this.buttonSaveNet.Size = new System.Drawing.Size(125, 24);
			this.buttonSaveNet.TabIndex = 0;
			this.buttonSaveNet.Text = "Сохранить веса";
			this.buttonSaveNet.UseVisualStyleBackColor = true;
			this.buttonSaveNet.Click += new System.EventHandler(this.buttonSaveNet_Click);
			// 
			// saveFileDialogNet
			// 
			this.saveFileDialogNet.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialogNet_FileOk);
			// 
			// openFileDialogLoadNet
			// 
			this.openFileDialogLoadNet.FileName = "openFileDialog1";
			this.openFileDialogLoadNet.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogLoadNet_FileOk);
			// 
			// buttonContinueLearn
			// 
			this.buttonContinueLearn.Enabled = false;
			this.buttonContinueLearn.Location = new System.Drawing.Point(227, 141);
			this.buttonContinueLearn.Name = "buttonContinueLearn";
			this.buttonContinueLearn.Size = new System.Drawing.Size(139, 22);
			this.buttonContinueLearn.TabIndex = 1;
			this.buttonContinueLearn.Text = "Продолжить обучение";
			this.buttonContinueLearn.UseVisualStyleBackColor = true;
			this.buttonContinueLearn.Click += new System.EventHandler(this.buttonContinueLearn_Click);
			// 
			// buttonStop
			// 
			this.buttonStop.Enabled = false;
			this.buttonStop.Location = new System.Drawing.Point(274, 113);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(120, 22);
			this.buttonStop.TabIndex = 1;
			this.buttonStop.Text = "Стоп";
			this.buttonStop.UseVisualStyleBackColor = true;
			this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(411, 285);
			this.Controls.Add(this.buttonSaveLog);
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
			this.Controls.Add(this.buttonContinueLearn);
			this.Controls.Add(this.buttonStop);
			this.Controls.Add(this.buttonLearn);
			this.Controls.Add(this.buttonSaveNet);
			this.Controls.Add(this.buttonLoadNet);
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
		private System.Windows.Forms.OpenFileDialog openFileDialogInput;
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
		private System.Windows.Forms.SaveFileDialog saveFileDialogResults;
		private System.Windows.Forms.TextBox textBoxTime;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.SaveFileDialog saveFileDialogLog;
		private System.Windows.Forms.Button buttonSaveLog;
		private System.Windows.Forms.Button buttonLoadNet;
		private System.Windows.Forms.Button buttonSaveNet;
		private System.Windows.Forms.SaveFileDialog saveFileDialogNet;
		private System.Windows.Forms.OpenFileDialog openFileDialogLoadNet;
		private System.Windows.Forms.Button buttonContinueLearn;
		private System.Windows.Forms.Button buttonStop;
	}
}

