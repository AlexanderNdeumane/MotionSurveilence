﻿namespace MotionSurveilence
{
    partial class MotionForm
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonCompose = new System.Windows.Forms.Button();
            this.cameraUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelState = new System.Windows.Forms.Label();
            this.videoViewerWF1 = new Ozeki.Media.VideoViewerWF();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rad_rec_AndDet_motion = new System.Windows.Forms.RadioButton();
            this.rad_rec_normal = new System.Windows.Forms.RadioButton();
            this.rad_rec_OnMotion = new System.Windows.Forms.RadioButton();
            this.rad_motion = new System.Windows.Forms.RadioButton();
            this.rad_normal = new System.Windows.Forms.RadioButton();
            this.label_Motion = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonDisconnect);
            this.groupBox1.Controls.Add(this.buttonConnect);
            this.groupBox1.Controls.Add(this.buttonCompose);
            this.groupBox1.Controls.Add(this.cameraUrl);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(31, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 117);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection";
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Location = new System.Drawing.Point(130, 66);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(75, 23);
            this.buttonDisconnect.TabIndex = 4;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(9, 66);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.TabIndex = 3;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonCompose
            // 
            this.buttonCompose.Location = new System.Drawing.Point(227, 38);
            this.buttonCompose.Name = "buttonCompose";
            this.buttonCompose.Size = new System.Drawing.Size(75, 23);
            this.buttonCompose.TabIndex = 2;
            this.buttonCompose.Text = "Compose";
            this.buttonCompose.UseVisualStyleBackColor = true;
            this.buttonCompose.Click += new System.EventHandler(this.buttonCompose_Click);
            // 
            // cameraUrl
            // 
            this.cameraUrl.Location = new System.Drawing.Point(9, 40);
            this.cameraUrl.Name = "cameraUrl";
            this.cameraUrl.Size = new System.Drawing.Size(196, 20);
            this.cameraUrl.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Camre URL:";
            // 
            // labelState
            // 
            this.labelState.AutoSize = true;
            this.labelState.Location = new System.Drawing.Point(28, 148);
            this.labelState.Name = "labelState";
            this.labelState.Size = new System.Drawing.Size(35, 13);
            this.labelState.TabIndex = 5;
            this.labelState.Text = "State:";
            // 
            // videoViewerWF1
            // 
            this.videoViewerWF1.BackColor = System.Drawing.Color.Black;
            this.videoViewerWF1.ContextMenuEnabled = true;
            this.videoViewerWF1.FlipMode = Ozeki.Media.FlipMode.None;
            this.videoViewerWF1.FrameStretch = Ozeki.Media.FrameStretch.Uniform;
            this.videoViewerWF1.FullScreenEnabled = true;
            this.videoViewerWF1.Location = new System.Drawing.Point(31, 165);
            this.videoViewerWF1.Name = "videoViewerWF1";
            this.videoViewerWF1.RotateAngle = 0;
            this.videoViewerWF1.Size = new System.Drawing.Size(320, 240);
            this.videoViewerWF1.TabIndex = 3;
            this.videoViewerWF1.Text = "videoViewerWF1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rad_rec_AndDet_motion);
            this.groupBox2.Controls.Add(this.rad_rec_normal);
            this.groupBox2.Controls.Add(this.rad_rec_OnMotion);
            this.groupBox2.Controls.Add(this.rad_motion);
            this.groupBox2.Controls.Add(this.rad_normal);
            this.groupBox2.Location = new System.Drawing.Point(418, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(352, 149);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Controllers";
            // 
            // rad_rec_AndDet_motion
            // 
            this.rad_rec_AndDet_motion.AutoSize = true;
            this.rad_rec_AndDet_motion.Location = new System.Drawing.Point(6, 87);
            this.rad_rec_AndDet_motion.Name = "rad_rec_AndDet_motion";
            this.rad_rec_AndDet_motion.Size = new System.Drawing.Size(151, 17);
            this.rad_rec_AndDet_motion.TabIndex = 4;
            this.rad_rec_AndDet_motion.TabStop = true;
            this.rad_rec_AndDet_motion.Text = "Record and Detect Motion";
            this.rad_rec_AndDet_motion.UseVisualStyleBackColor = true;
            this.rad_rec_AndDet_motion.CheckedChanged += new System.EventHandler(this.rad_rec_AndDet_motion_CheckedChanged);
            // 
            // rad_rec_normal
            // 
            this.rad_rec_normal.AutoSize = true;
            this.rad_rec_normal.Location = new System.Drawing.Point(6, 43);
            this.rad_rec_normal.Name = "rad_rec_normal";
            this.rad_rec_normal.Size = new System.Drawing.Size(60, 17);
            this.rad_rec_normal.TabIndex = 3;
            this.rad_rec_normal.TabStop = true;
            this.rad_rec_normal.Text = "Record";
            this.rad_rec_normal.UseVisualStyleBackColor = true;
            this.rad_rec_normal.CheckedChanged += new System.EventHandler(this.rad_rec_normal_CheckedChanged);
            // 
            // rad_rec_OnMotion
            // 
            this.rad_rec_OnMotion.AutoSize = true;
            this.rad_rec_OnMotion.Location = new System.Drawing.Point(6, 110);
            this.rad_rec_OnMotion.Name = "rad_rec_OnMotion";
            this.rad_rec_OnMotion.Size = new System.Drawing.Size(159, 17);
            this.rad_rec_OnMotion.TabIndex = 2;
            this.rad_rec_OnMotion.TabStop = true;
            this.rad_rec_OnMotion.Text = "Record on Motion Detection";
            this.rad_rec_OnMotion.UseVisualStyleBackColor = true;
            this.rad_rec_OnMotion.CheckedChanged += new System.EventHandler(this.rad_rec_OnMotion_CheckedChanged);
            // 
            // rad_motion
            // 
            this.rad_motion.AutoSize = true;
            this.rad_motion.Location = new System.Drawing.Point(6, 64);
            this.rad_motion.Name = "rad_motion";
            this.rad_motion.Size = new System.Drawing.Size(92, 17);
            this.rad_motion.TabIndex = 1;
            this.rad_motion.TabStop = true;
            this.rad_motion.Text = "Detect Motion";
            this.rad_motion.UseVisualStyleBackColor = true;
            this.rad_motion.CheckedChanged += new System.EventHandler(this.rad_motion_CheckedChanged);
            // 
            // rad_normal
            // 
            this.rad_normal.AutoSize = true;
            this.rad_normal.Checked = true;
            this.rad_normal.Location = new System.Drawing.Point(7, 19);
            this.rad_normal.Name = "rad_normal";
            this.rad_normal.Size = new System.Drawing.Size(62, 17);
            this.rad_normal.TabIndex = 0;
            this.rad_normal.TabStop = true;
            this.rad_normal.Text = "Nothing";
            this.rad_normal.UseMnemonic = false;
            this.rad_normal.UseVisualStyleBackColor = true;
            this.rad_normal.CheckedChanged += new System.EventHandler(this.rad_normal_CheckedChanged);
            // 
            // label_Motion
            // 
            this.label_Motion.Location = new System.Drawing.Point(415, 165);
            this.label_Motion.Name = "label_Motion";
            this.label_Motion.Size = new System.Drawing.Size(122, 24);
            this.label_Motion.TabIndex = 6;
            // 
            // MotionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label_Motion);
            this.Controls.Add(this.labelState);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.videoViewerWF1);
            this.Controls.Add(this.groupBox1);
            this.Name = "MotionForm";
            this.Text = "MotionForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelState;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonCompose;
        private System.Windows.Forms.TextBox cameraUrl;
        private System.Windows.Forms.Label label1;
        private Ozeki.Media.VideoViewerWF videoViewerWF1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label_Motion;
        private System.Windows.Forms.RadioButton rad_rec_OnMotion;
        private System.Windows.Forms.RadioButton rad_motion;
        private System.Windows.Forms.RadioButton rad_normal;
        private System.Windows.Forms.RadioButton rad_rec_normal;
        private System.Windows.Forms.RadioButton rad_rec_AndDet_motion;
    }
}