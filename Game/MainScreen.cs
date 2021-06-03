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
        string oldPrice;
        string needKey = "";
        string valueLeaderboard;
        bool personalSB;

        Image imgMakeAdmin = Image.FromFile("GUI/makeadmin.png");
        Image imgRemoveAdmin = Image.FromFile("GUI/removeadmin.png");
        public MainScreen()
        {
            InitializeComponent();
            tabs = this.Controls.OfType<Panel>().ToList();
            ResourceHandler.checkResources();
            refreshCoins();
            showProfile();
            
        }

        //start van updates
        private void activated(object sender, EventArgs e) => refreshCoins();
        private void pbInfo_Click(object sender, EventArgs e) => Utils.showInfoMessage();

        public void refreshCoins()
        {
            lblCoin.Text = DataHandler.getCoins().ToString();
        }

        //einde van updates

        //start van play
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
                GameForm gameFrm = new GameForm();
                gameFrm.Show();
            }

        }

        //einde van play

        //start van profielen
        private void txtSearch_Search(object sender, EventArgs e) => showResultsSearch(((TextBox)sender).Text);
        private void searchClick(object sender, EventArgs e) => showProfile(((Label)sender).Text);
        private void pbProfile_Click(object sender, EventArgs e) => showProfile();
        private void MainScreen_MouseDown(object sender, MouseEventArgs e) => tlpSearch.Visible = false;
        private void checkRoleBtn() => pbAdminRole.BackgroundImage = DataHandler.isAdmin(account) ? imgRemoveAdmin : imgMakeAdmin;

        private void pbAdminRole_Click(object sender, EventArgs e)
        {
            bool valid;
            if (DataHandler.isAdmin(account))
            {
                valid = DataHandler.removeAdmin(account);
            }
            else
            {
                valid = DataHandler.addAdmin(account);
            }
            if (!valid) MessageBox.Show("Something went wrong");
            checkRoleBtn();
        }

        private void showProfile(string username = null)
        {
            this.ActiveControl = Utils.showPnl("pnlProfile", tabs);
            account = username == null ? AccountHandler.getUsername() : username;

            lblProfileTitle.Text = account != AccountHandler.getUsername() ? $"{account}'s Profile" : "Your profile";

            if (DataHandler.isAdmin(account)) pbDelete.Hide();
            else pbDelete.Show();

            tlpSearch.Visible = false;
            pbAdminRole.Hide();
            if (DataHandler.isAdmin(AccountHandler.getUsername()))
            {
                txtSearch.Show();
                pbReset.Show();
                if (account != AccountHandler.getUsername())
                {
                    pbAdminRole.Show();
                    checkRoleBtn();
                }
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

        private void delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show($"Are you sure you want to delete {account}'s account?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No) return;
            bool deleted = DataHandler.deleteAccount(account);
            if (deleted)
            {
                MessageBox.Show($"{account}'s Account got deleted.");
                showProfile();
            }
            else MessageBox.Show($"Couldn't delete {account}'s account.");
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

                Label lblSearchItem = new Label
                {
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

            if (result.Rows.Count == 0)
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

        //einde van profielen

        //start van scoreborden
        private void pbScoreboard_Click(object sender, EventArgs e) => showLeaderboard();

        private void tbValueScoreboard_Click(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.SelectionStart =  0;
            txt.SelectionLength = txt.Text.Length;
        }

        private void showLeaderboard()
        {
            valueLeaderboard = personalSB ? "7" : "10";
            this.ActiveControl = Utils.showPnl("pnlScoreboard",tabs);
            loadLeaderboard(int.Parse(valueLeaderboard));
            tlpScoreboard.AutoScroll = false;
            tlpScoreboard.HorizontalScroll.Enabled = false;
            tlpScoreboard.AutoScroll = true;
        }

        private void loadLeaderboard(int value)
        {
            DataTable tbl = DataHandler.getTop(value, personalSB);
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
                tlpScoreboard.Controls.Add(new Label() { TextAlign = align, Text = tlpScoreboard.RowCount.ToString(), Font = lblFont, ForeColor = Color.White, Anchor = anchor, Height = 20 }, 0, i);
                tlpScoreboard.Controls.Add(new Label() { TextAlign = align, Text = tbl.Rows[i][0].ToString(), Font = lblFont, ForeColor = Color.White, Anchor = anchor, Height = 20 }, 1, i);
                tlpScoreboard.Controls.Add(new Label() { TextAlign = align, Text = tbl.Rows[i][1].ToString(), Font = lblFont, ForeColor = Color.White, Anchor = anchor, Height = 20 }, 2, i);
                tlpScoreboard.Controls.Add(new Label() { TextAlign = align, Text = tbl.Rows[i][2].ToString(), Font = lblFont, ForeColor = Color.White, Anchor = anchor, Height = 20 }, 3, i);
                tlpScoreboard.Controls.Add(new Label() { TextAlign = align, Text = tbl.Rows[i][3].ToString().Substring(3), Font = lblFont, ForeColor = Color.White, Anchor = anchor, Height = 20 }, 4, i);
            }
        }

        private void pbLeaderBoardFilterSave_Click(object sender, EventArgs e) => loadLeaderboard(int.Parse((tbValueScoreboard.Text)));

        private void pbSwitchSB_Click(object sender, EventArgs e)
        {
            personalSB = !personalSB;
            if (personalSB)
            {
                valueLeaderboard = "7";
                lblFilter.Text = "Range in days:";

                pbSwitchSB.BackgroundImage = Image.FromFile("GUI/showGlobal.png");
            }
            else
            {
                valueLeaderboard = "10";
                lblFilter.Text = "Show top:";
                pbSwitchSB.BackgroundImage = Image.FromFile("GUI/showPersonal.png");
            }
            tbValueScoreboard.Text = valueLeaderboard;
            tbValueScoreboard.Location = new Point(lblFilter.Location.X + lblFilter.Width + 5, tbValueScoreboard.Location.Y);
            loadLeaderboard(int.Parse(valueLeaderboard));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Console.WriteLine(valueLeaderboard);
            Utils.validateTextInt((TextBox)sender, valueLeaderboard, valueLeaderboard);
        }

        //einde van scoreborden

        //start van inventaris
        private void pbInventory_Click(object sender, EventArgs e) => showInventory();
        private void pbCancel_Click(object sender, EventArgs e) => showInventory();
        private void pbPlayerAdd_Click(object sender, EventArgs e) => showPanelItem(DataHandler.getTypeIDByName("Player"));
        private void pbGunAdd_Click(object sender, EventArgs e) => showPanelItem(DataHandler.getTypeIDByName("Gun"));

        private void showInventory()
        {
            this.ActiveControl = Utils.showPnl("pnlInventory",tabs);
            if (DataHandler.isAdmin())
            {
                pbPlayerAdd.Show();
                pbGunAdd.Show();
            }
            else
            {
                pbPlayerAdd.Hide();
                pbGunAdd.Hide();
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

            TableLayoutPanel[] typesInventory = new TableLayoutPanel[] { tlpGun, tlpPlayer };

            for (int i = 0; i < typesInventory.Length; i++)
            {
                DataTable items = DataHandler.getItems(types.Rows[i][0].ToString());
                typesInventory[i].RowCount++;
                typesInventory[i].RowStyles.Add(new RowStyle(SizeType.Absolute, typesInventory[i].Height - 60));
                if (items.Rows.Count>3)
                {
                    typesInventory[i].AutoScroll = false;
                    typesInventory[i].VerticalScroll.Enabled = false;
                    typesInventory[i].VerticalScroll.Visible = false;
                    typesInventory[i].AutoScroll = true;
                }

                for (int r = 0; r < items.Rows.Count; r++)
                {
                    typesInventory[i].ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130));
                    typesInventory[i].ColumnCount++;
                    bool owned = playerItems.Contains(items.Rows[r][0].ToString());
                    bool equipped = owned ? (bool.Parse(playerItemsData.Rows[playerItems.IndexOf(items.Rows[r][0].ToString())][3].ToString())) : false;
                    Image button = owned ? (equipped ? Image.FromFile("GUI/save.png") : Image.FromFile("GUI/save.png")) : Image.FromFile("GUI/scoreboard.png");

                    string text = items.Rows[r][4].ToString() == "0" ? "Free" : $"{items.Rows[r][4].ToString()} coins";
                    Label lblButton = new Label { Name = $"lbl{items.Rows[r][0]}", Text = owned ? (equipped ? "Equipped" : "Equip") : text, Anchor = anchor, Font = lblFont, TextAlign = align, ForeColor = Color.White };

                    lblButton.Click += new EventHandler(itemClick);
                    typesInventory[i].Controls.Add(createInventoryItem(items.Rows[r]), r, 0);
                    typesInventory[i].RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

                    PictureBox btn = new PictureBox { Name = $"btn{items.Rows[r][0]}", BackgroundImage = button, BackgroundImageLayout = ImageLayout.Stretch, Height = 35, Controls = { lblButton }, Cursor = Cursors.Hand };
                    btn.Click += new EventHandler(itemClick);
                    typesInventory[i].Controls.Add(btn, r, 1);
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
            inventoryItem.Size = new Size(100,80);
            string[] rgb = item[5].ToString().Split(',');

            inventoryItem.BackColor = Color.FromArgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));

            inventoryItem.BackgroundImage = Image.FromFile(item[2].ToString());
            inventoryItem.Name = $"pnl{item[0]}";
            inventoryItem.BackgroundImageLayout = ImageLayout.Center;
            if (DataHandler.isAdmin()) 
            {
                inventoryItem.Cursor = Cursors.Hand;
                inventoryItem.Click += new EventHandler(showItem);
            }

            return inventoryItem;
        }

        //einde van inventaris

        //start van item voorbeeld
        private void showPanelItem(string type = null)
        {
            previewType = type;
            this.ActiveControl = Utils.showPnl("pnlItem",tabs);
            tbPower.Maximum = 2;

            if (previewType == null)
            {
                pbDeleteItem.Show();
                DataTable itemtbl = DataHandler.getItem(previewItem);

                lblItem.Text = $"Edit {itemtbl.Rows[0][1]}";
                byte[] imageBytes = (byte[])itemtbl.Rows[0][6];
                MemoryStream buf = new MemoryStream(imageBytes);
                pbItemPreview.BackgroundImage = Image.FromStream(buf, true);

                tbName.Text = itemtbl.Rows[0][1].ToString();
                tbPrice.Text = itemtbl.Rows[0][4].ToString();
                oldPrice = itemtbl.Rows[0][4].ToString();
                if (itemtbl.Rows[0][8].ToString()=="False")
                {
                    cbDefault.Checked = false;
                    cbDefault.Enabled = true;
                    pbDeleteItem.Show();
                }
                else
                {
                    previewItem = null;
                    cbDefault.Checked = true;
                    cbDefault.Enabled = false;
                    pbDeleteItem.Hide();
                }

                tbPower.Value = int.Parse(itemtbl.Rows[0][7].ToString());

                string[] rgb = itemtbl.Rows[0][5].ToString().Split(',');
                Color backgroundColor = Color.FromArgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));

                pbColor.BackColor = backgroundColor;

                pbItemPreview.BackColor = backgroundColor;
            }
            else
            {
                pbItemPreview.BackgroundImage = Image.FromFile("GUI/plus.png");
                DataTable typetbl = DataHandler.getType(previewType);
                lblItem.Text = $"Add {typetbl.Rows[0][1]} skin";
                pbColor.BackColor = Color.FromArgb(224, 224, 224);
                cbDefault.Checked = false;
                cbDefault.Enabled = true;
                pbDeleteItem.Hide();
                pbColor.BackColor = Color.Transparent;
                pbItemPreview.BackColor = Color.Transparent;
                tbName.Clear();
                tbPrice.Clear();
                tbPower.Value = 0;
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
                Filter = "Image Files|*.png;",
            };

            Image item;
            if (Dialog.ShowDialog() == DialogResult.OK)
            {
                Size imageSize = DataHandler.getTypeSize(previewType==null? DataHandler.getTypeItem(previewItem) : previewType);
                item = new Bitmap(Image.FromFile(Dialog.FileName),imageSize);
                pbItemPreview.BackgroundImage = item;
            }
        }

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

            DataHandler.saveItem(tbName.Text,tbPower.Value.ToString(),pbColor.BackColor, pbItemPreview.BackgroundImage, tbPrice.Text, cbDefault.Checked, previewItem,previewType);
            ResourceHandler.checkResources();
            showInventory();
        }

        private void pbColor_Click(object sender, EventArgs e)
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

            MyDialog.Color = rgb.Length <3 ? Color.White : Color.FromArgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));

            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                pbColor.BackColor = MyDialog.Color;
                pbItemPreview.BackColor = MyDialog.Color;
            }
        }

        private void tbPrice_TextChanged(object sender, EventArgs e) => Utils.validateTextInt((TextBox)sender,oldPrice);

        private void pbDeleteItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show($"Are you sure you want to delete this item?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No) return;
            bool deleted = DataHandler.deleteItem(previewItem);
            if (deleted)
            {
                MessageBox.Show($"Item got deleted.");
                showInventory();
            }
            else MessageBox.Show($"Couldn't delete this Item.");
        }
        //einde van item voorbeeld

        //start van settings
        private void pbSettings_Click(object sender, EventArgs e) => showSettings();
        private void pbResetSettings_Click(object sender, EventArgs e)
        {
            DataHandler.createUserDefaultSettings(AccountHandler.getUsername());
            showSettings();
        }
        private void showSettings()
        {
            this.ActiveControl = Utils.showPnl("pnlSettings", tabs);
            tlpSettings.Controls.Clear();
            tlpSettings.RowStyles.RemoveAt(0);
            Font lblFont = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Regular);

            DataTable controls = DataHandler.getUserControls();
            for (int i = 0; i < controls.Rows.Count; i++)
            {
                tlpSettings.RowCount += 1;
                Label btn = new Label()
                {
                    TextAlign = ContentAlignment.MiddleCenter,
                    Anchor = AnchorStyles.Left,
                    Text = Enum.Parse(typeof(Keys), controls.Rows[i][0].ToString()).ToString(),
                    Font = lblFont,
                    ForeColor = Color.White,
                    BackgroundImage = Image.FromFile("GUI/control.png"),
                    BackgroundImageLayout = ImageLayout.Stretch,
                    BackColor = Color.Transparent,
                    Cursor = Cursors.Hand,
                    Name = controls.Rows[i][2].ToString()
                };
                btn.Click += new EventHandler(settingClick);
                tlpSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));
                tlpSettings.Controls.Add(new Label() { TextAlign = ContentAlignment.MiddleRight, Anchor = AnchorStyles.Right | AnchorStyles.Left, Text = $"{controls.Rows[i][1]}:", Font = lblFont, ForeColor = Color.White, Height = 20 }, 0, i);
                tlpSettings.Controls.Add(btn, 1, i);
            }
        }

        private void settingClick(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            lbl.Font = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Bold);
            lbl.Text = "(Press A button)";
            needKey = lbl.Name;
            this.Focus();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Console.WriteLine(keyData);
            Label lbl = tlpSettings.Controls.OfType<Label>().First(label => label.Name == needKey);
            if (needKey != "")
            {
                if (keyData != Keys.Escape)
                {
                    DataHandler.setUserControl(needKey, ((int)keyData).ToString());
                }
                lbl.Text = Enum.Parse(typeof(Keys), DataHandler.getUserControl(needKey).Rows[0][0].ToString()).ToString();
                lbl.Font = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Regular);
                needKey = "";
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void savePassword_Click(object sender, EventArgs e)
        {
            if (Utils.checkPassword(tbPassword.Text,tbRepeatPassword.Text))
            {
                bool done = DataHandler.changePassword(tbPassword.Text);
                if (done)MessageBox.Show("Password successfully changed");
                else MessageBox.Show("Something went wrong");
            }
        }

        //einde van settings

        //start van exit
        private void pbExit_Click(object sender, EventArgs e) => Utils.quit();

        private void MainScreen_KeyPress(object sender, KeyPressEventArgs e)
        {

        }


        //einde van exit
    }
}


