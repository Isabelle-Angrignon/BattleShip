namespace BattleShipVue
{
    partial class MainFrame
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DGV_MaGrille = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BTN_Placer = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BTN_Attaquer = new System.Windows.Forms.Button();
            this.DGV_GrilleEnemi = new System.Windows.Forms.DataGridView();
            this.BattleShip = new System.Windows.Forms.Label();
            this.TB_Log = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recommencerPartieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_MaGrille)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_GrilleEnemi)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGV_MaGrille
            // 
            this.DGV_MaGrille.AllowUserToResizeColumns = false;
            this.DGV_MaGrille.AllowUserToResizeRows = false;
            this.DGV_MaGrille.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGV_MaGrille.ColumnHeadersHeight = 21;
            this.DGV_MaGrille.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DGV_MaGrille.Location = new System.Drawing.Point(24, 32);
            this.DGV_MaGrille.Name = "DGV_MaGrille";
            this.DGV_MaGrille.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.DGV_MaGrille.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGV_MaGrille.RowHeadersWidth = 55;
            this.DGV_MaGrille.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DGV_MaGrille.RowTemplate.Height = 29;
            this.DGV_MaGrille.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DGV_MaGrille.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.DGV_MaGrille.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DGV_MaGrille.Size = new System.Drawing.Size(374, 313);
            this.DGV_MaGrille.TabIndex = 0;
            this.DGV_MaGrille.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.DGV_RowPrePaint);
            this.DGV_MaGrille.SelectionChanged += new System.EventHandler(this.DGV_MaGrille_SelectionChanged);
            this.DGV_MaGrille.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DGV_MaGrille_MouseUp);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.BTN_Placer);
            this.panel1.Controls.Add(this.DGV_MaGrille);
            this.panel1.Location = new System.Drawing.Point(13, 116);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(424, 405);
            this.panel1.TabIndex = 1;
            // 
            // BTN_Placer
            // 
            this.BTN_Placer.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_Placer.Location = new System.Drawing.Point(24, 351);
            this.BTN_Placer.Name = "BTN_Placer";
            this.BTN_Placer.Size = new System.Drawing.Size(374, 49);
            this.BTN_Placer.TabIndex = 1;
            this.BTN_Placer.Text = "Placer ce bateau";
            this.BTN_Placer.UseVisualStyleBackColor = true;
            this.BTN_Placer.Click += new System.EventHandler(this.BTN_Placer_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.BTN_Attaquer);
            this.panel2.Controls.Add(this.DGV_GrilleEnemi);
            this.panel2.Location = new System.Drawing.Point(443, 116);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(428, 405);
            this.panel2.TabIndex = 2;
            // 
            // BTN_Attaquer
            // 
            this.BTN_Attaquer.Enabled = false;
            this.BTN_Attaquer.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_Attaquer.Location = new System.Drawing.Point(27, 350);
            this.BTN_Attaquer.Name = "BTN_Attaquer";
            this.BTN_Attaquer.Size = new System.Drawing.Size(374, 51);
            this.BTN_Attaquer.TabIndex = 1;
            this.BTN_Attaquer.Text = "Attaquer";
            this.BTN_Attaquer.UseVisualStyleBackColor = true;
            this.BTN_Attaquer.Click += new System.EventHandler(this.BTN_Attaquer_Click);
            // 
            // DGV_GrilleEnemi
            // 
            this.DGV_GrilleEnemi.AllowUserToResizeColumns = false;
            this.DGV_GrilleEnemi.AllowUserToResizeRows = false;
            this.DGV_GrilleEnemi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGV_GrilleEnemi.ColumnHeadersHeight = 21;
            this.DGV_GrilleEnemi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DGV_GrilleEnemi.Enabled = false;
            this.DGV_GrilleEnemi.Location = new System.Drawing.Point(27, 32);
            this.DGV_GrilleEnemi.MultiSelect = false;
            this.DGV_GrilleEnemi.Name = "DGV_GrilleEnemi";
            this.DGV_GrilleEnemi.ReadOnly = true;
            this.DGV_GrilleEnemi.RowHeadersWidth = 55;
            this.DGV_GrilleEnemi.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DGV_GrilleEnemi.RowTemplate.Height = 29;
            this.DGV_GrilleEnemi.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DGV_GrilleEnemi.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.DGV_GrilleEnemi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DGV_GrilleEnemi.Size = new System.Drawing.Size(374, 313);
            this.DGV_GrilleEnemi.TabIndex = 0;
            this.DGV_GrilleEnemi.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.DGV_RowPrePaint);
            // 
            // BattleShip
            // 
            this.BattleShip.AutoSize = true;
            this.BattleShip.Font = new System.Drawing.Font("Motorwerk", 72F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BattleShip.Location = new System.Drawing.Point(195, 24);
            this.BattleShip.Name = "BattleShip";
            this.BattleShip.Size = new System.Drawing.Size(491, 73);
            this.BattleShip.TabIndex = 3;
            this.BattleShip.Text = "BattleShip";
            // 
            // TB_Log
            // 
            this.TB_Log.BackColor = System.Drawing.SystemColors.InfoText;
            this.TB_Log.ForeColor = System.Drawing.SystemColors.Window;
            this.TB_Log.Location = new System.Drawing.Point(13, 527);
            this.TB_Log.Multiline = true;
            this.TB_Log.Name = "TB_Log";
            this.TB_Log.ReadOnly = true;
            this.TB_Log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TB_Log.Size = new System.Drawing.Size(858, 126);
            this.TB_Log.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(883, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recommencerPartieToolStripMenuItem,
            this.quitterToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // recommencerPartieToolStripMenuItem
            // 
            this.recommencerPartieToolStripMenuItem.Name = "recommencerPartieToolStripMenuItem";
            this.recommencerPartieToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.recommencerPartieToolStripMenuItem.Text = "Recommencer Partie";
            this.recommencerPartieToolStripMenuItem.Click += new System.EventHandler(this.recommencerPartieToolStripMenuItem_Click);
            // 
            // quitterToolStripMenuItem
            // 
            this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
            this.quitterToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.quitterToolStripMenuItem.Text = "Quitter";
            this.quitterToolStripMenuItem.Click += new System.EventHandler(this.quitterToolStripMenuItem_Click);
            // 
            // MainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 665);
            this.Controls.Add(this.TB_Log);
            this.Controls.Add(this.BattleShip);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainFrame";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrame_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_MaGrille)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_GrilleEnemi)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGV_MaGrille;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button BTN_Attaquer;
        private System.Windows.Forms.DataGridView DGV_GrilleEnemi;
        private System.Windows.Forms.Label BattleShip;
        private System.Windows.Forms.TextBox TB_Log;
        private System.Windows.Forms.Button BTN_Placer;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recommencerPartieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
    }
}

