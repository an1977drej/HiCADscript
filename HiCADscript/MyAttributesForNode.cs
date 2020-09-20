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


public static class MyAttributesForNode
{
    static UnconstrainedContext Context;
    static frmGUI scriptGUI;


    static MyAttributesForNode()
    {
        Context = Script.Context;
        scriptGUI = Script.scriptGUI;
    }
    

    public static void GetNodeMyAttributes()
    {
       
        Scene activeScene = Context.ActiveScene;
        activeScene.NodeSelectionEvent += ActiveScene_NodeSelectionEvent;
    }


     public static void SetNodeMyAttributes(string attrName,  string attrValue)
    {
        Node actNode = Context.ActiveNode;
        if (actNode.AttributeSet.Contains(attrName))
        {
            actNode.AttributeSet[attrName].Value = attrValue;
        }
        else
        {
            actNode.AttributeSet.Add(new Attrib(attrName, attrValue));
        }
        actNode.Selected = true;
        actNode.Selected = false;
    }

    static void ActiveScene_NodeSelectionEvent(object sender, SelectionEventArgs e)
    {
        Dictionary<string, string> p = new Dictionary<string, string>();
        Node actNode = Context.ActiveNode;
        // Display all attributes of the main assembly
        string outputString = "";
        foreach (Attrib attrib in actNode.AttributeSet)
        {
            // Output all values 
            if (attrib.Value.GetType() != typeof(AttributeSet))
            {
                outputString += attrib.Name + " = " + attrib.Value.ToString() + Environment.NewLine;
                p.Add(attrib.Name, attrib.Value.ToString());
            }
                
            else // Output all values of a sub-attribute
                foreach (Attrib att in attrib.Value as AttributeSet)
                {
                    outputString += attrib.Name + "::" + att.Name + " = " + att.Value.ToString() + Environment.NewLine;
                    p.Add(attrib.Name + "::" + att.Name, att.Value.ToString());
                }
        }
        //scriptGUI.Say(outputString);
        scriptGUI.ShowProperties(p);
    }
}


