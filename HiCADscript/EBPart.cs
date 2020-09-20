using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class EBPart
{
    public EBPart()
    {

    }
    public EBPart(string bmk, string isGezeichnet, double length, double width, double height)
    {
        BMK = bmk;
        IsGezeichnet = isGezeichnet;
        Length = length;
        Width = width;
        Height = height;

    }
    public string BMK { get; set; }

    public string IsGezeichnet { get; set; }

    public double Length { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }

    public List<EBPart> GetEBParts()
    {
        List<EBPart> EBParts = new List<EBPart>();
        EBParts.Add(new EBPart("-K3", "x", 30, 20, 40));
        EBParts.Add(new EBPart("-K33", "x", 30, 20, 40));
        return EBParts;
    }
}

