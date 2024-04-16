namespace WindowsForms
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_sensorcarin = new System.Windows.Forms.Button();
            this.btn_sensorcarout = new System.Windows.Forms.Button();
            this.tx_url = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tx_sensor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tx_Indicate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tx_carintime = new System.Windows.Forms.TextBox();
            this.tx_carouttime = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_sensorcarin
            // 
            this.btn_sensorcarin.Location = new System.Drawing.Point(90, 200);
            this.btn_sensorcarin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_sensorcarin.Name = "btn_sensorcarin";
            this.btn_sensorcarin.Size = new System.Drawing.Size(75, 30);
            this.btn_sensorcarin.TabIndex = 0;
            this.btn_sensorcarin.Text = "地磁进场";
            this.btn_sensorcarin.UseVisualStyleBackColor = true;
            this.btn_sensorcarin.Click += new System.EventHandler(this.btn_sensorcarin_Click);
            // 
            // btn_sensorcarout
            // 
            this.btn_sensorcarout.Location = new System.Drawing.Point(256, 200);
            this.btn_sensorcarout.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_sensorcarout.Name = "btn_sensorcarout";
            this.btn_sensorcarout.Size = new System.Drawing.Size(75, 30);
            this.btn_sensorcarout.TabIndex = 1;
            this.btn_sensorcarout.Text = "地磁出场";
            this.btn_sensorcarout.UseVisualStyleBackColor = true;
            this.btn_sensorcarout.Click += new System.EventHandler(this.btn_sensorcarout_Click);
            // 
            // tx_url
            // 
            this.tx_url.Location = new System.Drawing.Point(126, 27);
            this.tx_url.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tx_url.Name = "tx_url";
            this.tx_url.Size = new System.Drawing.Size(261, 21);
            this.tx_url.TabIndex = 2;
            this.tx_url.Text = "http://localhost:778";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "服务器地址：";
            // 
            // tx_sensor
            // 
            this.tx_sensor.Location = new System.Drawing.Point(126, 60);
            this.tx_sensor.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tx_sensor.Name = "tx_sensor";
            this.tx_sensor.Size = new System.Drawing.Size(128, 21);
            this.tx_sensor.TabIndex = 4;
            this.tx_sensor.Text = "88888888";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(68, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "地磁编号：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(79, 99);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "流水号：";
            // 
            // tx_Indicate
            // 
            this.tx_Indicate.Location = new System.Drawing.Point(126, 95);
            this.tx_Indicate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tx_Indicate.Name = "tx_Indicate";
            this.tx_Indicate.Size = new System.Drawing.Size(76, 21);
            this.tx_Indicate.TabIndex = 7;
            this.tx_Indicate.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(68, 135);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "进场时间：";
            // 
            // tx_carintime
            // 
            this.tx_carintime.Location = new System.Drawing.Point(128, 132);
            this.tx_carintime.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tx_carintime.Name = "tx_carintime";
            this.tx_carintime.Size = new System.Drawing.Size(176, 21);
            this.tx_carintime.TabIndex = 9;
            this.tx_carintime.Text = "2019020";
            // 
            // tx_carouttime
            // 
            this.tx_carouttime.Location = new System.Drawing.Point(130, 167);
            this.tx_carouttime.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tx_carouttime.Name = "tx_carouttime";
            this.tx_carouttime.Size = new System.Drawing.Size(176, 21);
            this.tx_carouttime.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(70, 171);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "出场时间：";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 247);
            this.Controls.Add(this.tx_carouttime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tx_carintime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tx_Indicate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tx_sensor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tx_url);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_sensorcarout);
            this.Controls.Add(this.btn_sensorcarin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "地磁测试";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_sensorcarin;
        private System.Windows.Forms.Button btn_sensorcarout;
        private System.Windows.Forms.TextBox tx_url;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tx_sensor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tx_Indicate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tx_carintime;
        private System.Windows.Forms.TextBox tx_carouttime;
        private System.Windows.Forms.Label label5;
    }
}

