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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MotionForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonCompose = new System.Windows.Forms.Button();
            this.cameraUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelState = new System.Windows.Forms.Label();
            this.videoViewerWF1 = new Ozeki.Media.VideoViewerWF();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Stop_Recording = new System.Windows.Forms.Button();
            this.Start_Recording = new System.Windows.Forms.Button();
            this.detectMotion = new System.Windows.Forms.CheckBox();
            this.label_Motion = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cb_mobileAlert = new System.Windows.Forms.CheckBox();
            this.lbl_endpoint = new System.Windows.Forms.Label();
            this.lbl_hint = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonStopServer = new System.Windows.Forms.Button();
            this.buttonStartServer = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.buttonDisconnect.Enabled = false;
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
            this.buttonConnect.Enabled = false;
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
            this.cameraUrl.Enabled = false;
            this.cameraUrl.Location = new System.Drawing.Point(9, 40);
            this.cameraUrl.Name = "cameraUrl";
            this.cameraUrl.ReadOnly = true;
            this.cameraUrl.Size = new System.Drawing.Size(196, 20);
            this.cameraUrl.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Camera URL:";
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
            this.groupBox2.Controls.Add(this.Stop_Recording);
            this.groupBox2.Controls.Add(this.Start_Recording);
            this.groupBox2.Controls.Add(this.detectMotion);
            this.groupBox2.Location = new System.Drawing.Point(418, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(352, 149);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Controllers";
            // 
            // Stop_Recording
            // 
            this.Stop_Recording.Enabled = false;
            this.Stop_Recording.Location = new System.Drawing.Point(110, 111);
            this.Stop_Recording.Name = "Stop_Recording";
            this.Stop_Recording.Size = new System.Drawing.Size(94, 23);
            this.Stop_Recording.TabIndex = 18;
            this.Stop_Recording.Text = "Stop recording";
            this.Stop_Recording.UseVisualStyleBackColor = true;
            this.Stop_Recording.Click += new System.EventHandler(this.Stop_Recording_Click);
            // 
            // Start_Recording
            // 
            this.Start_Recording.Enabled = false;
            this.Start_Recording.Location = new System.Drawing.Point(10, 111);
            this.Start_Recording.Name = "Start_Recording";
            this.Start_Recording.Size = new System.Drawing.Size(94, 23);
            this.Start_Recording.TabIndex = 17;
            this.Start_Recording.Text = "Start recording";
            this.Start_Recording.UseVisualStyleBackColor = true;
            this.Start_Recording.Click += new System.EventHandler(this.Start_Recording_Click);
            // 
            // detectMotion
            // 
            this.detectMotion.AutoSize = true;
            this.detectMotion.Enabled = false;
            this.detectMotion.Location = new System.Drawing.Point(10, 44);
            this.detectMotion.Name = "detectMotion";
            this.detectMotion.Size = new System.Drawing.Size(100, 17);
            this.detectMotion.TabIndex = 16;
            this.detectMotion.Text = "Motion detector";
            this.detectMotion.UseVisualStyleBackColor = true;
            this.detectMotion.CheckedChanged += new System.EventHandler(this.detectMotion_CheckedChanged);
            // 
            // label_Motion
            // 
            this.label_Motion.Location = new System.Drawing.Point(415, 165);
            this.label_Motion.Name = "label_Motion";
            this.label_Motion.Size = new System.Drawing.Size(122, 24);
            this.label_Motion.TabIndex = 6;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cb_mobileAlert);
            this.groupBox3.Controls.Add(this.lbl_endpoint);
            this.groupBox3.Controls.Add(this.lbl_hint);
            this.groupBox3.Controls.Add(this.lblState);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.buttonStopServer);
            this.groupBox3.Controls.Add(this.buttonStartServer);
            this.groupBox3.Location = new System.Drawing.Point(418, 192);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(352, 196);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Streaming";
            // 
            // cb_mobileAlert
            // 
            this.cb_mobileAlert.AutoSize = true;
            this.cb_mobileAlert.Enabled = false;
            this.cb_mobileAlert.Location = new System.Drawing.Point(10, 144);
            this.cb_mobileAlert.Name = "cb_mobileAlert";
            this.cb_mobileAlert.Size = new System.Drawing.Size(80, 17);
            this.cb_mobileAlert.TabIndex = 15;
            this.cb_mobileAlert.Text = "Mobile alert";
            this.cb_mobileAlert.UseVisualStyleBackColor = true;
            this.cb_mobileAlert.CheckedChanged += new System.EventHandler(this.cb_mobileAlert_CheckedChanged);
            // 
            // lbl_endpoint
            // 
            this.lbl_endpoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_endpoint.Location = new System.Drawing.Point(7, 19);
            this.lbl_endpoint.Name = "lbl_endpoint";
            this.lbl_endpoint.Size = new System.Drawing.Size(150, 17);
            this.lbl_endpoint.TabIndex = 13;
            this.lbl_endpoint.Text = "Connect number";
            this.lbl_endpoint.Visible = false;
            // 
            // lbl_hint
            // 
            this.lbl_hint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_hint.Location = new System.Drawing.Point(7, 36);
            this.lbl_hint.Name = "lbl_hint";
            this.lbl_hint.Size = new System.Drawing.Size(166, 55);
            this.lbl_hint.TabIndex = 12;
            this.lbl_hint.Visible = false;
            // 
            // lblState
            // 
            this.lblState.Location = new System.Drawing.Point(54, 81);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(109, 21);
            this.lblState.TabIndex = 11;
            this.lblState.UseMnemonic = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "State";
            // 
            // buttonStopServer
            // 
            this.buttonStopServer.Enabled = false;
            this.buttonStopServer.Location = new System.Drawing.Point(88, 167);
            this.buttonStopServer.Name = "buttonStopServer";
            this.buttonStopServer.Size = new System.Drawing.Size(75, 23);
            this.buttonStopServer.TabIndex = 5;
            this.buttonStopServer.Text = "Stop";
            this.buttonStopServer.UseVisualStyleBackColor = true;
            this.buttonStopServer.Click += new System.EventHandler(this.buttonStopServer_Click);
            // 
            // buttonStartServer
            // 
            this.buttonStartServer.Enabled = false;
            this.buttonStartServer.Location = new System.Drawing.Point(7, 167);
            this.buttonStartServer.Name = "buttonStartServer";
            this.buttonStartServer.Size = new System.Drawing.Size(75, 23);
            this.buttonStartServer.TabIndex = 4;
            this.buttonStartServer.Text = "Start";
            this.buttonStartServer.UseVisualStyleBackColor = true;
            this.buttonStartServer.Click += new System.EventHandler(this.buttonStartServer_Click);
            // 
            // MotionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label_Motion);
            this.Controls.Add(this.labelState);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.videoViewerWF1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MotionForm";
            this.Text = "Motion Surveillance";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonStopServer;
        private System.Windows.Forms.Button buttonStartServer;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_hint;
        private System.Windows.Forms.Label lbl_endpoint;
        private System.Windows.Forms.CheckBox cb_mobileAlert;
        private System.Windows.Forms.Button Stop_Recording;
        private System.Windows.Forms.Button Start_Recording;
        private System.Windows.Forms.CheckBox detectMotion;
    }
}