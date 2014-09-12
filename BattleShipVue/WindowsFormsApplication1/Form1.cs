using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShipVue
{
    public partial class MainFrame : Form
    {
        const int DIMENSION_GRILLE_X = 10;
        const int DIMENSION_GRILLE_Y = 10;
        string[] headerY = new string[DIMENSION_GRILLE_Y] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        public MainFrame()
        {
            InitializeComponent();
            InitTheGrid(DGV_MaGrille);
            InitTheGrid(DGV_GrilleEnemi);
        }

        private void InitTheGrid(DataGridView dgv)
        {
            for (int i = 0; i < DIMENSION_GRILLE_X; ++i)
            {
                dgv.Columns.Add(headerY[i], headerY[i]);
            }
            dgv.Rows.Add(DIMENSION_GRILLE_Y - 1);
            for(int i = 1; i <= DIMENSION_GRILLE_Y; ++i)
            {
                dgv.Rows[i-1].HeaderCell.Value = i.ToString();
            }
        }

        private void MainFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            String messageQuitter = "Êtes-vous sur de vouloir quitter cette partie? (Toute progression va être perdu)";
            String nomMessage = "?";
            if (MessageBox.Show(messageQuitter, nomMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
            {
                e.Cancel = true;
            }
            else
            {
                // ENVOYER UN MESSAGE AU SERVEUR POUR LUI DIRE QUE JE QUITTE
            }

        }

        /// <summary>
        /// Cette fonction sert à enlever les icones de base dans les Row Headers des DGV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGV_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintCells(e.ClipBounds, DataGridViewPaintParts.All);

            e.PaintHeader(DataGridViewPaintParts.Background
                | DataGridViewPaintParts.Border
                | DataGridViewPaintParts.Focus
                | DataGridViewPaintParts.SelectionBackground
                | DataGridViewPaintParts.ContentForeground);

            e.Handled = true;
        }

        private void DGV_MaGrille_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        int _selectedRow = -1;
        int _selectedColumn = -1;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            TB_Log.Text = "";
            int pou = 0;
            switch (DGV_MaGrille.SelectedCells.Count)
            {
                case 0:
                    // store no current selection
                    _selectedRow = -1;
                    _selectedColumn = -1;
                    return;
                case 1:
                    // store starting point for multi-select
                    _selectedRow = dgv.SelectedCells[0].RowIndex;
                    _selectedColumn = dgv.SelectedCells[0].ColumnIndex;
                    return;
            }
            foreach (DataGridViewCell cell in dgv.SelectedCells)
            {
                if (cell.RowIndex == _selectedRow && dgv.SelectedCells.Count < 6)
                {
                    if (cell.ColumnIndex != _selectedColumn)
                    {
                        _selectedColumn = -1;
                    }
                }
                else if (cell.ColumnIndex == _selectedColumn && dgv.SelectedCells.Count < 6)
                {
                    if (cell.RowIndex != _selectedRow)
                    {
                        _selectedRow = -1;
                    }
                }
                // otherwise the cell selection is illegal - de-select
                else
                {
                    cell.Selected = false;
                }
            }
        }

        private Point[] posBateauCourant(DataGridView dgv)
        {
            Point[] tabPos = new Point[dgv.SelectedCells.Count];
            int compteurCell = 0;
            foreach (DataGridViewCell cell in dgv.SelectedCells)
            {
                tabPos[compteurCell].X = cell.ColumnIndex;
                tabPos[compteurCell].Y = cell.RowIndex;
                ++compteurCell;
            }

            return tabPos;
        }

        private bool validerPositions(Point[] tabPos)
        {
            bool estValide = false;
            int DifferenceX = Math.Abs(tabPos[0].X - tabPos[tabPos.Length - 1].X);
            int DifferenceY = Math.Abs(tabPos[0].Y - tabPos[tabPos.Length - 1].Y);

            // Avec les possibilités de sélection réduit à une seule ligne droite (pas de diagonale),
            // 
            if(DifferenceX + DifferenceY == tabPos.Length - 1)
            {
                estValide = true;
            }

            return estValide;
        }

        private void DGV_MaGrille_MouseUp(object sender, MouseEventArgs e)
        {
            Point[] tabPos = posBateauCourant( (DataGridView)sender);
            TB_Log.Text = validerPositions(tabPos).ToString();
        }
    }
}
