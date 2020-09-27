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


static class CreateShapeFromBox
{
    static UnconstrainedContext Context;
    static EBPart ebPart;
    static frmGUI scriptGUI;

    static CreateShapeFromBox()
    {
        Context = Script.Context;
        scriptGUI = Script.scriptGUI;
    }

    public static Part PlaceShapeInCurrentNode(EBPart ebPart)
    {
        AssemblyNode parent = (AssemblyNode)Context.ActiveNode;
        BlockCreator blockCreator = new BlockCreator(ebPart.Length, ebPart.Width, ebPart.Height);
        Part Box = Context.CreatePart(blockCreator, parent);
        Box.Name = ebPart.BMK;

        PartLabel simpleLbl = PartLabel.Create(
         new BasePoint(Box, new Point3D(0, 0, 0)),
         new Vector2D(0, 0),
         ebPart.BMK,
         Context.CurrentView);
        //Figure f;

        //string ftdPath = @"C:\HiCAD\templates\Font_Arial\sys\Textblock.ftd";
        //FigureLabel fl = FigureLabelCreator.Create(ftdPath, activeScene.ActiveFigure, new Point2D(0, 0), new Vector2D(25, -25));

        //2D text
        //FigureTextCreator.Create("ggg", activeScene.ActiveFigure, new Point2D(0, 0), new Angle(90));
        parent.Activate();
        Context.EnforceBrowserUpdate();
        Context.EnforceRedraw();
        return Box;
    }

    public static void PlaceShapeOnSelectedPoint(EBPart EbP)
    {
        ebPart = EbP;
        Selection.SelectionEvent += OnPointSelected;
        Selection.StartSelection(SelectionType.Point, "Select a point");
    }

    private static void OnPointSelected(object sender, SelectionEventArgs args)
    {
        Transformation blockTransformation = new Transformation();

        PointOption lastPoint = args.Result[0] as PointOption;

        if (lastPoint != null)
        {
            scriptGUI.Say(lastPoint.Point.ToString());
            blockTransformation.SetTranslation(new Vector3D(lastPoint.Point));
            Part box = PlaceShapeInCurrentNode(ebPart);
            box.Move(blockTransformation);
        }
        Selection.SelectionEvent -= OnPointSelected;
    }
}
