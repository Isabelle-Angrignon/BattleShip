using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlotteDLL;
using CommClient;

namespace BattleShipVue
{

    public partial class MainFrame : Form
    {
        // Private
        private int _selectedRow = -1;
        private int _selectedColumn = -1;
        private string nomBateauCourant = "";
        private int positionCourante = 0; // représente la position du bateau qui doit être placé dans la flotte
        private Flotte maFlotte;
        private int grandeurBateauCourant = 0;
        private CommClients comm;
        // Public
        public String nomJoueur;
        public String nomEnemi = "Illuminati";
        public static string[] headerY = new string[DIMENSION_GRILLE_Y] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        public const int DIMENSION_GRILLE_X = 10;
        public const int DIMENSION_GRILLE_Y = 10;


        public MainFrame()
        {
            InitializeComponent();
            InitTheGrid(DGV_MaGrille);
            InitTheGrid(DGV_GrilleEnemi);
            maFlotte = new Flotte();
            debutPlacerFlotte();
        }

        private void InitTheGrid(DataGridView dgv)
        {
            for (int i = 0; i < DIMENSION_GRILLE_X; ++i)
            {
                dgv.Columns.Add(headerY[i], headerY[i]);
            }
            dgv.Rows.Add(DIMENSION_GRILLE_Y - 1);
            for (int i = 1; i <= DIMENSION_GRILLE_Y; ++i)
            {
                dgv.Rows[i - 1].HeaderCell.Value = i.ToString();
            }
        }
        private void debutPlacerFlotte()
        {
            placerProchainBateau();
        }
        private void placerBateaux(Navire navire)
        {
            ecrireAuLog("Veuillez placer votre " + navire._nom + "  (" + navire._pos.Length + ")");
            nomBateauCourant = navire._nom;
            grandeurBateauCourant = navire._pos.Length;
        }

