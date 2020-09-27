using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class EBPart
{
    
    
    public string BMK { get; set; }
    public string IsGezeichnet { get; set; }
    public double Length { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public string StepPath { get; set; }

    public List<EBPart> GetEBParts()
    {
        List<EBPart> EBParts = new List<EBPart>();
        EBParts.Add(new EBPart { BMK = "- K3", IsGezeichnet = "x", Length = 30, Width = 20, Height = 40 });
        EBParts.Add(new EBPart { BMK = "- K33", IsGezeichnet = "x", Length = 30, Width = 20, Height = 40 });
        EBParts.Add(new EBPart { BMK = "- K50", IsGezeichnet = "x", Length = 30, Width = 20, Height = 40,
            StepPath = @"C:\Users\Andrej\Downloads\STEP-2.14\9666.926(2).stp" });
        return EBParts;
    }}

