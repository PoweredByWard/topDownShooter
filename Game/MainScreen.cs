using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public partial class MainScreen : Form
    {
        private AccountHandler account;
        private DataHandler data = new DataHandler();
        List<Panel> tabs;
        public MainScreen(AccountHandler accountData)
        {
            InitializeComponent();
            account = accountData;
            tabs = this.Controls.OfType<Panel>().ToList();
            showProfile();
        }

        private void pbPlay_Click(object sender, EventArgs e)
        {
            bool opened = false;
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                if (Application.OpenForms[i].Name == "GameForm")
                {
                    Application.OpenForms[i].Show();
                    Application.OpenForms[i].BringToFront();
                    opened = true;
                }
            }
            if (!opened)
            {
                GameForm gameFrm = new GameForm(account);
                gameFrm.Show();
            }

        }

        private void pbProfile_Click(object sender, EventArgs e)
        {
            showProfile();
        }

        private void showProfile(string username = null)
        {
            foreach (Panel pnl in tabs)
            {
                if (pnl.Name != "pnlProfile")
                {
                    pnl.Visible = false;
                }
                else
                {
                    pnl.Visible = true;
                }
            }
        }

        private void pbScoreboard_Click(object sender, EventArgs e)
        {
            showLeaderboard();
        }

        private void showLeaderboard()
        {
            foreach (Panel pnl in tabs)
            {
                if (pnl.Name != "pnlScoreboard")
                {
                    pnl.Visible = false;
                }
                else
                {
                    pnl.Visible = true;
                }
            }
            DataTable tbl = data.getTop(10);
            tlpScoreboard.Controls.Clear();
            tlpScoreboard.RowCount = 0;
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                tlpScoreboard.RowCount++;
                tlpScoreboard.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
                Font lblFont = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Regular);
                ContentAlignment align = ContentAlignment.TopCenter;
                AnchorStyles anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom);
                tlpScoreboard.Controls.Add(new Label() { TextAlign = align, Text = tlpScoreboard.RowCount.ToString(), Font = lblFont, ForeColor = Color.White, Anchor = anchor }, 0, i);
                tlpScoreboard.Controls.Add(new Label() { TextAlign = align, Text = tbl.Rows[i][0].ToString(), Font = lblFont, ForeColor = Color.White, Anchor = anchor }, 1, i);
                tlpScoreboard.Controls.Add(new Label() { TextAlign = align, Text = tbl.Rows[i][1].ToString(), Font = lblFont, ForeColor = Color.White, Anchor = anchor }, 2, i);
                tlpScoreboard.Controls.Add(new Label() { TextAlign = align, Text = tbl.Rows[i][2].ToString(), Font = lblFont, ForeColor = Color.White, Anchor = anchor }, 3, i);
                tlpScoreboard.Controls.Add(new Label() { TextAlign = align, Text = tbl.Rows[i][3].ToString(), Font = lblFont, ForeColor = Color.White, Anchor = anchor }, 4, i);
            }
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void pbInventory_Click(object sender, EventArgs e)
        {
            showInventory();
        }

        private void showInventory()
        {
            foreach (Panel pnl in tabs)
            {
                if (pnl.Name != "pnlInventory")
                {
                    pnl.Visible = false;
                }
                else
                {
                    pnl.Visible = true;
                }
            }
        }
    }
}