        private void MainFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            String messageQuitter = "Êtes-vous sur de vouloir quitter cette partie? (Toute progression va être perdu)";
            String nomMessage = "Vous êtes sur le point de quitter";
            if (MessageBox.Show(messageQuitter, nomMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
            {
                e.Cancel = true;
            }
            else
            {
                if (comm != null)
                {
                    comm.Communiquer("Quitte");
                }
            }

        }
        /// <summary>
        /// Cette fonction prend le texte passer en paramêtre et l'ajoute au log
        /// </summary>
        /// <param name="text"></param>
        private void ecrireAuLog(String text)
        {
            TB_Log.AppendText(text);
            TB_Log.AppendText(Environment.NewLine);
        }
        private Pos[] posBateauCourant(DataGridView dgv)
        {
            Pos[] tabPos = new Pos[dgv.SelectedCells.Count];
            int compteurCell = 0;
            foreach (DataGridViewCell cell in dgv.SelectedCells)
            {
                tabPos[compteurCell] = new Pos();
                tabPos[compteurCell]._x = cell.ColumnIndex;
                tabPos[compteurCell]._y = cell.RowIndex;
                ++compteurCell;
            }

            return tabPos;
        }

        private bool validerConsecutivite(Pos[] tabPos)
        {
            bool estConsecutif = false;
            if (tabPos != null)
            {
                int DifferenceX = Math.Abs(tabPos[0]._x - tabPos[tabPos.Length - 1]._x);
                int DifferenceY = Math.Abs(tabPos[0]._y - tabPos[tabPos.Length - 1]._y);

                // Avec les possibilités de sélection réduit à une seule ligne droite (pas de diagonale),
                // si la sélection est valide (consécutive), la taille de la selection doit être égal à la différence entre la première
                // et la dernière case
                if (DifferenceX + DifferenceY == tabPos.Length - 1)
                {
                    estConsecutif = true;
                }
            }
            return estConsecutif;
        }
        private bool validerPositionUnique(Pos[] tabPos)
        {
            bool estUnique = true;

            // Vérifie si les points sélectionnés correspondent au point d'un bateau déjà placé
            foreach (Navire navire in maFlotte._flotte)
            {
                foreach (Pos p in tabPos)
                {
                    for (int i = 0; i < navire._pos.Length; ++i)
                    {
                        if (navire._pos[i]._x == p._x && navire._pos[i]._y == p._y)
                        {
                            estUnique = false;
                        }
                    }
                }
            }

            return estUnique;
        }
        private void setBackgroundColor_of_DGV(DataGridView dgv, Color couleur)
        {
            Pos[] tabPos = posBateauCourant(dgv);
            foreach (Pos p in tabPos)
            {
                dgv.Rows[p._y].Cells[p._x].Style.BackColor = couleur;
            }
        }
        private void DGV_MaGrille_MouseUp(object sender, MouseEventArgs e)
        {
        }

        /// <summary>
        /// Cette fonction affiche à la console usager le bateau qui doit être placé
        /// </summary>
        private void placerProchainBateau()
        {
            placerBateaux(maFlotte._flotte[positionCourante]);
        }
        /// <summary>
        /// Cette fonction vérifie si la sélection est de la même taille que le bateau courant
        /// </summary>
        /// <returns></returns>
        private bool validerSelectionValide()
        {
            bool estValide = true;
            Pos[] tabPos = posBateauCourant(DGV_MaGrille);

            foreach (Navire nav in maFlotte._flotte)
            {
                if (nomBateauCourant == nav._nom && nav._pos.Length != tabPos.Length)
                {
                    estValide = false;
                }
            }

            return estValide;
        }

        private void finPlacerBateaux()
        {
            DGV_GrilleEnemi.Enabled = true;
            BTN_Placer.Enabled = false;
            BTN_Attaquer.Enabled = true;
            DGV_MaGrille.Enabled = false;

            comm = new CommClients("172.17.104.103", 8888);
            nomJoueur = comm.nom;
            ecrireAuLog("Bonjour " + nomJoueur + " ! ");

            envoyerFlotte();
        }

        private void BTN_Placer_Click(object sender, EventArgs e)
        {
            Pos[] tabPos = posBateauCourant(DGV_MaGrille);
            if (tabPos.Length > 0)
            {
                if (validerConsecutivite(tabPos))
                {
                    if (validerPositionUnique(tabPos))
                    {
                        if (validerSelectionValide())
                        {
                            // Trouver le bateau qui correspond au bateau courant et initialise sa position avec les cases données
                            foreach (Navire navire in maFlotte._flotte)
                            {
                                if (navire._nom == nomBateauCourant)
                                {
                                    navire.placerNavire(tabPos);
                                    ecrireAuLog("Le " + nomBateauCourant + " à été placé ");
                                    setBackgroundColor_of_DGV(DGV_MaGrille, Color.GreenYellow);
                                    DGV_MaGrille.ClearSelection();
                                }
                            }

                            if (++positionCourante < maFlotte._flotte.Length)
                                placerProchainBateau();
                            else
                                finPlacerBateaux();
                        }
                        else
                        {
                            ecrireAuLog("Cette sélection n'est pas valide car elle n'est pas de la même grandeur que le bateau que vous devez placer.");
                        }

                    }
                    else
                    {
                        ecrireAuLog("Cette sélection n'est pas valide car elle contient une case déjà utilisé.");
                    }
                }
                else
                {
                    ecrireAuLog("Cette sélection n'est pas valide car elle n'est pas consécutive.");
                }
            }
            else
            {
                ecrireAuLog("Vous devez sélectionner au moins une case pour placer un bateau.");
            }
        }

        private void recommencerPartieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void finDuTour()
        {
            DGV_GrilleEnemi.Enabled = false;
            DGV_MaGrille.Enabled = false;
            BTN_Attaquer.Enabled = false;
            BTN_Placer.Enabled = false;

            ecrireAuLog("En attente d'une réponse de votre adversaire.");
        }

        /// <summary>
        /// Cette fonction divise 
        /// </summary>
        /// <param name="pos">Le String résultant du serveur suite à une attaque contre ma grille</param>
        private void traiterMessageAttaque(String messageAttaque, DataGridView dgvConcerne)
        {
            String[] message = messageAttaque.Split('=');

            if (message.Length > 1)
            {
                switch (message[0])
                {
                    case "Manque":
                        Pos position = convertStringToPos(message[1]);
                        dgvConcerne.Rows[position._y].Cells[position._x].Style.BackColor = Color.Yellow;
                        ecrireAuLog("Attaque manquée à la case : " + position.ToString());
                        return;

                    case "Touche":
                        bateauEstTouche(message[1], dgvConcerne);
                        return;

                    case "Coule":
                        if (dgvConcerne.Name == "DGV_MaGrille")
                        {
                            coulerMonNavire(message[1], dgvConcerne);
                        }
                        else
                        {
                            coulerNavireEnemi(message[2], dgvConcerne);
                            ecrireAuLog("Le " + message[1] + " enemi est coullé");
                        }

                        return;

                    case "Gagnant":
                        bateauEstTouche(message[1], dgvConcerne);
                        partieGagnee();
                        return;

                    case "Perdant":
                        bateauEstTouche(message[1], dgvConcerne);
                        partiePerdu();
                        return;

                    default:
                        return;
                }
            }
            else
            {
                if (message[0] != "Attaque")
                    ecrireAuLog("Erreur ! Nombre de paramêtre insufisant dans le message du serveur.");
            }

        }

        private void partieGagnee()
        {
            ecrireAuLog("Vous avez gagnez ! ");
            TB_Log.BackColor = Color.Green;
            BTN_Attaquer.Enabled = false;
            DGV_GrilleEnemi.Enabled = false;
        }
        private void partiePerdu()
        {

            ecrireAuLog("Vous avez perdu ! Meilleure chance la prochaine fois ! =) ");
            TB_Log.BackColor = Color.Red;
            BTN_Attaquer.Enabled = false;
            DGV_GrilleEnemi.Enabled = false;
        }
        private void bateauEstTouche(string position, DataGridView dgvConcerne)
        {
            if (position.Length == 2)
            {
                Pos pos = convertStringToPos(position);
                dgvConcerne.Rows[pos._y].Cells[pos._x].Style.BackColor = Color.Tomato;

                ecrireAuLog("Touché à la position: " + pos.ToString());
            }
        }

        private void coulerNavireEnemi(String navire, DataGridView dgv)
        {
            String[] posDuNavire = navire.Split(',');

            foreach (String pos in posDuNavire)
            {
                dgv.Rows[int.Parse(pos[1].ToString())].Cells[int.Parse(pos[0].ToString())].Style.BackColor = Color.Red;
            }
        }
        private void coulerMonNavire(String nomNavire, DataGridView dgv)
        {
            bool estTrouve = false;

            foreach (Navire nav in maFlotte._flotte)
            {
                if (nav._nom == nomNavire)
                {
                    foreach (Pos pos in nav._pos)
                    {
                        dgv.Rows[pos._y].Cells[pos._x].Style.BackColor = Color.Red;
                        estTrouve = true;
                    }
                }
            }

            if (!estTrouve)
            {
                ecrireAuLog("Le bateau " + nomNavire + " n'est pas coullable car il est introuvable dans la flotte courrante.");
            }
            else
            {
                ecrireAuLog("Votre " + nomNavire + " est coullé.");
            }
        }
        private String attaquer()
        {
            string x = DGV_GrilleEnemi.SelectedCells[0].ColumnIndex.ToString();
            string y = DGV_GrilleEnemi.SelectedCells[0].RowIndex.ToString();
            String reponse = comm.Communiquer(x + y);
            DGV_GrilleEnemi.ClearSelection();

            return reponse;
        }

        private Pos convertStringToPos(String aConvertir)
        {
            Pos pos = new Pos();
            if (aConvertir.Length > 1)
            {
                pos._x = int.Parse(aConvertir.Substring(0, 1));
                pos._y = int.Parse(aConvertir.Substring(1, 1));
            }
            return pos;
        }
        private void envoyerFlotte()
        {
            finDuTour();
            String reponse = comm.Communiquer("Flotte:" + maFlotte.ToString());
            debutDuTour();
            traiterMessageAttaque(reponse, DGV_MaGrille);
        }

        private void debutDuTour()
        {
            DGV_GrilleEnemi.Enabled = true;
            BTN_Attaquer.Enabled = true;

            ecrireAuLog("Début de votre tour !");
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

        private void DGV_MaGrille_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            if (dgv.SelectedCells.Count == grandeurBateauCourant)
            {
                dgv.DefaultCellStyle.SelectionBackColor = Color.DarkBlue;
            }
            else
            {
                dgv.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
            }
            switch (dgv.SelectedCells.Count)
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
                if (cell.RowIndex == _selectedRow && dgv.SelectedCells.Count < grandeurBateauCourant + 1)
                {
                    if (cell.ColumnIndex != _selectedColumn)
                    {
                        _selectedColumn = -1;
                    }
                }
                else if (cell.ColumnIndex == _selectedColumn && dgv.SelectedCells.Count < grandeurBateauCourant + 1)
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

        private void BTN_Attaquer_Click(object sender, EventArgs e)
        {
            sequenceAttaque();
        }

        private void sequenceAttaque()
        {
            String reponse = attaquer();
            traiterMessageAttaque(reponse, DGV_GrilleEnemi);
            finDuTour();
            String attaqueEnemi = comm.Communiquer("Ok");
            debutDuTour();
            traiterMessageAttaque(attaqueEnemi, DGV_MaGrille);
        }
    }
}
