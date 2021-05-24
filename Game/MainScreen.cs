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
        List<Panel> tabs;
        public MainScreen()
        {
            InitializeComponent();
            tabs = this.Controls.OfType<Panel>().ToList();
            refreshCoins();
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
                GameForm   gameFrm = new GameForm();
                gameFrm.Show();
            }

        }

        private void pbProfile_Click(object sender, EventArgs e)
        {
            showProfile();
        }

        public void refreshCoins()
        {
            lblCoin.Text = DataHandler.getCoins().ToString();
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
            const int ROW_HEIGHT = 30;
            DataTable tbl = DataHandler.getProfile(username==null?AccountHandler.getUsername():username);

            tlpStats.Controls.Clear();
            tlpStats.RowStyles.RemoveAt(0);
            tlpStats.RowCount = 4;
            tlpStats.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
            Font lblFont = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Regular);
            ContentAlignment align = ContentAlignment.TopCenter;
            AnchorStyles anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom);
            tlpStats.RowStyles.Add(new RowStyle(SizeType.Absolute, ROW_HEIGHT));
            tlpStats.Controls.Add(new Label() { TextAlign = align, Text = "Completed Waves:", Font = lblFont, ForeColor = Color.White, Anchor = anchor }, 0, 0);
            tlpStats.Controls.Add(new Label() { TextAlign = align, Text = tbl.Rows[0][2].ToString() == "" ? "0" : tbl.Rows[0][2].ToString(), Font = lblFont, ForeColor = Color.White, Anchor = anchor }, 1, 0);

            tlpStats.RowStyles.Add(new RowStyle(SizeType.Absolute, ROW_HEIGHT));
            tlpStats.Controls.Add(new Label() { TextAlign = align, Text = "Total Kills:", Font = lblFont, ForeColor = Color.White, Anchor = anchor }, 0, 1);
            tlpStats.Controls.Add(new Label() { TextAlign = align, Text = tbl.Rows[0][0].ToString() == "" ? "0" : tbl.Rows[0][0].ToString(), Font = lblFont, ForeColor = Color.White, Anchor = anchor }, 1, 1);

            tlpStats.RowStyles.Add(new RowStyle(SizeType.Absolute, ROW_HEIGHT));
            tlpStats.Controls.Add(new Label() { TextAlign = align, Text = "Total Damage Dealt:", Font = lblFont, ForeColor = Color.White, Anchor = anchor }, 0, 2);
            tlpStats.Controls.Add(new Label() { TextAlign = align, Text = tbl.Rows[0][4].ToString() == "" ? "0" : tbl.Rows[0][4].ToString(), Font = lblFont, ForeColor = Color.White, Anchor = anchor }, 1, 2);

            tlpStats.RowStyles.Add(new RowStyle(SizeType.Absolute, ROW_HEIGHT));
            tlpStats.Controls.Add(new Label() { TextAlign = align, Text = "Total Turrets Placed:", Font = lblFont, ForeColor = Color.White, Anchor = anchor }, 0, 3);
            tlpStats.Controls.Add(new Label() { TextAlign = align, Text = tbl.Rows[0][1].ToString() == "" ? "0" : tbl.Rows[0][1].ToString(), Font = lblFont, ForeColor = Color.White, Anchor = anchor }, 1, 3);

            tlpStats.RowStyles.Add(new RowStyle(SizeType.Absolute, ROW_HEIGHT));
            tlpStats.Controls.Add(new Label() { TextAlign = align, Text = "Total Playtime:", Font = lblFont, ForeColor = Color.White, Anchor = anchor }, 0, 4);
            tlpStats.Controls.Add(new Label() { TextAlign = align, Text = tbl.Rows[0][3].ToString() == "" ? "0:00:00" : tbl.Rows[0][3].ToString(), Font = lblFont, ForeColor = Color.White, Anchor = anchor }, 1, 4);
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
            DataTable tbl = DataHandler.getTop(10);
            tlpScoreboard.Controls.Clear();
            tlpScoreboard.RowCount = 0;
            tlpScoreboard.RowStyles.RemoveAt(0);
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                tlpScoreboard.RowCount++;
                tlpScoreboard.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
                Font lblFont = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Regular);
                ContentAlignment align = ContentAlignment.TopCenter;
                AnchorStyles anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom);
                tlpScoreboard.Controls.Add(new Label() { TextAlign = align, Text = tlpScoreboard.RowCount.ToString(), Font = lblFont, ForeColor = Color.White, Anchor = anchor, Height=20 }, 0, i);
                tlpScoreboard.Controls.Add(new Label() { TextAlign = align, Text = tbl.Rows[i][0].ToString(), Font = lblFont, ForeColor = Color.White, Anchor = anchor, Height = 20 }, 1, i);
                tlpScoreboard.Controls.Add(new Label() { TextAlign = align, Text = tbl.Rows[i][1].ToString(), Font = lblFont, ForeColor = Color.White, Anchor = anchor, Height = 20 }, 2, i);
                tlpScoreboard.Controls.Add(new Label() { TextAlign = align, Text = tbl.Rows[i][2].ToString(), Font = lblFont, ForeColor = Color.White, Anchor = anchor, Height = 20 }, 3, i);
                tlpScoreboard.Controls.Add(new Label() { TextAlign = align, Text = tbl.Rows[i][3].ToString().Substring(3), Font = lblFont, ForeColor = Color.White, Anchor = anchor, Height = 20 }, 4, i);
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
            DataTable types = DataHandler.getItemTypes();
            DataTable playerItemsData = DataHandler.getPlayerItems();
            List<string> playerItems = getPlayerItems(playerItemsData);

            tlpPlayer.Controls.Clear();
            tlpPlayer.RowStyles.Clear();
            tlpPlayer.ColumnStyles.Clear();

            tlpGun.Controls.Clear();
            tlpGun.RowStyles.Clear();
            tlpGun.ColumnStyles.Clear();

            Font lblFont = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
            ContentAlignment align = ContentAlignment.BottomCenter;
            AnchorStyles anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom);

            for (int t = 0; t < types.Rows.Count; t++)
            {
                DataTable items = DataHandler.getItems(types.Rows[t][0].ToString());
                if (types.Rows[t][1].ToString()=="Gun")
                {
                    tlpGun.RowCount++;
                    tlpGun.RowStyles.Add(new RowStyle(SizeType.Absolute, tlpGun.Height - 40));

                    for (int i = 0; i < items.Rows.Count; i++)
                    {
                        tlpGun.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130));
                        tlpGun.ColumnCount++;
                        bool owned = playerItems.Contains(items.Rows[i][0].ToString());
                        bool equipped = owned ? (bool.Parse(playerItemsData.Rows[playerItems.IndexOf(items.Rows[i][0].ToString())][3].ToString())) : false;
                        Image button = owned ? (equipped ? Image.FromFile("GUI/save.png") : Image.FromFile("GUI/save.png")) : Image.FromFile("GUI/scoreboard.png");
                        Label lblButton = new Label { Name = $"lbl{items.Rows[i][0]}", Text = owned ? (equipped ? "Equipped" : "Equip") : $"{items.Rows[i][4]} coins", Anchor = anchor, Font = lblFont, TextAlign = align, ForeColor = Color.White };

                        lblButton.Click += new EventHandler(itemClick);
                        tlpGun.Controls.Add(createInventoryItem(items.Rows[i]), i, 0);
                        tlpGun.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

                        PictureBox btn = new PictureBox { Name = $"btn{items.Rows[i][0]}", BackgroundImage = button, BackgroundImageLayout = ImageLayout.Stretch, Height = 35, Controls = { lblButton }, Cursor = Cursors.Hand };
                        btn.Click += new EventHandler(itemClick);
                        tlpGun.Controls.Add(btn, i, 1);
                    }
                }
                else if (types.Rows[t][1].ToString() == "Player")
                {
                    tlpPlayer.RowCount++;
                    tlpPlayer.RowStyles.Add(new RowStyle(SizeType.Absolute, tlpPlayer.Height - 40));

                    for (int i = 0; i < items.Rows.Count; i++)
                    {
                        tlpPlayer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130));
                        tlpPlayer.ColumnCount++;
                        bool owned = playerItems.Contains(items.Rows[i][0].ToString());
                        bool equipped = owned ? (bool.Parse(playerItemsData.Rows[playerItems.IndexOf(items.Rows[i][0].ToString())][3].ToString())) : false;
                        Image button = owned ? (equipped ? Image.FromFile("GUI/save.png"): Image.FromFile("GUI/save.png")) : Image.FromFile("GUI/scoreboard.png");
                        Label lblButton = new Label { Name = $"lbl{items.Rows[i][0]}", Text = owned ? (equipped? "Equipped" : "Equip") : $"{items.Rows[i][4]} coins", Anchor = anchor, Font = lblFont, TextAlign = align, ForeColor = Color.White };
                        
                        lblButton.Click += new EventHandler(itemClick);
                        tlpPlayer.Controls.Add(createInventoryItem(items.Rows[i]), i, 0);
                        tlpPlayer.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

                        PictureBox btn = new PictureBox { Name = $"btn{items.Rows[i][0]}" , BackgroundImage = button, BackgroundImageLayout = ImageLayout.Stretch, Height = 35, Controls = { lblButton }, Cursor = Cursors.Hand };
                        btn.Click += new EventHandler(itemClick);
                        tlpPlayer.Controls.Add(btn, i, 1);
                    }
                }

            }
        }

        protected void itemClick(object sender, EventArgs e)
        {
            string itemID = ((Control)sender).Name.Substring(3);
            DataTable playerItemsData = DataHandler.getPlayerItems();
            List<string> playerItems = getPlayerItems(playerItemsData);

            bool owned = playerItems.Contains(itemID);
            bool equipped = owned ? (playerItemsData.Rows[playerItems.IndexOf(itemID)][3].ToString() == "1") : false;

            if (!owned)
            {
                bool canbuy = DataHandler.buyItem(itemID);
                refreshCoins();
                if (!canbuy)
                {
                    MessageBox.Show("You don't have enough coins.");
                }
                else
                {
                    showInventory();
                }
            }
            else
            {
                if (!equipped)
                {
                    DataHandler.equipItem(itemID);
                    showInventory();
                }
            }
        }

        private List<string> getPlayerItems(DataTable playerItemsData)
        {
            List<string> playerItems = new List<string>();

            for (int i = 0; i < playerItemsData.Rows.Count; i++)
            {
                playerItems.Add(playerItemsData.Rows[i][2].ToString());
            }
            return playerItems;
        }

        private Panel createInventoryItem(DataRow item)
        {
            Font lblFont = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Regular);
            ContentAlignment align = ContentAlignment.TopCenter;
            AnchorStyles anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom);

            Panel inventoryItem = new Panel();
            inventoryItem.Size = new Size(100,120);
            string[] rgb = item[5].ToString().Split(',');

            inventoryItem.BackColor = Color.FromArgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));

            inventoryItem.BackgroundImage = Image.FromFile(item[2].ToString());
            inventoryItem.BackgroundImageLayout = ImageLayout.Center;

            return inventoryItem;
        }

        private void activated(object sender, EventArgs e)
        {
            refreshCoins();
        }
    }
}


