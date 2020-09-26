using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


public partial class frmGUI : Form
{
    public frmGUI()
    {
        InitializeComponent();
        InitializeListView();
        InitListViewProperties();
    }

    public void Say(string txt)
    {
        txtMessage.Text += txt + Environment.NewLine;
    }

    public void ShowProperties(Dictionary<string, string> prop)
    {
        listViewProperties.Items.Clear();
        foreach (KeyValuePair<string, string> p in prop)
        {
            listViewProperties.Items.Add(new ListViewItem(new[] { p.Key, p.Value, }));
        }
    }

    private void InitListViewProperties()
    {
        ColumnHeader header1, header2;
        header1 = new ColumnHeader();
        header2 = new ColumnHeader();
        header1.Text = "Designation";
        header1.TextAlign = HorizontalAlignment.Left;
        header1.Width = 100;
        header2.TextAlign = HorizontalAlignment.Left;
        header2.Text = "Value";
        header2.Width = 200;

        listViewProperties.Columns.Add(header1);
        listViewProperties.Columns.Add(header2);
        listViewProperties.View = View.Details;
        listViewProperties.GridLines = true;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        Script.LoadKRA();
    }
    private void InitializeListView()
    {
        EBPart part = new EBPart();
        olvListViewEbParts.SetObjects(part.GetEBParts());
    }

    private void olvSongs_DoubleClick(object sender, EventArgs e)
    {
        EBPart ebPart = (EBPart)olvListViewEbParts.SelectedObject;
        CreateBox.PlaceBoxOnSelectedPoint(ebPart);
        ebPart.IsGezeichnet = "w";
        //MessageBox.Show(part.BMK);
        //MessageBox.Show(olvSongs.SelectedItem.Text);
        olvListViewEbParts.RefreshObject(ebPart);
        olvListViewEbParts.Update();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        Script.test();
    }

    private void button3_Click(object sender, EventArgs e)
    {
        //Script.butt3();
        Script.GetCoordinate();
    }

    private void button4_Click(object sender, EventArgs e)
    {

    }

    private void btnSetAttr_Click(object sender, EventArgs e)
    {
        MyAttributesForNode.SetNodeMyAttributes("MyAttrubute", "Hallo");
    }
}

