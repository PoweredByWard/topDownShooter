using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public partial class MainScreen : Form
    {
        List<Panel> tabs;
        string account;
        string previewType;
        string previewItem;
        public MainScreen()
        {
            InitializeComponent();
            tabs = this.Controls.OfType<Panel>().ToList();
            ResourceHandler.checkResources();
            refreshCoins();
            showProfile();
            
        }

        private void pbPlay_Click(object sender, EventArgs e)
        {
            ResourceHandler.checkResources();
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
            account = username == null ? AccountHandler.getUsername() : username;

            lblProfileTitle.Text = account != AccountHandler.getUsername() ? $"{account}'s Profile" : "Your profile";

            if (!DataHandler.isAdmin(account))pbDelete.Visible = DataHandler.isAdmin(AccountHandler.getUsername());

            tlpSearch.Visible = false;
            if (DataHandler.isAdmin(AccountHandler.getUsername()))
            {
                txtSearch.Visible = true;
                pbReset.Visible = true;
            }

            const int ROW_HEIGHT = 30;
            DataTable tbl = DataHandler.getProfile(account);

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

        private void pbExit_Click(object sender, EventArgs e) => System.Windows.Forms.Application.Exit();
        private void pbInventory_Click(object sender, EventArgs e) => showInventory();

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
            Panel inventoryItem = new Panel();
            inventoryItem.Size = new Size(100,120);
            string[] rgb = item[5].ToString().Split(',');

            inventoryItem.BackColor = Color.FromArgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));

            inventoryItem.BackgroundImage = Image.FromFile(item[2].ToString());
            inventoryItem.Name = $"pnl{item[0]}";
            inventoryItem.BackgroundImageLayout = ImageLayout.Center;
            inventoryItem.Cursor = Cursors.Hand;

            inventoryItem.Click += new EventHandler(showItem);

            return inventoryItem;
        }


        private void showResultsSearch(string searchValue)
        {
            tlpSearch.Visible = searchValue != "";

            DataTable result = DataHandler.findUser(searchValue);

            tlpSearch.Controls.Clear();

            Font lblFont = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
            tlpSearch.RowCount = 0;
            tlpSearch.RowStyles.Clear();
            int height = 0;
            for (int i = 0; i < result.Rows.Count; i++)
            {
                tlpSearch.RowCount++;
                tlpSearch.RowStyles.Add(new RowStyle(SizeType.Absolute, 21));

                Label lblSearchItem = new Label {
                    Text = result.Rows[i][0].ToString(),
                    Height = 20,
                    BackColor = Color.White,
                    Width = tlpSearch.Width,
                    Font = lblFont,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom),
                    ForeColor = Color.Black,
                    Cursor = Cursors.Hand
                };
                height += lblSearchItem.Height;
                lblSearchItem.Click += new EventHandler(searchClick);
                tlpSearch.Controls.Add(lblSearchItem, 0, i);
            }

            if (result.Rows.Count==0)
            {
                tlpSearch.RowCount++;
                tlpSearch.RowStyles.Add(new RowStyle(SizeType.Absolute, 21));

                Label lblSearchItem = new Label
                {
                    Text = "No results",
                    Height = 20,
                    BackColor = Color.White,
                    Width = tlpSearch.Width,
                    Font = lblFont,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom),
                    ForeColor = Color.Black,
                };
                height += lblSearchItem.Height;
                tlpSearch.Controls.Add(lblSearchItem, 0, 0);
            }

            tlpSearch.Height = height;
        }

        private void activated(object sender, EventArgs e) => refreshCoins();

        private void txtSearch_TextChanged(object sender, EventArgs e) => showResultsSearch(((TextBox)sender).Text);

        private void searchClick(object sender, EventArgs e) => showProfile(((Label)sender).Text);

        private void MainScreen_MouseDown(object sender, MouseEventArgs e) => tlpSearch.Visible = false;
        private void pbCancel_Click(object sender, EventArgs e) => showInventory();

        private void delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show($"Are you sure you want to delete {account}'s account?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No) return;
            bool deleted = DataHandler.deleteAccount(account);
            if (deleted) 
            {
                MessageBox.Show($"{account}'s Account got deleted.");
                showProfile();
            }else MessageBox.Show($"Couldn't delete {account}'s account.");
        }

        private void reset_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show($"Are you sure you want to reset {account}'s account?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No) return;
            bool reset = DataHandler.resetAccount(account);
            if (reset)
            {
                MessageBox.Show($"{account}'s Account got reset.");
                showProfile(account);
            }
            else MessageBox.Show($"Couldn't reset {account}'s account.");
        }

        private void showPanelItem(string type = null)
        {
            previewType = type;
            if (type != null)previewItem = null;
            foreach (Panel pnl in tabs)
            {
                if (pnl.Name != "pnlItem")
                {
                    pnl.Visible = false;
                }
                else
                {
                    pnl.Visible = true;
                }
            }
            tbPower.Maximum = 2;
            if (previewItem != null)
            {
                DataTable itemtbl = DataHandler.getItem(previewItem);
                byte[] imageBytes = (byte[])itemtbl.Rows[0][6];
                Console.WriteLine(imageBytes);
                MemoryStream buf = new MemoryStream(imageBytes);
                pbItemPreview.BackgroundImage = Image.FromStream(buf, true);
                tbName.Text = itemtbl.Rows[0][1].ToString();
                tbPower.Value = int.Parse(itemtbl.Rows[0][7].ToString());
                string[] rgb = itemtbl.Rows[0][5].ToString().Split(',');
                Color backgroundColor = Color.FromArgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));
                pbColor.BackColor = backgroundColor;
                pbItemPreview.BackColor = backgroundColor;
            }
            else
            {
                pbColor.BackColor = Color.Transparent;
                pbItemPreview.BackColor = Color.Transparent;
                tbName.Clear();
                tbPower.Value = 0;
                pbItemPreview.BackgroundImage = null;
            }
        }

        private void showItem(object sender, EventArgs e) 
        {
            Panel inventoryItem = (Panel)sender;
            previewItem = inventoryItem.Name.Substring(3);
            showPanelItem();
        }

        private void pbItemPreview_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog Dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                Title ="Select Image",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;",
            };

            Image item;
            Size imageSize = DataHandler.getTypeSizeByName(previewType);
            if (Dialog.ShowDialog() == DialogResult.OK)
            {
                item = new Bitmap(Image.FromFile(Dialog.FileName),imageSize);
                pbItemPreview.BackgroundImage = item;
            }
        }

        private void pbPlayerAdd_Click(object sender, EventArgs e) => showPanelItem("Player");
        private void pbGunAdd_Click(object sender, EventArgs e) => showPanelItem("Gun");

        private void pbSave_Click(object sender, EventArgs e)
        {
            if (tbName.Text=="")
            {
                MessageBox.Show("Name can't be empty");
                return;
            }
            if (tbName.Text.Length>64)
            {
                MessageBox.Show("Name is to long.");
                return;
            }

            DataHandler.saveItem(tbName.Text,tbPower.Value.ToString(),pbColor.BackColor, pbItemPreview.BackgroundImage,previewItem);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DataRow item;
            string[] rgb = new string[] { };
            if (previewItem!=null)
            {
                item = DataHandler.getItem(previewItem).Rows[0];
                rgb = item[5].ToString().Split(',');
            }
            ColorDialog MyDialog = new ColorDialog();

            MyDialog.AllowFullOpen = true;

            MyDialog.Color = previewItem != null ? Color.White : Color.FromArgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));

            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                pbColor.BackColor = MyDialog.Color;
                pbItemPreview.BackColor = MyDialog.Color;
            }
        }
    }
}


