using EInkTemplatingAPI.Models;
using Scriban;
using System.IO;

namespace EInkTemplatingAPI.Services;


public class TemplateService
{
    private readonly string _templatesRoot;


    public TemplateService()
    {
        // assume working directory contains /Templates folder
        _templatesRoot = Path.Combine(Directory.GetCurrentDirectory(), "Templates");
    }


    public string RenderProduct(Product product, ScreenProfile screen)
    {
        var templatePath = Path.Combine(_templatesRoot, "ProductTemplate.html");
        var templateText = File.ReadAllText(templatePath);


        var template = Template.Parse(templateText);


        var model = new
        {
            Product = product,
            Screen = new { screen.Width, screen.Height, screen.DiagonalInches, Ppi = screen.Ppi }
        };


        var html = template.Render(model, memberRenamer: member => member.Name);


        // Inject CSS scale so physical size is more consistent across screens (optional)
        double baselinePpi = 167.0;
        double scaleFactor = screen.Ppi / baselinePpi;
        var cssScale = $"<style>body{{zoom:{scaleFactor:0.##};-webkit-text-size-adjust:100%;}}</style>";


        // Insert before </head>
        html = html.Replace("</head>", cssScale + "</head>");
        return html;
    }
}

