
// <debug /> 
// <assembly>API/ISD.CAD.Dimensioning.dll</assembly>
// <assembly>API/ISD.CAD.IO.dll</assembly>
// <assembly>API/ObjectListView.dll</assembly>


using System;
using System.Windows.Forms;
using ISD.Scripting;
using ISD.BaseTypes;
using ISD.Math;
using ISD.CAD.Base;
using ISD.CAD.Data;
using ISD.CAD.Contexts;
using ISD.CAD.Creators;
using ISD.CAD.Dimensioning;
using ISD.CAD.IO;
using System.Collections.Generic;
using ISD.CAD.Interface;


public class Script : ScriptBase
{
    public static UnconstrainedContext Context = (UnconstrainedContext)BaseContext;
    public static ISD.CAD.Data.Scene activeScene = Context.ActiveScene;
    public static frmGUI scriptGUI;

    public static void Main()
    {
        //Hallo Script
        scriptGUI = new frmGUI();
        scriptGUI.Show();
        MyAttributesForNode.GetNodeMyAttributes();
    }
    


    

    public static void test()
    {
        string ftdPath = @"C:\HiCAD\templates\Font_Arial\sys\Bezeichnung.ftd";
        var nodes = new List<ISD.CAD.Data.Node>();
        foreach (ISD.CAD.Data.Node n in Context.ActiveScene.Nodes)
            nodes.Add(n);
        ISD.CAD.Dimensioning.LabelCreation.Create(Context.ActiveScene.ActiveView, nodes, ftdPath, true);

        
    }


    public static void butt3()
    {
        LabelContainer ls = new LabelContainer(Context.ActiveNode);
        foreach (var item in ls)
        {
            item.Delete();
        }
    }


    public static void GetCoordinate()
    {
        scriptGUI.Say(Context.ActiveNode.CoordinateSystem.Origin.X.ToString());
        scriptGUI.Say(Context.ActiveNode.BoundingBox.SizeX.ToString());
        
    }



    public static void Init()
    {
        /// Create a new scene
        //Context.NewScene("EX018.SZA");
        /// Create a new main assembly
        var ac = new AssemblyCreator();
        ac.Name = "Main";

        Context.NewScene("EX018.SZA");
        //MessageBox.Show(cnt.ToString());
        Context.ActiveScene.CreateMainAssembly(ac);

    }

    public static FeatureExecContext Context1
    {
        get
        {
            return BaseContext as FeatureExecContext;
        }
    }

    private static void create(AssemblyNode parent)
    {
        // Create a block
        BlockCreator blockCreator = new BlockCreator(40, 40, 40);
        Part block = Context.CreatePart(blockCreator, parent);
        // Translate the block
        Transformation blockTransformation = new Transformation();
        blockTransformation.SetTranslation(new Vector3D(-20, -20, -20));
        block.Move(blockTransformation);
        // Create a cylinder
        CylinderCreator cylCreator = new CylinderCreator();
        cylCreator.Radius = 10;
        cylCreator.Height = 40;
        Part cyl = Context.CreatePart(cylCreator, parent);
        Transformation cylTransformation = new Transformation();
        // Also transform the cylinder
        cylTransformation.SetTranslation(new Vector3D(0, 0, 20));
        cyl.Move(cylTransformation);
        // Copy the cylinder three times
        Transformation cylRotation = new Transformation();
        for (int i = 0; i < 3; ++i)
        {
            cylRotation.SetIdent();
            cylRotation.SetRotation(new Point3D(0, 0, 0), NormVector3D.E1, (i + 1) * 90.0);
            Node tmp = cyl.Copy();
            tmp.Move(cylRotation);
            tmp.DisplaceTo(parent);
        }

    }

    private static void Label(AssemblyNode parent)
    {
        Node pt = Context.CreatePart(new BlockCreator(100, 100, 100), parent);
        PartLabel simpleLbl = PartLabel.Create(
           new BasePoint(pt, new Point3D(0, 0, 0)),
           new Vector2D(25, -25),
           "DemoLabel",
           Context.CurrentView);
    }

    private static void imortStep()
    {
        ISD.CAD.IO.StepImportSettings settings = new ISD.CAD.IO.StepImportSettings();
        ISD.CAD.IO.FileIO.Load(@"C:\Users\Andrej\Downloads\STEP-2.14\9666.926(2).stp", settings);

    }


    private static void selectItem()
    {
        // Create a list of catalogues to display
        List<String> cats = new List<string>();
        //cats.Add("I_PROFILE");
        //cats.Add("BETONSTAHL");
        cats.Add("BOLDT_8546");

        // Call the dialog and store the result in a Nullable<StandardItem>
        var si = StandardSelectionDialog.SelectStandardItem(cats);
        //StandardItem? si = StandardSelectionDialog.SelectStandardItem(cats);
        //StandardSelectionDialog.StandardSelectionDialog();
        // If si contains a value, the user selected an entry in the dialog
        //if (si.HasValue)
        //MessageBox.Show("si.Value.ToString()");
    }

    public static void LoadKRA()
    {
        ISD.CAD.Data.Scene s = Context.ActiveScene;
        Node e = s.LoadNodeToStoredPosition(@"C:\HiCAD\Kataloge\Werksnormen\BLECHLASCHE.KRA", s.MainAssembly);
        MessageBox.Show(e.Name);
    }


    public static void SetAtr()
    {
        Node parent = (AssemblyNode)Context.ActiveNode;
        parent.AttributeSet.Add(new Attrib("MyString", "fgfgfg"));
        AttributeSet attributes = parent.AttributeSet.CreateSubSet("ProjectAttributes").Value as AttributeSet;
        attributes.Add(new Attrib("MyStringAttribute", "abc"));
    }

}




